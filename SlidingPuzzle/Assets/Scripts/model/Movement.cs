using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.model
{
    public class Movement
    {
        public int Number { get; private set; }
        public Tuple<int,int> Tuple { get; private set; }
        
        public Movement(int number, Tuple<int, int> tuple)
        {
            Number = number;
            Tuple = tuple;
        }

        public void SwapTuple(Tuple<int, int> tuple)
        {
            Tuple = tuple;
        }

    }
}
