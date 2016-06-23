using LogiCC;
using LogiCC.ShapeExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LogicModel
{
    /// <summary>
    /// вход на логическую операцию
    /// </summary>
    public class LogicIn
    {
        public bool? Value { get; set; }
        public LogicOut Bind { get; set; }//связь с выходом другой ЛО

        public LogicIn()
        {
            Value = false;
        }

        #region GUI
    
        public const int SIZE = 10;
        public const int THINKNESS = 1;
        public const int TEXT_WIDTH = 20;
        public const int TEXT_HEIGHT = 22;
        public readonly Brush COLOR = Brushes.Black;
        public readonly Brush COLOR_IN = Brushes.White;

        public int x { get; set; }
        public int y { get; set; }


        public void Draw(MainWindow window, int x, int y)
        {
            this.x = x;
            this.y = y;

            //кружок для связи
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("Icons/InOut.png", UriKind.Relative);
            bi3.EndInit();
            ImageInOut img = new ImageInOut();
            img.logicIn = this;
            img.MouseLeftButtonDown += window.Mouse_DownInOut;
            img.Source = bi3;
            img.Width = SIZE;
            img.Height = SIZE;
            Canvas.SetLeft(img, x - SIZE / 2);
            Canvas.SetTop(img, y - SIZE / 2);
            window.WorkField.Children.Add(img);

            //Кнопка со знчением
            ButtonLogicIn button = new ButtonLogicIn();
            button.logicIn = this;
            button.Click += window.LogicInChangeValue;
            button.Width = TEXT_WIDTH;
            button.Height = TEXT_HEIGHT;
            button.Content = Value == null ? "?" : (Value.Value ? "1" : "0");
            Canvas.SetLeft(button, x + SIZE / 2);
            Canvas.SetTop(button, y - TEXT_HEIGHT / 2);
            window.WorkField.Children.Add(button);
        }
        #endregion

        #region Serialization
        public int Id { get; set; }
        public int bindId { get; set; }

        #endregion
    }
}
