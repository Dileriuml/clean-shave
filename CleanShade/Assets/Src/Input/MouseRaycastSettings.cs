using System;
using UnityEngine;

namespace Src.Input
{
    [Serializable]
    public class MouseRaycastSettings : IMouseRaycastSettings
    {
        [SerializeField]
        private LayerMask aimRaycastingMask;

        public LayerMask AimRaycastingMask => aimRaycastingMask;
    }
}