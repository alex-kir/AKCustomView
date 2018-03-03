using System;
using System.Collections.Generic;

namespace AK
{
    public class LongTapGestureRecognizer : GestureRecognizer
    {
        protected override IEnumerable<bool> NextYield()
        {
            if (touches.Length == 1 && touches[0].IsDown) {
                var pos = touches[0].XY;
                var time = DateTime.Now;
                yield return false;

                while (touches.Length == 1) {
                    if (touches[0].IsCancelled)
                    {
                        yield return false;
                        yield break;
                    }
                    else if (touches[0].IsUp) {
                        yield return false;
                        yield break;
                    }
                    else {
                        var t = (DateTime.Now - time).TotalSeconds;
                        if (t < 0.7)
                        {
                            yield return false;
                        }
                        else
                        {
                            yield return true;

                            while (touches[0].IsUp)
                                yield return false;
                            yield break;
                        }
                    }
                }
            }
        }
    }
}

