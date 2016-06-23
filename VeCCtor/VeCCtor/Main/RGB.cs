using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VeCCtor
{
    public class RGB
    {
        public byte r { get; set; }
        public byte g { get; set; }
        public byte b { get; set; }
        public byte a { get; set; }

        public RGB(bool hidden)
        {
            r = 0;
            g = 0;
            b = 0;

            if (hidden)
                a = 0;
            else
                a = 1;
        }
    }
}
