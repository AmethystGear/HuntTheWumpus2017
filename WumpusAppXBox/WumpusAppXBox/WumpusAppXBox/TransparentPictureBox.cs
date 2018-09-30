using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WumpusAppXBox
{
    /// <summary>
    ///  A ui element that extends the picturebox
    ///  behaves the same as a picturebox except also has zOrder (painting order) functions,
    ///  full transparency against other pictureboxes, and rotation.
    ///  </summary>
    

    class TransparentPictureBox : PictureBox
    {
        //the list of all the TransparentPictureBoxes
        static List<TransparentPictureBox> allTransparentPictureBoxControls = new List<TransparentPictureBox>();

        //the zOrder (order in which painting occurs)
        int zOrder = 0;
        //angle of the pictureBox
        float angleOfPictureBox = 0;
        //whether the picturebox has been made transparent or not
        bool madeTransparent = false;

        //constructor - this version exists so that the designer doesnt break, 
        //the designer won't put in parameters when creating UI elements, 
        //so a blank default constructor must exist.
        public TransparentPictureBox()
        {
            initTransparentPictureBox(0);
        }

        public TransparentPictureBox(int zOrder)
        {
            initTransparentPictureBox(zOrder);
        }

        public void initTransparentPictureBox(int zOrder)
        {
            //make the BackColor Transparent
            BackColor = Color.Transparent;
            //we dont want to see the picturebox, it's image will be drawn by graphics.
            Visible = false;
            //copy over the zOrder
            this.zOrder = zOrder;

            //start at zero
            int n = 0;
            while (n < allTransparentPictureBoxControls.Count && allTransparentPictureBoxControls[n].getZOrder() < zOrder)
            {
                //keep adding to n until the zOrders are bigger or the end of the list is reached.
                n++;
            }
            // insert the picturebox into the ordered list (ordered by zOrder)
            allTransparentPictureBoxControls.Insert(n, this);

        }

        //roatates the picturebox's Image to a certain angle
        public void rotateTo (float angle)
        {
            Image = rotateImage(Image, angle - angleOfPictureBox);
        }

        //rotates an Image passed to it by a certain angle - not used outside of this class
        private Image rotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new PointF(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }

        //returns the zOrder of the image
        public int getZOrder()
        {
            return zOrder;
        }

        //returns all transparent pictureboxes
        public static List<TransparentPictureBox> getAllPictureBoxes()
        {
            return allTransparentPictureBoxControls;
        }

        //removes a pictureBox from the list of pictureboxes
        public static void removePictureBox(TransparentPictureBox t)
        {
            allTransparentPictureBoxControls.Remove(t);
        }


        //paints the picturebox
        public void Paint(PaintEventArgs e)
        {
            //make the image transparent if it's not already. Pure white is assumed to be transparent.
            if (Image != null && !madeTransparent)
            {
                Bitmap b = (Bitmap)Image;
                b.MakeTransparent(Color.White);
                Image = b;
                madeTransparent = true;                
            }
            
            //if the image isn't null, draw the image. Else, print out that this image is null.
            if (Image != null)
            {
                e.Graphics.DrawImage(Image, Location);
            }
            else
            {
                Console.WriteLine(Name + " Image is null!");
            }
                        
        }
        

        //enable transparent painting
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TRANSPARENT = 0x20;
                var cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TRANSPARENT;
                
                return cp;
            }
        }

    }
}
