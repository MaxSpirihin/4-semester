using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicModel
{
    public class LogicOperationOr : LogicOperation
    {
        public LogicIn first { get; set; }
        public LogicIn second { get; set; }

        public LogicOperationOr()
        {
            Out = new LogicOut();
            first = new LogicIn();
            second = new LogicIn();
        }

        public override void Execute()
        {
           bool result = first.Value.Value || second.Value.Value;
           Out.Value = result;
           Out.SendValue();
        }
    }
}
