using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicModel
{
    public class LogicOperation
    {
        public static void Bind(LogicIn In, LogicOut Out)
        {
            In.Bind = Out;
            if (!Out.Bind.Contains(In))
                Out.Bind.Add(In);
        }


        public static void UnBind(LogicIn In, LogicOut Out)
        {
            if (In.Bind == Out)
                In.Bind = null;
            if (Out.Bind.Contains(In))
                Out.Bind.Remove(In);
        }

        public LogicOut Out {get; set;}

        public virtual void Execute()
        {
        }

        public const int SIZE = 50;
        public const int THINKNESS = 50;
        public const Brush COLOR = Brushes.Black;
        public const Brush COLOR_IN = Brushes.Red;



        public int x { get; set; }
        public int y { get; set; }

        public void MainDraw(MainWindow window)
        {
            System.Windows.Shapes.Rectangle rc = new System.Windows.Shapes.Rectangle();
            Canvas.SetLeft(rc, x);
            Canvas.SetTop(rc, y);
            rc.Width = SIZE;
            rc.Height = SIZE;
            rc.StrokeThickness = THINKNESS;
            rc.Stroke = COLOR;
            rc.Fill = COLOR_IN;
            window.WorkField.Children.Add(rc);
        }


    }
}
