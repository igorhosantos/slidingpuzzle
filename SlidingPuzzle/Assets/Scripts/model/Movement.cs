using System;

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
