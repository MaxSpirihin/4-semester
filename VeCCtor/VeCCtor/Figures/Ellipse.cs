using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Runtime.Serialization;

namespace VeCCtor
{
    public class Ellipse : Figure
    {
        
          public Ellipse() : base()
        {
            type = TYPE_ELLIPSE;
        }

        public override void Draw(Canvas canva)
        {
            float tempX1, tempX2, tempY1, tempY2;
            if (X1 < X2)
            {
                tempX1 = X1;
                tempX2 = X2;
            }
            else
            {
                tempX1 = X2;
                tempX2 = X1;
            }

            if (Y1 < Y2)
            {
                tempY1 = Y1;
                tempY2 = Y2;
            }
            else
            {
                tempY1 = Y2;
                tempY2 = Y1;
            }

            //объект и отступы
            System.Windows.Shapes.Ellipse el = new System.Windows.Shapes.Ellipse();
            el.Width = tempX2 - tempX1;
            el.Height = tempY2 - tempY1;
            Canvas.SetLeft(el, tempX1);
            Canvas.SetTop(el, tempY1);
            el.StrokeThickness = thinkness;

            //ставим цвет линий
            Color realColor = Color.FromArgb(color.a, color.r, color.g, color.b);
            Brush brush = new SolidColorBrush(realColor);
            el.Stroke = brush;


            //ставим цвет внутреннности
            Color realColorIn = Color.FromArgb(colorIn.a, colorIn.r, colorIn.g, colorIn.b);
            Brush brushIn = new SolidColorBrush(realColorIn);
            el.Fill = brushIn;


            canva.Children.Add(el);
        }


        public override void DrawChecked(Canvas canva)
        {
            float tempX1, tempX2, tempY1, tempY2;
            if (X1 < X2)
            {
                tempX1 = X1;
                tempX2 = X2;
            }
            else
            {
                tempX1 = X2;
                tempX2 = X1;
            }

            if (Y1 < Y2)
            {
                tempY1 = Y1;
                tempY2 = Y2;
            }
            else
            {
                tempY1 = Y2;
                tempY2 = Y1;
            }

            //объект и отступы
            System.Windows.Shapes.Ellipse el = new System.Windows.Shapes.Ellipse();
            el.Width = tempX2 - tempX1;
            el.Height = tempY2 - tempY1;
            Canvas.SetLeft(el, tempX1);
            Canvas.SetTop(el, tempY1);
            el.StrokeThickness = thinkness;

            //ставим цвет внутреннности
            el.Fill = Figure.brushChanged;



            canva.Children.Add(el);

            DrawCheckedBase(canva);
        }


        public override int MouseIn(Vector mouse)
        {
            int result = MouseInBase(mouse);
            if (result != MOUSE_IN_NO)
                return result;


            double cX = (X1 + X2) / 2;
            double cY = (Y1 + Y2) / 2;
            double rX = Math.Abs((X1 - X2) / 2);
            double rY = Math.Abs((Y1 - Y2) / 2);

            if (Math.Pow((mouse.X - cX) / rX, 2) + Math.Pow((mouse.Y - cY) / rY, 2) < 1)
                return MOUSE_IN;
            return MOUSE_IN_NO;
        }
    }
}
