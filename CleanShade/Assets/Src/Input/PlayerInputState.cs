using UnityEngine;

namespace Src.Input
{
    public class PlayerInputState : IPlayerInputState
    {
        public Vector3 MoveVector { get; set; }
        
        public Vector3 AimLocation { get; set; }
        
        public bool IsFiring { get; set; }
    }
}