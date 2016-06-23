using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VeCCtor.Extra;

namespace VeCCtor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //основные объекты
        List<Figure> figures;
        int current = -1;
        Propertys prop;
        bool gridOn = true;
        Figure buffer;

        //для перемещения
        bool mouseBinded = false;
        Vector mouseStart;



        //начало
        public MainWindow()
        {
            InitializeComponent();



            //поля свойств
            prop = new Propertys(this);

            //основа
            figures = new List<Figure>();// { new Ellipse(), new Rectangle(), new Line() };




            this.Loaded += new RoutedEventHandler(MainContainer_Loaded);

            UpdateWorkField();
        }


        void MainContainer_Loaded(object sender, RoutedEventArgs e)
        {

            if (Application.Current.Properties["ArbitraryArgName"] != null)
            {
                string fname = Application.Current.Properties["ArbitraryArgName"].ToString();
                string text = System.IO.File.ReadAllText(fname);


                try
                {
                    figures = Serializer.Deserialize(text);
                    UpdateWorkField();
                }
                catch { MessageBox.Show("Error Load File"); }
            }
        }




        //изменение некого поля
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            //передаем изменния полю для редактирования
            ((NumberTextBox)sender).BeforeTextChanged();

        }


        private void UpdateWorkField()
        {
            PropertiesField.Visibility = current >= 0 ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;

            LabelClick.Content = figures.Count == 0 ? "Create object\n (Edit -> Add)" : "Click to any object";
            LabelClick.Visibility = current < 0 ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;

            WorkField.Children.RemoveRange(0, WorkField.Children.Count);
            if (gridOn)
                WorkField.Children.Add(Grid);

            foreach (Figure f in figures)
            {
                f.Draw(WorkField);
                if (figures.IndexOf(f) == current)
                    f.DrawChecked(WorkField);
            }

            if (current >= 0)
            {
                figures[current].InflateFields(prop);
            }

        }

        private void Mouse_Down(object sender, MouseButtonEventArgs e)
        {
            Vector mouse = new Vector(e.GetPosition(WorkField).X, e.GetPosition(WorkField).Y);

            current = -1;
            foreach (Figure f in figures)
                if (f.MouseIn(mouse) != Figure.MOUSE_IN_NO)
                    current = figures.IndexOf(f);


            if (current >= 0)
                if (figures[current].MouseIn(mouse) != Figure.MOUSE_IN_NO)
                    BindMouse(e, figures[current].MouseIn(mouse));

            UpdateWorkField();
        }





        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            if (mouseBinded)
            {
                Vector mouse = new Vector(e.GetPosition(WorkField).X, e.GetPosition(WorkField).Y);
                bool result = figures[current].Translate((int)(mouse.X - mouseStart.X), (int)(mouse.Y - mouseStart.Y));
                UpdateWorkField();

                if ((Mouse.LeftButton == MouseButtonState.Released) || (!result))
                {
                    mouseBinded = false;
                }

            }

        }


        private void BindMouse(MouseButtonEventArgs e, int state)
        {
            mouseBinded = true;
            mouseStart = new Vector(e.GetPosition(WorkField).X, e.GetPosition(WorkField).Y);
            figures[current].Bind(state);
        }


        private void DrawCircle(int cX, int cY, int radius)
        {
            System.Windows.Shapes.Ellipse el = new System.Windows.Shapes.Ellipse();
            el.Width = 2 * radius;
            el.Height = 2 * radius;
            el.VerticalAlignment = VerticalAlignment.Top;
            el.Fill = Brushes.Red;
            Canvas.SetLeft(el, cX - radius);
            Canvas.SetTop(el, cY - radius);
            WorkField.Children.Add(el);
        }


        private void AddRect(object sender, RoutedEventArgs e)
        {
            figures.Add(new Rectangle());
            current = figures.Count - 1;
            UpdateWorkField();
        }

        private void AddEllipse(object sender, RoutedEventArgs e)
        {
            figures.Add(new Ellipse());
            current = figures.Count - 1;
            UpdateWorkField();
        }

        private void AddLine(object sender, RoutedEventArgs e)
        {
            figures.Add(new Line());
            current = figures.Count - 1;
            UpdateWorkField();
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {


            //обновляем свойства фигуры
            if (current >= 0)
            {
                figures[current].GetProperties(prop);
                UpdateWorkField();
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            figures.Clear();
            current = -1;
            UpdateWorkField();
        }

        private void GridOnOffClick(object sender, RoutedEventArgs e)
        {
            gridOn = !gridOn;
            GridOnOff.Header = gridOn ? "Off Grid" : "On Grid";
            UpdateWorkField();
        }


        private void AboutClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The program is developed by Maxim Spirikhin");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (current >= 0)
            {
                figures.Remove(figures[current]);
                current = -1;
                UpdateWorkField();
            }
        }

        private void NewClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult key = MessageBox.Show(
           "Save current file?",
           "Confirm",
           MessageBoxButton.YesNo,
           MessageBoxImage.Question,
           MessageBoxResult.No);
            if (key == MessageBoxResult.Yes)
                SaveClick(null, null);

            Clear(null, null);
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "VeCCtor File (*.vecc)|*.vecc";
            if (saveFileDialog.ShowDialog() == true)
            {
                string text = Serializer.Serialize(figures);

                System.IO.File.WriteAllText(saveFileDialog.FileName, text);
            }

        }

        private void LoadClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "VeCCtor File (*.vecc)|*.vecc";
            if (openFileDialog.ShowDialog() == true)
            {
                string text = System.IO.File.ReadAllText(openFileDialog.FileName);

                try
                {
                    figures = Serializer.Deserialize(text);
                    current = -1;
                    UpdateWorkField();
                }
                catch { MessageBox.Show("Error"); }
            }

        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            if (current >= 0)
                buffer = figures[current].Clone();
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            if (buffer != null)
                figures.Add(buffer.Clone());
            UpdateWorkField();
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C && Keyboard.Modifiers == ModifierKeys.Control)
            {
                Copy(null, null);
            }

            if (e.Key == Key.Delete)
            {
                Delete_Click(null, null);
            }

            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                Paste(null, null);
            }

            if (e.Key == Key.N && Keyboard.Modifiers == ModifierKeys.Control)
            {
                NewClick(null, null);
            }

            if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
            {
                SaveClick(null, null);
            }

            if (e.Key == Key.O && Keyboard.Modifiers == ModifierKeys.Control)
            {
                LoadClick(null, null);
            }

        }

        private void Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (figures.Count > 0)
            {

                MessageBoxResult key = MessageBox.Show(
              "Save current file?",
              "Confirm",
              MessageBoxButton.YesNo,
              MessageBoxImage.Question,
              MessageBoxResult.No);
                if (key == MessageBoxResult.Yes)
                    SaveClick(null, null);
            }
        }



    }
}
