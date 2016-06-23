using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using VeCCtor.Main;

namespace VeCCtor
{
    public class Line : Figure
    {

        public Line() : base()
        {
            type = TYPE_LINE;
        }


        public override void Draw(Canvas canva)
        {
            System.Windows.Shapes.Line ln = new System.Windows.Shapes.Line();
            ln.X1 = X1;
            ln.Y1 = Y1;
            ln.X2 = X2;
            ln.Y2 = Y2;
            ln.StrokeThickness = thinkness;
            //ставим цвет линий
            Color realColor = Color.FromArgb(color.a, color.r, color.g, color.b);
            Brush brush = new SolidColorBrush(realColor);
            ln.Stroke = brush;
            canva.Children.Add(ln);
            
        }


        public override void DrawChecked(Canvas canva)
        {
            System.Windows.Shapes.Line ln = new System.Windows.Shapes.Line();
            ln.X1 = X1;
            ln.Y1 = Y1;
            ln.X2 = X2;
            ln.Y2 = Y2;
            ln.StrokeThickness = thinkness;
            //ставим цвет линий
            ln.Stroke = Figure.brushChanged;
            canva.Children.Add(ln);

            DrawCheckedBase(canva);
        }


        public override int MouseIn(System.Windows.Vector mouse)
        {
            int result = MouseInBase(mouse);
            if (result != MOUSE_IN_NO)
                return result;

            double cX = (X1 + X2) / 2;
            double cY = (Y1 + Y2) / 2;
            double height = (float)Math.Sqrt(Math.Pow(X1 - X2, 2) + Math.Pow(Y1 - Y2, 2));


          
            double alpha = (Y2 == Y1) ? 90 : (float)Math.Atan(-(double)(X2 - X1) / (double)(Y2 - Y1)) * 180 / Math.PI;

            if (Utils.PoiinsIsInRectangle(mouse.X, mouse.Y, cX, cY, thinkness, height, alpha))
                return MOUSE_IN;
            return MOUSE_IN_NO;
        }



    }
}
