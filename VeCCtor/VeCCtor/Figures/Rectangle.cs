using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VeCCtor
{
    public class Rectangle : Figure
    {

        public Rectangle() : base()
        {
            type = TYPE_RECTANGLE;
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

            System.Windows.Shapes.Rectangle rc = new System.Windows.Shapes.Rectangle();
            Canvas.SetLeft(rc, tempX1);
            Canvas.SetTop(rc, tempY1);
            rc.Width = tempX2 - tempX1;
            rc.Height = tempY2 - tempY1;
            rc.StrokeThickness = thinkness;
            
            //ставим цвет линий
            Color realColor = Color.FromArgb(color.a, color.r, color.g, color.b);
            Brush brush = new SolidColorBrush(realColor);
            rc.Stroke = brush;


            //ставим цвет внутреннности
            Color realColorIn = Color.FromArgb(colorIn.a, colorIn.r, colorIn.g, colorIn.b);
            Brush brushIn = new SolidColorBrush(realColorIn);
            rc.Fill = brushIn;


            canva.Children.Add(rc);

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

            System.Windows.Shapes.Rectangle rc = new System.Windows.Shapes.Rectangle();
            Canvas.SetLeft(rc, tempX1);
            Canvas.SetTop(rc, tempY1);
            rc.Width = tempX2 - tempX1;
            rc.Height = tempY2 - tempY1;
            rc.StrokeThickness = thinkness;

            //ставим цвет внутреннности
            rc.Fill = Figure.brushChanged;

            canva.Children.Add(rc);

            DrawCheckedBase(canva);
        }
   
    
        public override int MouseIn(Vector mouse)
        {
            int result = MouseInBase(mouse);
            if (result != MOUSE_IN_NO)
                return result;


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

            if ((mouse.X > tempX1) && (mouse.X < tempX2) && (mouse.Y > tempY1) && (mouse.Y < tempY2))
                return MOUSE_IN;
            return MOUSE_IN_NO;
        }
    }
}
