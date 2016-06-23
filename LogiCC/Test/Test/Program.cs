using LogicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<LogicOperation> operations = new List<LogicOperation>();

            LogicOperationAnd add = new LogicOperationAnd();
            add.first.Value = true;
            add.second.Value = false;
            operations.Add(add);

            add = new LogicOperationAnd();
            add.first.Value = true;
            add.second.Value = true;
            operations.Add(add);

            add = new LogicOperationAnd();
            operations.Add(add);

            add = new LogicOperationAnd();
            add.first.Value = true;
            operations.Add(add);

            LogicOperation.Bind(((LogicOperationAnd)operations[3]).second, operations[0].Out);
            LogicOperation.Bind(((LogicOperationAnd)operations[2]).first, operations[0].Out);
            LogicOperation.Bind(((LogicOperationAnd)operations[2]).second, operations[1].Out);

            foreach (LogicOperation l in operations)
            {
                l.Execute();
            }
            

        }
    }
}
