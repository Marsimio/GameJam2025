using UnityEngine;

public class StartingLevel : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject spawnerObject;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            StartCoroutine(MovePlayer(player));
        }
    }

    System.Collections.IEnumerator MovePlayer(GameObject player)
    {
        Vector3 startPos = player.transform.position;
        Vector3 endPos = new Vector3(startPos.x, 0, startPos.z);
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            player.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            yield return null;
        }

        player.transform.position = endPos;

        yield return new WaitForSeconds(1f);

        if (audioSource != null)
        {
            audioSource.Play();
        }

        if (spawnerObject != null)
        {
            spawnerObject.SetActive(true);
        }
    }
}
