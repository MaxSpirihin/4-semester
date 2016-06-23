using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace VeCCtor
{
    /// <summary>
    /// содержит все текстовые поля для свойств фигуры
    /// </summary>
    public class Propertys
    {
        public TextBox tbX1 { get; set; }
        public TextBox tbX2 { get; set; }
        public TextBox tbY1 { get; set; }
        public TextBox tbY2 { get; set; }
        public TextBox tbThin { get; set; }

        public TextBox colR { get; set; }
        public TextBox colG { get; set; }
        public TextBox colB { get; set; }
        public TextBox colA { get; set; }

        public TextBox fillR { get; set; }
        public TextBox fillG { get; set; }
        public TextBox fillB { get; set; }
        public TextBox fillA { get; set; }



        public Propertys(MainWindow wind)
        {
            tbX1 = wind.tbX1;
            tbX2 = wind.tbX2;
            tbY1 = wind.tbY1;
            tbY2 = wind.tbY2;
            tbThin = wind.tbThinkness;

            colR = wind.colR;
            colG = wind.colG;
            colB = wind.colB;
            colA = wind.colA;

            fillR = wind.fillR;
            fillG = wind.fillG;
            fillB = wind.fillB;
            fillA = wind.fillA;
        }
    }
}
