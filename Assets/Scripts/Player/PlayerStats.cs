using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    //Need to setup UI for this
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            health -= 1;
            other.gameObject.SetActive(false);
            if (health <= 0)
            {
                gameObject.SetActive(false);
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
