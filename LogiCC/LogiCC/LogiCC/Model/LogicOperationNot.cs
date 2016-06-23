using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LogicModel
{
    public class LogicOperationNot : LogicOperation
    {
        public LogicIn first { get; set; }

        public LogicOperationNot(bool CreateInOuts)
        {
            if (CreateInOuts)
            {
                Out = new LogicOut();
                first = new LogicIn();
            }
        }

        public override void Execute()
        {
           bool result = !first.Value.Value;
           Out.Value = result;
           Out.SendValue();
        }

        public override void PreDelete()
        {
            base.PreDelete();
            if (first.Bind != null)
                UnBind(first, first.Bind);
        }

        #region GUI

        public override void Draw(LogiCC.MainWindow window)
        {
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("Icons/Not.png", UriKind.Relative);
            bi3.EndInit();

            Image img = new Image();
            img.Source = bi3;
            img.Width = SIZE;
            img.Height = SIZE;
            Canvas.SetLeft(img, x);
            Canvas.SetTop(img, y);
            window.WorkField.Children.Add(img);

            base.Draw(window);
            first.Draw(window, x, y + SIZE / 2);
        }
        #endregion
    }
}
