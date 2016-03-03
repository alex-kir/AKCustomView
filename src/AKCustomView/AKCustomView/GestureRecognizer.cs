using System;

using Xamarin.Forms;
using System.Collections.Generic;

namespace AK
{
    public abstract class GestureRecognizer
    {
        protected Touch[] touches { get; private set; }
        IEnumerator<bool> next = null;

        public bool Next(Touch[] touches)
        {
            this.touches = touches;
            if (next == null)
                next = NextYield().GetEnumerator();
            if (next.MoveNext()) {
                return next.Current;
            }
            else {
                next = NextYield().GetEnumerator();
                if (next.MoveNext()) {
                    return next.Current;
                }
                else {
                    return false;
                }
            }
        }

        protected abstract IEnumerable<bool> NextYield();
    }



//    class SingleMovePattern : GestureRecognizer
//    {
//        public SizeF Shift;
//
//        protected override IEnumerable<bool> NextYield()
//        {
//            if (touches.Length == 1 && touches[0].IsDown) {
//                yield return false;
//                while (touches.Length == 1) {
//                    if (touches[0].IsUp) {
//                        yield break;
//                    }
//                    else {
//                        Shift = new SizeF(touches[0].XY) - new SizeF(touches[0].PrevXY);
//                        yield return true;
//                    }
//                }
//            }
//        }
//    }
//
//    class PinchZoomPattern : GestureRecognizer
//    {
//        public float Scale;
//
//        protected override IEnumerable<bool> NextYield()
//        {
//            if (touches.Length == 1 && touches[0].IsDown) {
//                yield return false;
//
//                while (touches.Length == 1) {
//                    yield return false;
//                }
//
//                while (touches.Length == 2) {
//                    if (touches[0].IsDownOrUp || touches[1].IsDownOrUp) {
//                        yield return false;
//                    }
//                    else {
//                        float prevDist = touches[0].PrevXY.Distance(touches[1].PrevXY);
//                        float dist = touches[0].XY.Distance(touches[1].XY);
//                        if (prevDist > 0) {
//                            Scale = dist / prevDist;
//                            yield return true;
//                        }
//                        else {
//                            yield return false;
//                        }
//                    }
//                }
//            }
//        }
//    }

}


