using UnityEngine;

public class DroneEnemy : Enemy
{
    public float flySpeed;
    private void FixedUpdate()
    {
        float newY = Mathf.Sin(Time.time * 2) * flySpeed;
        transform.Translate(new Vector2(-speed * Time.deltaTime, newY - transform.position.y));
    }
}
