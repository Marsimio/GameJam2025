using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    private bool isDisabled;
    
    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    
    public void DisableEnemy()
    {
        isDisabled = true;
    }
    private void OnBecameInvisible()
    {
        if (isDisabled) return;
        gameObject.SetActive(false);
    }
    
    
}
