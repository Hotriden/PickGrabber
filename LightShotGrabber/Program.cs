using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightShotGrabber
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();
            parser.ParseFromUrl();
            Console.ReadLine();
        }
    }
}
