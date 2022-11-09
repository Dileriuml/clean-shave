using UnityEngine;
using Zenject;

namespace Src.Characters.Player
{
    public class PlayerFacade : MonoBehaviour
    {
        private PlayerModel model;
        //PlayerDamageHandler _hitHandler;

        [Inject]
        public void Construct(PlayerModel model)//, PlayerDamageHandler hitHandler)
        {
            this.model = model;
            //_hitHandler = hitHandler;
        }

        public bool IsDead => model.IsDead;

        public Vector3 Position => model.Position;

        public Quaternion Rotation => model.Rotation;

        public void TakeDamage(Vector3 moveDirection)
        {
            //_hitHandler.TakeDamage(moveDirection);
        }
    }
}