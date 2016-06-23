using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeCCtor;
using VeCCtor.Extra;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Figure> figures = new List<Figure>() { new Ellipse(), new Rectangle(), new Line() };



             string s =Serializer.Serialize(figures);

             Console.WriteLine(s);

             Serializer.Deserialize(s);

             Console.ReadLine();
        }
    }
}
