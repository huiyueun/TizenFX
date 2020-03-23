
using System.Collections.Generic;

namespace Tizen.NUI
{
    public interface IRotaryTouchController
    {
        void ProcessTouchEvent(RotarySelectorItem item);
        void ProcessMotionEvent(List<RotaryItemWrapper> wrapperList, RotarySelectorItem item);
    }
}