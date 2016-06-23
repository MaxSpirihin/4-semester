using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicModel
{
    public class LogicOut
    {
        public bool? Value { get; set; }
        public List<LogicIn> Bind { get; set; }

        public LogicOut()
        {
            Value = null;
            Bind = new List<LogicIn>();
        }

        public void SendValue()
        {
            foreach (LogicIn l in Bind)
                l.Value = Value;
        }
    }
}
