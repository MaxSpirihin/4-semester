using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicModel
{
    public class LogicOperationNot : LogicOperation
    {
        public LogicIn first { get; set; }

        public LogicOperationNot()
        {
            Out = new LogicOut();
            first = new LogicIn();
        }

        public override void Execute()
        {
           bool result = !first.Value.Value;
           Out.Value = result;
           Out.SendValue();
        }
    }
}
