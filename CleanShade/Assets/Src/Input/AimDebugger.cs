using UnityEngine;
using Zenject;

namespace Src.Input
{
    public class AimDebugger : MonoBehaviour
    {
        [Inject]
        private IPlayerInputState playerInputState;

        private void Update()
        {
            transform.position = playerInputState.AimLocation;
        }
    }
}