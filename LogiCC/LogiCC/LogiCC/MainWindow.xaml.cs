using LogiCC.Model;
using LogiCC.ShapeExt;
using LogicModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogiCC
{
    public partial class MainWindow : Window
    {
        //для связи линий с объектами
        public List<Line> lines { get; set; }
        public List<LineBind> lineBinds { get; set; }

        List<LogicOperation> operations;
        bool isRun;
        string saveString;

        //для перемещения
        bool mouseBinded = false;
        Vector mouseStart;
        int currentMove;
        double width;
        double height;

        //для исполнения
        List<LogicOperation> currentOpers;
        bool[] completed;
        int[] queue;
        int tactNaumber;
        int tactCount;
        int CycleNumber;

        //для создания связи
        Line tempLine;
        LogicIn start;
        LogicOut end;
        bool buttonWasPressed;



        public MainWindow()
        {
            InitializeComponent();
            isRun = false;

            operations = new List<LogicOperation>();
            currentOpers = new List<LogicOperation>();
            lines = new List<Line>();
            lineBinds = new List<LineBind>();

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
                    operations = Serializer.Deserialize(text);
                    UpdateWorkField();
                    UpdateWorkField();
                }
                catch { MessageBox.Show("Ошибка. Файл не корректен."); }
            }
        }


        /// <summary>
        /// обновляет поле - рисует все объекты
        /// </summary>
        void UpdateWorkField()
        {
            //чистим поле
            WorkField.Children.Clear();

            if (isRun)
            {
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri("Icons/Grid.png", UriKind.Relative);
                bi3.EndInit();
                double heiToWid = 3.0 / 5;

                Image img = new Image();
                img.Source = bi3;
                if (height / width < heiToWid)
                {
                    img.Width = width;
                    img.Height = width * heiToWid;
                }
                else
                {
                    img.Height = height;
                    img.Width = height / heiToWid;
                }
                WorkField.Children.Add(img);
            }


            //чистим линии, они добавятся заново
            lines.Clear();
            lineBinds.Clear();

            //если есть временная связь, рисуем ее
            if (tempLine != null)
                WorkField.Children.Add(tempLine);

            //рисуем все логические операции
            foreach (LogicOperation l in operations)
            {
                l.Draw(this);
            }


            if (isRun)
            {
                Rectangle rect = new Rectangle();
                rect.Width = width;
                rect.Height = height - 2.5f > 0 ? height - 2.5f : 0;
                rect.Stroke = Brushes.Red;
                rect.StrokeThickness = 5;
                WorkField.Children.Add(rect);

                foreach (LogicOperation op in currentOpers)
                    op.DrawCurrent(this);
            }
        }


        /// <summary>
        /// смена значения в логическом входе
        /// </summary>
        public void LogicInChangeValue(object sender, RoutedEventArgs e)
        {
            if (isRun) return;

            ButtonLogicIn btn = (ButtonLogicIn)sender;

            //меняем занчение на следующее
            if (btn.logicIn.Bind == null)
            {
                if (btn.logicIn.Value != null)
                    btn.logicIn.Value = !btn.logicIn.Value;
            }
            else
            {
                if (btn.logicIn.Value == null)
                    btn.logicIn.Value = false;
                else if (!btn.logicIn.Value.Value)
                    btn.logicIn.Value = true;
                else
                    btn.logicIn.Value = null;
            }

            UpdateWorkField();
        }

        /// <summary>
        /// обработчик нажатия на поле
        /// </summary>
        private void Mouse_Down(object sender, MouseButtonEventArgs e)
        {
            if (isRun) return;

            //если нажат ребенок, СТОП
            if (buttonWasPressed)
            {
                buttonWasPressed = false;
                return;
            }

            //координты мыши
            Vector mouse = new Vector(e.GetPosition(WorkField).X, e.GetPosition(WorkField).Y);

            //ищем операцию, на которую нажали
            currentMove = -1;
            foreach (LogicOperation l in operations)
                if (l.MouseIn(mouse))
                    currentMove = operations.IndexOf(l);

            //зацепляем ее
            if (currentMove >= 0)
                if (operations[currentMove].MouseIn(mouse))
                    BindMouse(e);


            //если есть временная связь, удаляем ее
            if (tempLine != null)
            {
                tempLine = null;
                start = null;
                end = null;
            }

            UpdateWorkField();
        }


        /// <summary>
        /// привязывает мышь для перемещения
        /// </summary>
        private void BindMouse(MouseButtonEventArgs e)
        {
            mouseBinded = true;
            mouseStart = new Vector(e.GetPosition(WorkField).X, e.GetPosition(WorkField).Y);
            operations[currentMove].MouseBind();
        }



        /// <summary>
        /// обработчик движения по полю
        /// </summary>
        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            if (isRun) return;

            //если мы перемещаем объект
            if (mouseBinded)
            {
                Vector mouse = new Vector(e.GetPosition(WorkField).X, e.GetPosition(WorkField).Y);
                bool result = operations[currentMove].Translate((int)(mouse.X - mouseStart.X), (int)(mouse.Y - mouseStart.Y), (int) width, (int) height);
                UpdateWorkField();

                if ((Mouse.LeftButton == MouseButtonState.Released) || (!result))
                {
                    mouseBinded = false;
                }
            }

            //если есть временная связь
            if (tempLine != null)
            {
                tempLine.X2 = Mouse.GetPosition(WorkField).X;
                tempLine.Y2 = Mouse.GetPosition(WorkField).Y;
                UpdateWorkField();
            }

        }


        /// <summary>
        /// нажатие на связь
        /// </summary>
        public void Mouse_DownInOut(object sender, MouseButtonEventArgs e)
        {
            if (isRun) return;

            //запоминаем привязанные объект входа или выхода
            if (((ImageInOut)sender).logicIn != null)
            {
                start = ((ImageInOut)sender).logicIn;
            }
            else
            {
                end = ((ImageInOut)sender).logicOut;
            }


            if (tempLine == null)
            {
                //это начало временной связи
                tempLine = new Line();
                tempLine.Stroke = Brushes.Black;
                tempLine.StrokeThickness = LogicOut.THINKNESS;
                tempLine.X1 = Mouse.GetPosition(WorkField).X;
                tempLine.Y1 = Mouse.GetPosition(WorkField).Y;
                tempLine.X2 = 200;
                tempLine.Y2 = 200;
            }
            else
            {
                //это конец временной связи
                if (start != null && end != null)
                    LogicOperation.Bind(start, end);

                tempLine = null;
                start = null;
                end = null;
            }

            UpdateWorkField();

            //чтобы тормознуть родителя
            buttonWasPressed = true;
        }


        /// <summary>
        /// нажатие прявой на линию связи - удаление
        /// </summary>
        public void Mouse_DownDelBind(object sender, MouseButtonEventArgs e)
        {
            if (isRun) return;

            int position = lines.IndexOf((Line)sender);//позиция линии

            LogicOperation.UnBind(lineBinds[position].logicIn, lineBinds[position].logicOut);
            UpdateWorkField();
        }

        private void Add_And(object sender, RoutedEventArgs e)
        {
            operations.Add(new LogicOperationAnd(true));
            operations.Last().x = (int)width / 2 - LogicOperation.SIZE / 2;
            operations.Last().y = (int)height / 2 - LogicOperation.SIZE / 2;
            UpdateWorkField();
            try
            {
                ((Button)sender).Focusable = false;
            }
            catch { }
        }

        private void Add_Or(object sender, RoutedEventArgs e)
        {
            operations.Add(new LogicOperationOr(true));
            operations.Last().x = (int)width / 2 - LogicOperation.SIZE / 2;
            operations.Last().y = (int)height / 2 - LogicOperation.SIZE / 2;
            UpdateWorkField();
            try
            {
                ((Button)sender).Focusable = false;
            }
            catch { }
        }

        private void Add_Not(object sender, RoutedEventArgs e)
        {
            operations.Add(new LogicOperationNot(true));
            operations.Last().x = (int)width / 2 - LogicOperation.SIZE / 2;
            operations.Last().y = (int)height / 2 - LogicOperation.SIZE / 2;
            UpdateWorkField();
            try
            {
                ((Button)sender).Focusable = false;
            }
            catch { }
        }

        private void Mouse_Right_Down(object sender, MouseButtonEventArgs e)
        {
            if (isRun) return;

            //координты мыши
            Vector mouse = new Vector(e.GetPosition(WorkField).X, e.GetPosition(WorkField).Y);

            currentMove = -1;
            foreach (LogicOperation l in operations)
                if (l.MouseIn(mouse))
                    currentMove = operations.IndexOf(l);

            if (currentMove >= 0)
            {
                operations[currentMove].PreDelete();
                operations.RemoveAt(currentMove);
            }

            UpdateWorkField();
        }

        private void Play(object sender, RoutedEventArgs e)
        {

            if (!isRun)
            {
                if (!isRun && operations.Count == 0)
                {
                    MessageBox.Show("Для запуска создайте схему :)");
                }
                else
                {

                    //бэкапим
                    saveString = Serializer.Serialize(operations);
                    //такт
                    tactNaumber = 0;
                    LabelConsole.Content = String.Format("Исполнение. Тактов выполнено - {0}", tactNaumber);
                    Cycle.IsEnabled = false;
                    
                    isRun = true;
                    CycleNumber = 0;
                    //для отметок о выполнении
                    completed = new bool[operations.Count];
                    queue = new int[operations.Count];
                    //ищем новые элементы
                    ComputeCurrentOpers();

                    if (currentOpers.Count == 0)
                    {
                        MessageBox.Show("Схема некорректна. Ни одна операция не может быть испонена. Задайте начальные значения. ");
                        Stop(null,null);
                    }
                }

            }
            else
            {
                //выполняем такт
                foreach (LogicOperation oper in currentOpers)
                {
                    oper.Execute();
                    if (CycleNumber==0)
                    {
                        completed[operations.IndexOf(oper)] = true;

                        queue[operations.IndexOf(oper)] = tactNaumber;
                    }
                    
                }
                //такт
                if (currentOpers.Count != 0)
                    tactNaumber++;
                
                //ищем новые элементы
                ComputeCurrentOpers();
                //если их нет, то конец.
                if (currentOpers.Count == 0)
                {
                    if (!Cycle.IsChecked.Value)
                        LabelConsole.Content = String.Format("Завершено. Тактов выполнено - {0}", tactNaumber);
                    else
                    {
                        //начинаем цикл
                        CycleNumber++;
                        tactCount = tactNaumber ;
                        tactNaumber = 0;
                        ComputeCurrentOpers();

                        LabelConsole.Content = String.Format("Исполнение. Тактов выполнено - {0}", tactNaumber + tactCount*CycleNumber);
                    }

                }
                else
                    LabelConsole.Content = String.Format("Исполнение. Тактов выполнено - {0}", tactNaumber + tactCount * CycleNumber);


            }
            UpdateWorkField();
            try
            {
                ((Button)sender).Focusable = false;
            }
            catch { }
        }


        private void ComputeCurrentOpers()
        {
            currentOpers.Clear();

            if (CycleNumber == 0)
                foreach (LogicOperation lo in operations)
                {
                    if (!completed[operations.IndexOf(lo)])
                    {
                        if (lo.GetType() == typeof(LogicOperationAnd))
                            if (((LogicOperationAnd)lo).first.Value != null && ((LogicOperationAnd)lo).second.Value != null)
                                currentOpers.Add(lo);

                        if (lo.GetType() == typeof(LogicOperationOr))
                            if (((LogicOperationOr)lo).first.Value != null && ((LogicOperationOr)lo).second.Value != null)
                                currentOpers.Add(lo);

                        if (lo.GetType() == typeof(LogicOperationNot))
                            if (((LogicOperationNot)lo).first.Value != null)
                                currentOpers.Add(lo);
                    }
                }
            else
                for (int i = 0; i < queue.Count() ;i++ )
                    if (queue[i] == tactNaumber)
                        currentOpers.Add(operations[i]);
        }


        private void Stop(object sender, RoutedEventArgs e)
        {
            if (!isRun) return;

            Cycle.IsEnabled = true;
            isRun = false;
            LabelConsole.Content = "Режим правки";
            operations = Serializer.Deserialize(saveString);
            UpdateWorkField();
            UpdateWorkField();
            try
            {
                ((Button)sender).Focusable = false;
            }
            catch { }
        }

        private void WorkField_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            width = ((Canvas)sender).ActualWidth;
            height = ((Canvas)sender).ActualHeight;
            UpdateWorkField();
        }


        private void About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Разработчик - Максим Спирихин");
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            operations = new List<LogicOperation>();
            UpdateWorkField();
            try
            {
                ((Button)sender).Focusable = false;
            }
            catch { }
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "LogiCC File (*.lgcc)|*.lgcc";
            if (openFileDialog.ShowDialog() == true)
            {
                string text = System.IO.File.ReadAllText(openFileDialog.FileName);

                try
                {
                    operations = Serializer.Deserialize(text);
                    UpdateWorkField();
                    UpdateWorkField();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }


        private void Save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "LogiCC File (*.lgcc)|*.lgcc";
            if (saveFileDialog.ShowDialog() == true)
            {
                string text = Serializer.Serialize(operations);

                System.IO.File.WriteAllText(saveFileDialog.FileName, text);
            }
        }

        private void New(object sender, RoutedEventArgs e)
        {
            MessageBoxResult key = MessageBox.Show(
           "Save current file?",
           "Confirm",
           MessageBoxButton.YesNo,
           MessageBoxImage.Question,
           MessageBoxResult.No);
            if (key == MessageBoxResult.Yes)
                Save(null, null);

            Clear(null, null);
        }





    }
}
