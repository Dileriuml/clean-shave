using System;

namespace Src.Characters.Player
{
    [Serializable]
    public class PlayerState
    {
        public bool IsDead; // { get; set; }

        public bool IsMoving; // { get; set; }

        public bool IsFiring; // { get; set; }
    }
}