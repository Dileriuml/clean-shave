using UnityEngine;
using Zenject;

namespace Src.Characters.Player
{
    public class PlayerFacade : MonoBehaviour
    {
        [SerializeField]
        private PlayerModel model;
        
        [SerializeField]
        private PlayerState playerState;
        
        //PlayerDamageHandler _hitHandler;

        [Inject]
        public void Construct(
            PlayerModel model, 
            PlayerState playerState)//, PlayerDamageHandler hitHandler)
        {
            this.model = model;
            this.playerState = playerState;
            //_hitHandler = hitHandler;
        }

        public bool IsDead => playerState.IsDead;

        public Vector3 Position => model.Position;

        public Quaternion Rotation => model.Rotation;

        public void TakeDamage(Vector3 moveDirection)
        {
            //_hitHandler.TakeDamage(moveDirection);
        }
    }
}