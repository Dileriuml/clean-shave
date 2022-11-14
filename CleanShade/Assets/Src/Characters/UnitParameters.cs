using System;
using UnityEngine;

namespace Src.Characters
{
    [Serializable]
    public class UnitParameters
    {
        [SerializeField]
        private float moveSpeed;

        [SerializeField] 
        private float moveAnimtionScale;
        
        public float MoveSpeed => moveSpeed;

        public float MoveAnimtionScale => moveSpeed;
    }
}