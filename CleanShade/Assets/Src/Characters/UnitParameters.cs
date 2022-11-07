using System;
using UnityEngine;

namespace Src.Characters
{
    [Serializable]
    public class UnitParameters
    {
        [SerializeField]
        private float speed;

        public float Speed => speed;
    }
}