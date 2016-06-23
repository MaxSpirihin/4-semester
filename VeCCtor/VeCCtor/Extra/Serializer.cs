using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VeCCtor.Extra
{
    public class Serializer
    {
            
        public static string Serialize(List<Figure> figures)
        {
           return JsonConvert.SerializeObject(figures);
        }

        public static List<Figure> Deserialize(string jsonString)
        {
            List<Figure> jArr = JsonConvert.DeserializeObject < List<Figure>>(jsonString);
       
            //заменим на наследников
            List<Figure> result = new List<Figure>();
            foreach(Figure last in jArr)
            {
                Figure f = new Figure();
                switch(last.type)
                {
                    case Figure.TYPE_ELLIPSE:
                        f = new Ellipse();
                        break;
                    case Figure.TYPE_LINE:
                        f = new Line();
                        break;
                    case Figure.TYPE_RECTANGLE:
                        f = new Rectangle();
                        break;
                }
                f.X1 = last.X1;
                f.X2 = last.X2;
                f.Y1 = last.Y1;
                f.Y2 = last.Y2;
                f.thinkness = last.thinkness;
                f.color.a = last.color.a;
                f.color.r = last.color.r;
                f.color.g = last.color.g;
                f.color.b = last.color.b;
                f.colorIn.a = last.colorIn.a;
                f.colorIn.r = last.colorIn.r;
                f.colorIn.g = last.colorIn.g;
                f.colorIn.b = last.colorIn.b;

                result.Add(f);

            }

            return result;
        }
    


    }
}
