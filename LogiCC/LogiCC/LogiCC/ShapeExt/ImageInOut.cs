﻿using LogicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace LogiCC.ShapeExt
{
    public class ImageInOut : Image
    {
        public LogicIn logicIn { get; set; }
        public LogicOut logicOut { get; set; }
    }
}
