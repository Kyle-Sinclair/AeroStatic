using System;
using System.Collections.Generic;

namespace Utility {
    public class UtilityFunctions 
    {
        public static void Shuffle<T>(IList<T> array) {
            
            var _rng = new Random();

            for (int n = array.Count; n > 1;) {
                int k = _rng.Next(n);
                --n;
                (array[n], array[k]) = (array[k], array[n]);
            }
        }
    }
}
