using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    
    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
    
    
}
