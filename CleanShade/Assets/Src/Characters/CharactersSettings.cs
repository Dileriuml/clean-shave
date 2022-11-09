using System;
using UnityEngine;

namespace Src.Characters
{
    [Serializable]
    public class CharactersSettings
    {
        [SerializeField]
        private Player playerSettings;
        
        [SerializeField]
        private Enemy enemySettings;

        public Player PlayerSettings => playerSettings;

        public Enemy EnemySettings => enemySettings;

        [Serializable]
        public class Player : UnitParameters
        {   
        }

        [Serializable]
        public class Enemy : UnitParameters
        {
        }
    }
}