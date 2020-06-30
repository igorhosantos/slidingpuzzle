using System;
using System.Collections.Generic;
using Assets.Scripts.model;

namespace Assets.Scripts.engine
{
    public class BoardEngine
    {
        public List<Movement> Movements { get; private set; }

        private const int TOTAL_LINES = 3;
        private const int TOTAL_COLUMN = 3;

        public BoardEngine()
        {
            Movements = new List<Movement>();
            List<int> bucket = ValidNumberBucket.Generate();//new List<int>{1,2,3,4,5,6,7,8,-1};

            int count = 0;
            for (int i = 0; i < TOTAL_LINES; i++)
            {
                for (int j = 0; j < TOTAL_COLUMN; j++)
                {
                    Movements.Add(new Movement(bucket[count], new Tuple<int, int>(i, j)));
                    count++;
                }
            }
        }

        private Movement GetMovementPerTuple(Tuple<int, int> currentTuple)
        {
            return Movements.Find(m => m.Tuple.Item1 == currentTuple.Item1 && m.Tuple.Item2 == currentTuple.Item2);
        }

        public Movement ValidateMovement(Movement currentMovement)
        {
            Movement movement = FindEmptySpace(currentMovement.Tuple);
            if (movement!=null)
            {
                var validMovement = new Movement(currentMovement.Number, movement.Tuple);
                UpdateMovementList(currentMovement,movement);
                return validMovement;
            }

            return null;
        }

        private void UpdateMovementList(Movement current, Movement target)
        {
            target.SwapTuple(current.Tuple);
        }

        private Movement FindEmptySpace(Tuple<int,int> currentTuple)
        {
            //left
            Movement left = GetMovementPerTuple(new Tuple<int, int>(currentTuple.Item1 - 1, currentTuple.Item2));
            if (left != null && left.Number == 0)
            {
                return left;
            }

            Movement right = GetMovementPerTuple(new Tuple<int, int>(currentTuple.Item1 + 1, currentTuple.Item2));
            if (right != null && right.Number == 0)
            {
                return right;
            }

            Movement up = GetMovementPerTuple(new Tuple<int, int>(currentTuple.Item1, currentTuple.Item2-1));
            if (up != null && up.Number == 0)
            {
                return up;
            }

            Movement down = GetMovementPerTuple(new Tuple<int, int>(currentTuple.Item1, currentTuple.Item2+1));
            if (down != null && down.Number == 0)
            {
                return down;
            }


            return null;
        }

        

    }
}
