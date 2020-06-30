using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.engine
{
    public static class ValidNumberBucket
    {
        private static readonly List<int> defaultNumbers = new List<int>{1,2,3,4,5,6,7,8,0};
        private static int InvertedCount(List<int> arr)
        {
            var count = 0;
            for (int i = 0; i < arr.Count - 1; i++)
            for (int j = i + 1; j < arr.Count; j++)

                if (arr[j] > 0 && arr[i] > 0 && arr[i] > arr[j])
                    count++;

            return count;
        }

        private static bool IsSolvable(List<int> list) => (InvertedCount(list) % 2 == 0);
        
        public static List<int> Generate()
        {
            List<int> currentNumbers;
            do
            {
                currentNumbers = defaultNumbers.OrderBy(x=>Guid.NewGuid()).ToList();

            } while (!IsSolvable(currentNumbers));

            return currentNumbers;
        }
    }
}
