using UnityEngine;

public class BubbleProjectile : MonoBehaviour
{
    public GameObject bubble;
    public float bubbleSpeed = 5f;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * bubbleSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.CompareTag("Enemy") && player != null)
    {
        GameManager.Instance.IncreasePlayerHealth();
        GameObject orbitingBubble = new GameObject("OrbitingBubble");

        orbitingBubble.transform.SetParent(player.transform);
        orbitingBubble.transform.position = Vector3.zero;

        

        GameObject enemyCopy = Instantiate(other.gameObject, other.transform.position, other.transform.rotation);
        other.gameObject.SetActive(false);
        var enemyScript = enemyCopy.gameObject.GetComponent<Enemy>();
        if (enemyScript)
        {
            enemyScript.DisableEnemy();
            enemyScript.enabled = false;
        } 
        enemyCopy.gameObject.GetComponent<Collider2D>().enabled = false;

        enemyCopy.transform.SetParent(orbitingBubble.transform);
        enemyCopy.transform.position = new Vector3(2f, 0f, 0f);

        GameObject instantiatedBubble = Instantiate(bubble, enemyCopy.transform.position, Quaternion.identity);
        instantiatedBubble.transform.SetParent(orbitingBubble.transform);

        enemyCopy.tag = "Untagged";

        orbitingBubble.AddComponent<Orbiting>();

        gameObject.SetActive(false);
    }
}


    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
