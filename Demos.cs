using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
u

namespace Demos
{
    class matrix
    {
        static Random rnd = new Random();

        public static void matrixdemo()
        {
            while(true)
            {
                Console.Write(rnd.Next());
                if(Sys.KeyboardManager.AltPressed)
                {
                    break;
                }
            }
        }
    }
}