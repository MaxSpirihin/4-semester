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

namespace LogicModel
{
    /// <summary>
    /// Логическая операция - основа
    /// </summary>
    public class LogicOperation
    {

        #region Статические методы

        /// <summary>
        /// Связывает вход и выход 
        /// </summary>
        public static void Bind(LogicIn In, LogicOut Out)
        {
            if (In.Bind == null)
            {
                In.Bind = Out;
                In.Value = null;
                if (!Out.Bind.Contains(In))
                    Out.Bind.Add(In);
            }
        }

        /// <summary>
        /// Удаляет связку между переданными входом и выходом
        /// </summary>
        public static void UnBind(LogicIn In, LogicOut Out)
        {
            if (In.Bind == Out)
            {
                In.Bind = null;
                In.Value = false;
            }
            if (Out.Bind.Contains(In))
                Out.Bind.Remove(In);
        }

        #endregion

        public LogicOut Out { get; set; }

        /// <summary>
        /// вычисление и передача в связанные
        /// </summary>
        public virtual void Execute()
        {
        }

        /// <summary>
        /// вызывать перед удалением
        /// </summary>
        public virtual void PreDelete()
        {
            //чистим все связи в out
            List<LogicIn> inForDelete = new List<LogicIn>();

            foreach (LogicIn In in Out.Bind)
            {
                inForDelete.Add(In);
            }

            foreach (LogicIn In in inForDelete)
            {
                UnBind(In, Out);
            }
        }

        #region GUI

        public const int SIZE = 90;
        public const int THINKNESS = 2;
        public readonly Brush COLOR = Brushes.Black;
        public readonly Brush COLOR_IN = Brushes.Red;

        //координаты
        public int x { get; set; }
        public int y { get; set; }
        //для перемещения
        public int bindX { get; set; }
        public int bindY { get; set; }

        
        public virtual void Draw(MainWindow window)
        {
            Out.Draw(window, x + SIZE, y + SIZE/2);
        }

        /// <summary>
        /// проверка попадания мышт
        /// </summary>
        public bool MouseIn(Vector mouse)
        {
            return ((mouse.X > x) && (mouse.X < x+SIZE) && (mouse.Y >y) && (mouse.Y < y+SIZE));
        }


        /// <summary>
        /// привязать координаты для перемещения
        /// </summary>
        public void MouseBind()
        {
            bindX = x;
            bindY = y;
        }

        /// <summary>
        /// переместить
        /// </summary>
        public bool Translate(int x, int y, int maxWidth, int maxHeight)
        {
            this.x = bindX + x;
            this.y = bindY + y;

            if (this.x < 0)
            {
                this.x = 1;
                return false;
            }
            if (this.y < 0)
            {
                this.y = 1;
                return false;
            }

            if (this.x + SIZE > maxWidth)
            {
                this.x = maxWidth - SIZE - 1;
                return false;
            }
            if (this.y + SIZE > maxHeight)
            {
                this.y = maxHeight - SIZE - 1;
                return false;
            }

            return true;
        }


        public void DrawCurrent(MainWindow window)
        {
            Rectangle rect = new Rectangle();
            rect.Width = SIZE;
            rect.Height = SIZE;
            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
            rect.StrokeThickness = 5;
            rect.Stroke = Brushes.Yellow;
            window.WorkField.Children.Add(rect);
        }


        #endregion

        #region Serialization
        public int Id { get; set; }

        #endregion
    }
}
