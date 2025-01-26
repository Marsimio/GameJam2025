using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBack : MonoBehaviour
{
    private void Start()
    {
        // Add a click listener if this script is attached to a UI Button
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnBackToMainMenu);
        }
    }

    private void OnBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}