using Src.Characters;
using UnityEngine;
using Zenject;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    [Inject]
    private IPlayerInput playerInput;
    
    [SerializeField]
    private UnitParameters stats;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        var moveVector = playerInput.GetMoveVector();
        Vector3 calculatedMovement = Vector3.forward * moveVector * (stats.Speed * Time.deltaTime);
        transform.position += calculatedMovement;
    }
}
