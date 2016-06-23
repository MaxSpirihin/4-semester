using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicModel
{
    public class LogicIn
    {
        public bool? Value { get; set; }
        public LogicOut Bind { get; set; }

        public LogicIn()
        {
            Value = null;
        }
    }
}
