using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.gameObject.name);
        // Check if the enemy hits the boundary on the left (assuming it's a trigger)
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false); // Destroy the enemy on collision with the boundary
        }
    }
}
