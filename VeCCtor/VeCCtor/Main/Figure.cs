using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VeCCtor
{
    /// <summary>
    /// основной класс
    /// </summary>
    public class Figure
    {
        public static Brush brushChanged;
        public const int WIDTH =20; 
        public const int MOUSE_IN_1 = 1;
        public const int MOUSE_IN_2 = 2;
        public const int MOUSE_IN_NO = -1;
        public const int MOUSE_IN = 0;
        public const int TYPE_ELLIPSE = 0;
        public const int TYPE_RECTANGLE = 1;
        public const int TYPE_LINE = 2;



        public int type { get; set; }
        public int X1 { get; set; }
        public int X2 { get; set; }
        public int Y1 { get; set; }
        public int Y2 { get; set; }
        public int thinkness { get; set; }
        public RGB color { get; set; }
        public RGB colorIn { get; set; }

        public int bindX1 { get; set; }
        public int bindY1 { get; set; }
        public int bindX2 { get; set; }
        public int bindY2 { get; set; }
        public int state { get; set; }


        /// <summary>
        /// создает фигуру по умолчанию в центре
        /// </summary>
        public Figure()
        {
            if (brushChanged == null)
            {
                Color realColor = Color.FromArgb(200,0,250,100);
                brushChanged = new SolidColorBrush(realColor); 
            }

            X1 = 150;
            X2 = 350;
            Y1 = 150;
            Y2 = 350;
            thinkness = 5;
            color = new RGB(false) { r=0, g=0, b=0, a=255 };
            colorIn = new RGB(false) { r = 255, g = 255, b = 255, a = 255 };
        }


        public Figure Clone()
        {
            Figure f = null;

                switch(type)
                {
                    case Figure.TYPE_ELLIPSE:
                        f = new Ellipse();
                        break;
                    case Figure.TYPE_LINE:
                        f = new Line();
                        break;
                    case Figure.TYPE_RECTANGLE:
                        f = new Rectangle();
                        break;
                }
                f.X1 = X1;
                f.X2 = X2;
                f.Y1 = Y1;
                f.Y2 = Y2;
                f.thinkness = thinkness;
                f.color.a = color.a;
                f.color.r = color.r;
                f.color.g = color.g;
                f.color.b = color.b;
                f.colorIn.a = colorIn.a;
                f.colorIn.r = colorIn.r;
                f.colorIn.g = colorIn.g;
                f.colorIn.b = colorIn.b;

                return f;
        }


        /// <summary>
        /// отрисовка фигуры
        /// </summary>
        /// <param name="canva"></param>
        public virtual void Draw(Canvas canva)
        {
        }


        /// <summary>
        /// отрисовка фигуры, если она выделена
        /// </summary>
        /// <param name="canva"></param>
        public virtual void DrawChecked(Canvas canva)
        {
        }


        /// <summary>
        /// заполнение свойств в полях из данных фигуры
        /// </summary>
        /// <param name="prop"></param>
        public void InflateFields(Propertys prop)
        {
            prop.tbX1.Text = X1.ToString();
            prop.tbX2.Text = X2.ToString();
            prop.tbY1.Text = Y1.ToString();
            prop.tbY2.Text = Y2.ToString();
            prop.tbThin.Text = thinkness.ToString();

            prop.colR.Text = color.r.ToString();
            prop.colG.Text = color.g.ToString();
            prop.colB.Text = color.b.ToString();
            prop.colA.Text = color.a.ToString();

            prop.fillR.Text = colorIn.r.ToString();
            prop.fillG.Text = colorIn.g.ToString();
            prop.fillB.Text = colorIn.b.ToString();
            prop.fillA.Text = colorIn.a.ToString();
        }



        /// <summary>
        /// получение свойств фигуры из полей
        /// </summary>
        /// <param name="prop"></param>
        public void GetProperties(Propertys prop)
        {
            try
            {
                X1 = Convert.ToInt32(prop.tbX1.Text);
                X2 = Convert.ToInt32(prop.tbX2.Text);
                Y1 = Convert.ToInt32(prop.tbY1.Text);
                Y2 = Convert.ToInt32(prop.tbY2.Text);
                thinkness = Convert.ToInt32(prop.tbThin.Text);
            }
            catch { }


            //теперь цвета
            try
            {
                
                color.r = Convert.ToByte(prop.colR.Text);
                color.g = Convert.ToByte(prop.colG.Text);
                color.b = Convert.ToByte(prop.colB.Text);
                color.a = Convert.ToByte(prop.colA.Text);
            }
            catch { }

            try
            {

                colorIn.r = Convert.ToByte(prop.fillR.Text);
                colorIn.g = Convert.ToByte(prop.fillG.Text);
                colorIn.b = Convert.ToByte(prop.fillB.Text);
                colorIn.a = Convert.ToByte(prop.fillA.Text);
            }
            catch { }

        }
   
    
        public virtual int MouseIn(Vector mouse)
        {
            return MOUSE_IN_NO;
        }

        public void Bind(int state)
        {
            bindX1 = X1;
            bindY1 = Y1;
            bindX2 = X2;
            bindY2 = Y2;
            this.state = state;
        }

        protected void DrawCheckedBase(Canvas canva)
        {
           /* //объект и отступы
            System.Windows.Shapes.Ellipse el = new System.Windows.Shapes.Ellipse();
            el.Width = WIDTH;
            el.Height = WIDTH;
            Canvas.SetLeft(el, X1 - WIDTH/2);
            Canvas.SetTop(el, Y1 - WIDTH/2);
            el.StrokeThickness = 0;
            el.Fill = Brushes.Black;*/


            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("Icons/hand.png", UriKind.Relative);
            bi3.EndInit();

            Image img = new Image();
            img.Source = bi3;
            img.Width = WIDTH;
            img.Height = WIDTH;
            Canvas.SetLeft(img, X1 - WIDTH / 2);
            Canvas.SetTop(img, Y1 - WIDTH / 2);

            Image img2 = new Image();
            img2.Source = bi3;
            img2.Width = WIDTH;
            img2.Height = WIDTH;
            Canvas.SetLeft(img2, X2 - WIDTH / 2);
            Canvas.SetTop(img2, Y2 - WIDTH / 2);



            canva.Children.Add(img);
            canva.Children.Add(img2);
        }


        protected int MouseInBase(Vector mouse)
        {
            if (Math.Pow((mouse.X - X1)*2 / WIDTH, 2) + Math.Pow((mouse.Y - Y1)*2 / WIDTH, 2) < 1)
                return MOUSE_IN_1;
            if (Math.Pow((mouse.X - X2)*2 / WIDTH, 2) + Math.Pow((mouse.Y - Y2)*2 / WIDTH, 2) < 1)
                return MOUSE_IN_2;
            return MOUSE_IN_NO;

        }

        public bool Translate(int x, int y)
        {

            int width = X2 - X1;
            int height = Y2 - Y1;

            if ((state == MOUSE_IN_1) || (state == MOUSE_IN))
            {
                X1 = bindX1 + x;
                Y1 = bindY1 + y;
            }
            if ((state == MOUSE_IN_2) || (state == MOUSE_IN))
            {
                X2 = bindX2 + x;
                Y2 = bindY2 + y;
            }

            int min = -1;
            int max = 501;

            if (X1 < min)
            {
                X1 = min+1;
                X2 = X1 + width;
                return false;
            }
            if (X1 > max)
            {
                X1 = max-1;
                X2 = X1 + width;
                return false;
            }
            if (X2 < min)
            {
                X2 = min + 1;
                X1 = X2 - width;
                return false;
            }
            if (X2 > max)
            {
                X2 = max - 1;
                X1 = X2 - width;
                return false;
            }
            if (Y1 < min)
            {
                Y1 = min + 1;
                Y2 = Y1 + height;
                return false;
            }
            if (Y1 > max)
            {
                Y1 = max - 1;
                Y2 = Y1 + height;
                return false;
            }
            if (Y2 < min)
            {
                Y2 = min + 1;
                Y1 = Y2 - height;
                return false;
            }
            if (Y2 > max)
            {
                Y2 = max - 1;
                Y1 = Y2 - height;
                return false;
            }

            return true;
            
        }

       
    }
}
