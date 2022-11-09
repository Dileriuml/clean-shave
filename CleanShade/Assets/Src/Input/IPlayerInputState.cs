using UnityEngine;

namespace Src.Input
{
    public interface IPlayerInputState
    {
        public bool IsMoving { get; }

        public bool IsFiring { get; set; }

        public Vector3 MoveVector { get; set; }
    }
}