using UnityEngine;

namespace Src.Input
{
    public interface IMouseRaycastSettings
    {
        LayerMask AimRaycastingMask { get; }
    }
}