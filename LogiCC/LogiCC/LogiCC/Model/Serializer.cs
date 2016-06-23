using LogicModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiCC.Model
{
    class Serializer
    {
        public static string Serialize(List<LogicOperation> operations)
        {
            SetId(operations);
            SerializeTop top = new SerializeTop();
            top.Ins = new List<LogicInS>();
            top.Outs = new List<LogicOutS>();
            top.Opers = new List<LogicOperationS>();
            foreach (LogicOperation oper in operations)
            {
                if (oper.GetType() == typeof(LogicOperationAnd))
                {
                    top.Ins.Add( new LogicInS(((LogicOperationAnd)oper).first));
                    top.Ins.Add(new LogicInS(((LogicOperationAnd)oper).second));
                }
                if (oper.GetType() == typeof(LogicOperationOr))
                {
                    top.Ins.Add(new LogicInS(((LogicOperationOr)oper).first));
                    top.Ins.Add(new LogicInS(((LogicOperationOr)oper).second));
                }
                if (oper.GetType() == typeof(LogicOperationNot))
                {
                    top.Ins.Add(new LogicInS(((LogicOperationNot)oper).first));
                }

                top.Outs.Add(new LogicOutS(oper.Out));
                top.Opers.Add(new LogicOperationS(oper));
            }
            return JsonConvert.SerializeObject(top);
        }

        public static List<LogicOperation> Deserialize(string s)
        {
            SerializeTop top = JsonConvert.DeserializeObject<SerializeTop>(s);
        
            //переводим ины
            List<LogicIn> Ins = new List<LogicIn>();
            foreach (LogicInS topIn in top.Ins)
            {
                LogicIn newIn = new LogicIn();
                newIn.Value = topIn.Value;
                newIn.Id = topIn.Id;
                newIn.bindId = topIn.Bind;
                Ins.Add(newIn);
            }

            //переводим ины
            List<LogicOut> Outs = new List<LogicOut>();
            foreach (LogicOutS topOut in top.Outs)
            {
                LogicOut newOut = new LogicOut();
                newOut.Value = topOut.Value;
                newOut.Id = topOut.Id;
                newOut.bindIds = topOut.Bind;
                Outs.Add(newOut);
            }

            //преобразуем ссылки 
            foreach (LogicIn In in Ins)
            {
                In.Bind = Outs.Where(x => x.Id == In.bindId).FirstOrDefault();
            }
            foreach (LogicOut Out in Outs)
            {
                foreach (int bindId in Out.bindIds)
                {
                    Out.Bind.Add(Ins.Where(x => x.Id == bindId).FirstOrDefault());
                }
            }

            //ну и операцию
            List<LogicOperation> result = new List<LogicOperation>();
            foreach (LogicOperationS operS in top.Opers)
            {
                LogicOperation newOper;
                if (operS.type == LogicOperationS.TYPE_AND)
                {
                    newOper = new LogicOperationAnd(false);
                    ((LogicOperationAnd)newOper).first = Ins.Where(x => x.Id == operS.first).FirstOrDefault();
                    ((LogicOperationAnd)newOper).second = Ins.Where(x => x.Id == operS.second).FirstOrDefault();
                }
                else if (operS.type == LogicOperationS.TYPE_OR)
                {
                    newOper = new LogicOperationOr(false);
                    ((LogicOperationOr)newOper).first = Ins.Where(x => x.Id == operS.first).FirstOrDefault();
                    ((LogicOperationOr)newOper).second = Ins.Where(x => x.Id == operS.second).FirstOrDefault();
                }
                else
                {
                    newOper = new LogicOperationNot(false);
                    ((LogicOperationNot)newOper).first = Ins.Where(x => x.Id == operS.first).FirstOrDefault();
                }
                newOper.Out = Outs.Where(x => x.Id == operS.Out).FirstOrDefault();
                newOper.x = operS.x;
                newOper.y = operS.y;

                result.Add(newOper);
            }

  

            return result;
        }



        private static void SetId(List<LogicOperation> operations)
        {
            //присваиваем каждому LogicIn свой Id
            int id = 1;
            foreach (LogicOperation oper in operations)
            {
                if (oper.GetType() == typeof(LogicOperationAnd))
                {
                    ((LogicOperationAnd)oper).first.Id = id;
                    id++;
                    ((LogicOperationAnd)oper).second.Id = id;
                    id++;
                }
                if (oper.GetType() == typeof(LogicOperationOr))
                {
                    ((LogicOperationOr)oper).first.Id = id;
                    id++;
                    ((LogicOperationOr)oper).second.Id = id;
                    id++;
                }
                if (oper.GetType() == typeof(LogicOperationNot))
                {
                    ((LogicOperationNot)oper).first.Id = id;
                    id++;
                }
            }

            id = 1;
            //каждмому out
            foreach (LogicOperation oper in operations)
            {
                oper.Out.Id = id;
                id++;
            }


            id = 1;
            //каждмому oper
            foreach (LogicOperation oper in operations)
            {
                oper.Id = id;
                id++;
            }
        }
   
        private class LogicInS
        {
            public bool? Value { get; set; }
            public int Id { get; set; }
            public int Bind { get; set; }

            public LogicInS()
            {

            }

            public LogicInS(LogicIn In)
            {
                Value = In.Value;
                Id = In.Id;
                Bind = In.Bind == null ? 0 : In.Bind.Id;
            }
        }

        private class LogicOutS
        {
            public bool? Value { get; set; }
            public int Id { get; set; }
            public List<int> Bind { get; set; }

            public LogicOutS()
            {

            }

            public LogicOutS(LogicOut Out)
            {
                Value = Out.Value;
                Id = Out.Id;
                Bind = new List<int>();
                foreach (LogicIn In in Out.Bind)
                    Bind.Add(In.Id);
            }
        }
    
        private class LogicOperationS        
        {
            public const int TYPE_AND = 1;
            public const int TYPE_OR = 2;
            public const int TYPE_NOT = 3;

            public int Id { get; set; }
            public int type { get; set; }
            public int Out { get; set; }
            public int first { get; set; }
            public int second { get; set; }
            public int x { get; set; }
            public int y { get; set; }

            public LogicOperationS()
            {
            }

            public LogicOperationS(LogicOperation oper)
            {
                Id = oper.Id;
                Out = oper.Out.Id;
                x = oper.x;
                y = oper.y;

                if (oper.GetType() == typeof(LogicOperationAnd))
                {
                    type = TYPE_AND;
                    first = ((LogicOperationAnd)oper).first.Id;
                    second = ((LogicOperationAnd)oper).second.Id;
                }

                if (oper.GetType() == typeof(LogicOperationOr))
                {
                    type = TYPE_OR;
                    first = ((LogicOperationOr)oper).first.Id;
                    second = ((LogicOperationOr)oper).second.Id;
                }


                if (oper.GetType() == typeof(LogicOperationNot))
                {
                    type = TYPE_NOT;
                    first = ((LogicOperationNot)oper).first.Id;
                    second = 0;
                }
            }
        }
    
        private class SerializeTop
        {
            public List<LogicInS> Ins { get; set; }
            public List<LogicOutS> Outs { get; set; }
            public List<LogicOperationS> Opers { get; set; }
        }
    }
}
