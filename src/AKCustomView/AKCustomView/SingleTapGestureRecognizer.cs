using System;
using System.Collections.Generic;

namespace AK
{
    public class SingleTapGestureRecognizer : GestureRecognizer
    {
        protected override IEnumerable<bool> NextYield()
        {
            if (touches.Length == 1 && touches[0].IsDown) {
                var pos = touches[0].XY;
                var time = DateTime.Now;
                yield return false;

                while (touches.Length == 1) {
                    if (touches[0].IsUp) {
                        yield return (DateTime.Now - time).TotalSeconds < 0.3 && touches[0].XY.Distance(pos) < 10;
                        yield break;
                    }
                    else {
                        yield return false;
                    }
                }
            }
        }
    }
}

