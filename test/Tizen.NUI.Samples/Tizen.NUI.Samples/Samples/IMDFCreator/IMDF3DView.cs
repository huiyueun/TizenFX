using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.Samples
{
    // Make custom view to ignore Layout features so we can use Depth information
    public class IMDF3DView : CustomView
    {
        // Default View Enabled SizeNegotiations, and It is Breakdown the depth values.
        // So we need to create CustomView which CustomViewBehavior.DisableSizeNegotiation.
        public IMDF3DView() : base("Custom3DView", CustomViewBehaviour.DisableSizeNegotiation)
        {
            PositionUsesPivotPoint = true;
            ParentOrigin = Position.ParentOriginCenter;
            PivotPoint = Position.PivotPointCenter;
        }
    }
}
