using System;
using System.Collections;
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
        
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void FixedUpdate()
    {
        Vector2 input = _playerAbilitiesActions.Movement.ReadValue<Vector2>();
        if (input != Vector2.zero)
        {
            _motor.ProcessMove(input);
        }
        else
        {
            _motor.ProcessMove(new Vector2(0, -0.2f));
        }
}
    private void OnEnable()
    {
        _playerAbilitiesActions.Enable();
    }

    private void OnDisable()
    {
        _playerAbilitiesActions.Disable();
    }

    private void OnBecameInvisible()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.GameOver);

        StartCoroutine(WaitThenLoadScene());
    }
    IEnumerator WaitThenLoadScene()
    {
        gameObject.transform.position = new Vector3(-100, -100, 0);
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}