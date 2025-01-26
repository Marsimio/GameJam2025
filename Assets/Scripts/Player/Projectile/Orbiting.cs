using UnityEngine;

public class Orbiting : MonoBehaviour
{
    public float bounceSpeed = 2f;
    public float bounceHeight = 0.5f; 

    private float bounceTime;

    void Update()
    {
        if (transform.parent != null)
        {
            transform.RotateAround(transform.position, Vector3.up, 50 * Time.deltaTime);

            bounceTime += Time.deltaTime * bounceSpeed;
            float newY = Mathf.Sin(bounceTime) * bounceHeight;

            Vector3 parentPosition = transform.parent.position;
            transform.position = new Vector3(parentPosition.x, parentPosition.y + newY, parentPosition.z);
        }
    }
}
