using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerInput.PlayerAbilitiesActions _playerAbilitiesActions;
    
    private PlayerMotors _motor;
    
    public ObjectPooling projectilePool;
    public float fireCooldown = 15f;
    private float _lastFireTime;
    private VisualElement Indicator;

    void Awake()
    {
        Indicator = GameObject.FindGameObjectWithTag("UI").GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Indicator");
        _playerInput = new PlayerInput();
        _playerAbilitiesActions = _playerInput.PlayerAbilities;
        
        _lastFireTime = -fireCooldown;

        _playerAbilitiesActions.Fire.started += ctx =>
        {
            Debug.Log($"GameState: {GameManager.Instance.CurrentState}, PlayerHealth: {GameManager.Instance.PlayerHealth}");
            if (GameManager.Instance.CurrentState == GameManager.GameState.Playing && GameManager.Instance.PlayerHealth < 3 
                && Time.time - _lastFireTime >= fireCooldown)
            {
                FireProjectile();
                _lastFireTime = Time.time;
            }
        };
    }

    void Update()
    {
        if (Time.time - _lastFireTime >= fireCooldown)
        {
            Indicator.visible = true;
        }
        else
        {
            Indicator.visible = false;
        }
    }
    
    private void FireProjectile()
    {
        GameObject projectile = projectilePool.GetPooledObject();
        if (projectile != null)
        {
            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;
            projectile.SetActive(true);
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
}
