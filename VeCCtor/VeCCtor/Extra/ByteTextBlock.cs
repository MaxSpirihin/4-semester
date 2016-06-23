using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VeCCtor
{
    class ByteTextBox : NumberTextBox
    {
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


            if (Convert.ToInt32(Text) > 255)
            {
                Text = "255";
            }



        }
    }
}
