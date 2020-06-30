using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts.model;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.engine
{
    public class BoardEngine
    {
        public List<Movement> Movements { get; private set; }

        private const int TOTAL_PER_LINE = 3;

        private static readonly List<int> solvedProblem = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 0 };

        public BoardEngine()
        {
            Movements = new List<Movement>();

            //easy way to debugging list
            ////new List<int> { 1, 2, 3, 4, 5, 6, 0, 7, 8 };////
            List<int> bucket = ValidNumberBucket.Generate();

            int line = 0;
            int column = 0;

            for (int i = 0; i < bucket.Count; i++)
            {
                Movements.Add(new Movement(i,bucket[i], new Tuple<int, int>(line, column)));

                line++;
                if (line == TOTAL_PER_LINE)
                {
                    column++;
                    line = 0;
                }
            }
        }

        public void UpdateBoard(Movement current, Movement target)
        {
            Movement temp = current;
            Movements[GetIndexPosition(current)] = target;
            Movements[GetIndexPosition(target)] = temp;
        }

        private Movement GetMovementPerTuple(Tuple<int, int> currentTuple)
        {
            return Movements.Find(m => m.Tuple.Item1 == currentTuple.Item1 && m.Tuple.Item2 == currentTuple.Item2);
        }

        private int GetIndexPosition(Movement movement)
        {
            for (int i = 0; i < Movements.Count; i++)
            {
                if (Movements[i].Id == movement.Id)
                {
                    return i;
                }
            }

            return -1;
        }

        public bool FinishGame()
        {
            for (int i = 0; i < Movements.Count; i++)
            {
                if (solvedProblem[i] != Movements[i].Number)
                {
                    return false;
                }
            }

            return true;
        }

        public Tuple<int,int> ValidateMovement(Movement currentMovement)
        {
            Movement movement = FindEmptySpace(currentMovement.Tuple);
            if (movement != null)
            {
                Tuple<int,int> tp = movement.Tuple;
                movement.SwapTuple(currentMovement.Tuple);
                UpdateBoard(currentMovement, movement);

                return tp;
            }

            return null;
        }

        private Movement FindEmptySpace(Tuple<int,int> currentTuple)
        {
            Movement left = GetMovementPerTuple(new Tuple<int, int>(currentTuple.Item1 - 1, currentTuple.Item2));
            if (left != null && left.Number == 0) return left;
           
            Movement right = GetMovementPerTuple(new Tuple<int, int>(currentTuple.Item1 + 1, currentTuple.Item2));
            if (right != null && right.Number == 0) return right;
            

            Movement up = GetMovementPerTuple(new Tuple<int, int>(currentTuple.Item1, currentTuple.Item2-1));
            if (up != null && up.Number == 0) return up;
           
            Movement down = GetMovementPerTuple(new Tuple<int, int>(currentTuple.Item1, currentTuple.Item2+1));
            if (down != null && down.Number == 0) return down;
            
            return null;
        }

    }
}
