using System;
using System.Collections.Generic;
using System.Linq;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.engine
{
    public static class ValidNumberBucket
    {
        private static readonly List<int> defaultNumbers = new List<int>{1,2,3,4,5,6,7,8,0};
        private static int InvertedCount(int[,] arr)
        {
            var count = 0;
            for (int i = 0; i < 3 - 1; i++)
            for (int j = i + 1; j < 3; j++)

                if (arr[j, i] > 0 && arr[j, i] > arr[i, j])
                    count++;

            return count;
        }

        private static bool IsSolvable(int[,] puzzle) => (InvertedCount(puzzle) % 2 == 0);
        
        public static List<int> Generate()
        {
            List<int> currentNumbers;
            int[,] puzzle = new int[3, 3];

            do
            {
                string str = "";
                currentNumbers = defaultNumbers.OrderBy(x=>Guid.NewGuid()).ToList();
                int count = 0;
                for (int i = 0; i < puzzle.GetLength(0); i++)
                {
                    for (int j = 0; j < puzzle.GetLength(1); j++)
                    {
                        puzzle[i, j] = currentNumbers[count];
                        str += puzzle[i, j] + ",";
                        count++;
                    }
                }

                Debug.LogWarning("Is Solvable ? "  + IsSolvable(puzzle) +  " |  " + str);

            } while (!IsSolvable(puzzle));

            return currentNumbers;
        }
    }
}
