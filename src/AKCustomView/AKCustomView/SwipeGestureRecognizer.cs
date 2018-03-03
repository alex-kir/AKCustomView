using System.Linq;

namespace AK
{
    public class SwipeGestureRecognizer : GestureRecognizer
    {
        readonly PointF swipeShift;
        readonly float swipeShiftRadius;
        readonly float swipeOutRadius;

        public SwipeGestureRecognizer(float x, float y, float radius)
        {
            this.swipeShift = new PointF(x, y); this.swipeShiftRadius = radius;
            this.swipeOutRadius = swipeShift.Distance(new PointF(0, 0));
        }

        protected override System.Collections.Generic.IEnumerable<bool> NextYield()
        {
            if (touches.Length == 1 && touches.All(it => it.IsDown))
            {
                var start = touches.First().XY;
                yield return false;
                while (touches.Length == 1)
                {
                    var pos = touches[0].XY;
                    if (pos.Distance(start) > swipeOutRadius)
                    {
                        yield return new PointF(start.X + swipeShift.X, start.Y + swipeShift.Y).Distance(pos) < swipeShiftRadius;
                        break;
                    }
                    yield return false;
                }
            }
        }
    }
}
