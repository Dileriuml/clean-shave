using UnityEngine;

namespace Src.Input
{
    public interface IPlayerInputState
    {
        public bool IsFiring { get; set; }

        public Vector3 MoveVector { get; set; }
        
        public Vector3 AimLocation { get; set; }
    }
}