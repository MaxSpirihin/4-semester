using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace VeCCtor
{
    public class NumberTextBox : TextBox
    {
        public  bool IsZero {get;set ;}

        static NumberTextBox()
        {
            EventManager.RegisterClassHandler(
                typeof(NumberTextBox),
                DataObject.PastingEvent,
                (DataObjectPastingEventHandler)((sender, e) =>
                {
                    if (!IsDataValid(e.DataObject))
                    {
                        DataObject data = new DataObject();
                        data.SetText(String.Empty);
                        e.DataObject = data;
                        e.Handled = false;
                    }
                }));
        }

        protected override void OnDrop(DragEventArgs e)
        {
            e.Handled = !IsDataValid(e.Data);
            base.OnDrop(e);
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (!IsDataValid(e.Data))
            {
                e.Handled = true;
                e.Effects = DragDropEffects.None;
            }

            base.OnDragEnter(e);
        }

        private static Boolean IsDataValid(IDataObject data)
        {
            Boolean isValid = false;
            if (data != null)
            {
                String text = data.GetData(DataFormats.Text) as String;
                if (!String.IsNullOrEmpty(text == null ? null : text.Trim()))
                {
                    Int32 result = -1;
                    if (Int32.TryParse(text, out result))
                    {
                        if (result > 0)
                        {
                            isValid = true;
                        }
                    }
                }
            }

            return isValid;
        }



        public void BeforeTextChanged()
        {
           if (Text == "")
            {
                Text = "0";
                IsZero = true;
                return;
            }


            if ((IsZero) && (SelectionStart < 2))
            {
                Text = Text.Substring(0, 1);
                SelectionStart = 1;
                IsZero = false;
                return;
            }

            try
            {
                if (Convert.ToInt32(Text) > 500)
                {
                    Text = "500";
                }
            }
            catch { Text = "0"; }


           
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            

            if (e.Key < Key.D0 || e.Key > Key.D9)
            {
                if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
                {
                    if (e.Key != Key.Back)
                    {
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
