using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerInput.PlayerAbilitiesActions _playerAbilitiesActions;
    
    private PlayerMotors _motor;
    
    void Awake()
    {
        _playerInput = new PlayerInput();
        _playerAbilitiesActions = _playerInput.PlayerAbilities;
        _motor = GetComponent<PlayerMotors>();
        
        //AssignInputs();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void FixedUpdate()
    {
        _motor.ProcessMove(_playerAbilitiesActions.Movement.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        _playerAbilitiesActions.Enable();
    }

    private void OnDisable()
    {
        _playerAbilitiesActions.Disable();
    }
}