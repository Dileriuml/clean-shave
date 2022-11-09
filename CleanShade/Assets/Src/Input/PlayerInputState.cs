using UnityEngine;

namespace Src.Input
{
    public class PlayerInputState : IPlayerInputState
    {
        public const float MoveEpsilon = 1e-4f;
        
        public Vector3 MoveVector { get; set; }
        
        public bool IsMoving => MoveVector.magnitude > MoveEpsilon;
        
        public bool IsFiring { get; set; }
    }
}