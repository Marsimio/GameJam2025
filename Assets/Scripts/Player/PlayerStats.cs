using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStats : MonoBehaviour
{
    public GameUI gameUI;

    public void Start()
    {
        GameManager.Instance.SetPlayerHealth(transform.childCount);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (transform.childCount > 0)
            {
                var child = transform.GetChild(0);
                Destroy(child.gameObject);
            }

            GameManager.Instance.DecreasePlayerHealth();
            other.gameObject.SetActive(false);

            if (transform.childCount == 0)
            {
                GameManager.Instance.ChangeState(GameManager.GameState.GameOver);
                StartCoroutine(WaitThenLoadScene());
            }
        }
    }
    IEnumerator WaitThenLoadScene()
    {
        gameObject.transform.position = new Vector3(-100, -100, 0);
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        
    }
}