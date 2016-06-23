using LogiCC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using LogiCC.ShapeExt;
using System.Windows.Media.Imaging;

namespace LogicModel
{
    /// <summary>
    /// выход из логической операции
    /// </summary>
    public class LogicOut
    {
        public bool? Value { get; set; }
        public List<LogicIn> Bind { get; set; }//связаныые входы

        public LogicOut()
        {
            Value = null;
            Bind = new List<LogicIn>();
        }

        /// <summary>
        /// посылает вычисленное значение на связнные входы
        /// </summary>
        public void SendValue()
        {
            foreach (LogicIn l in Bind)
                l.Value = Value;
        }

        #region GUI

        public int x { get; set; }
        public int y { get; set; }

        public const int SIZE = 10;
        public const int THINKNESS = 1;
        public const int TEXT_WIDTH = 20;
        public const int TEXT_HEIGHT = 22;

        public void Draw(MainWindow window, int x, int y)
        {
            this.x = x;
            this.y = y;


            //линии связи
            foreach (LogicIn l in Bind)
            {
                Line ln = new Line();
                ln.MouseRightButtonDown += window.Mouse_DownDelBind;
                ln.X1 = x;
                ln.Y1 = y;
                ln.X2 = l.x;
                ln.Y2 = l.y;
                ln.StrokeThickness = 3;
                ln.Stroke = Brushes.Black;
                window.WorkField.Children.Add(ln);

                //это для связи линии с объектами
                LineBind lb = new LineBind() { logicIn = l, logicOut = this };
                window.lineBinds.Add(lb);
                window.lines.Add(ln);
            }


            //кружок для связи
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("Icons/InOut.png", UriKind.Relative);
            bi3.EndInit();

            ImageInOut img = new ImageInOut();
            img.logicOut = this;
            img.MouseLeftButtonDown += window.Mouse_DownInOut;
            img.Source = bi3;
            img.Width = SIZE;
            img.Height = SIZE;
            Canvas.SetLeft(img, x - SIZE/2);
            Canvas.SetTop(img, y - SIZE/2);
            window.WorkField.Children.Add(img);

            //Значение
            Button tbValue = new Button();
            tbValue.Width = TEXT_WIDTH;
            tbValue.Height = TEXT_HEIGHT;
            tbValue.Content = Value == null ? "?" : (Value.Value ? "1" : "0");
            Canvas.SetLeft(tbValue, x - SIZE - TEXT_WIDTH);
            Canvas.SetTop(tbValue, y - TEXT_HEIGHT/2);
            window.WorkField.Children.Add(tbValue);


        
        }
        #endregion

        #region Serialization
        public int Id { get; set; }
        public List<int> bindIds { get; set; }
        #endregion
    }
}
