using System;
using UnityEngine;

public class PlayerMotors : MonoBehaviour
{
    private Vector2 playerVelocity;
    Vector2 moveDirection = Vector2.zero;
    public float speed = 5.0f;
    public float rightBound = 0.2f;
    private float screenLeftLimit;
    private float screenRightLimit;
    private float screenTopLimit;
    private float screenBottomLimit;
    private float originalSpeed;

    // This is the shit that makes it so that the player cant leave the area, Joe if you can fix the padding values
    // if you think its going out
    private void Awake()
    {
        originalSpeed = speed;
        screenLeftLimit = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + 0.5f;
        screenRightLimit = Camera.main.ViewportToWorldPoint(new Vector3(rightBound, 0, 0)).x;
        screenTopLimit = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 0.5f;
        screenBottomLimit = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + 0.5f;
    }

    public void ProcessMove(Vector2 input)
    {
        moveDirection.x = input.x;
        moveDirection.y = input.y;

        playerVelocity = speed * moveDirection;

        Vector3 newPosition = transform.position + (Vector3)(playerVelocity * Time.deltaTime);

        newPosition.x = Mathf.Clamp(newPosition.x, screenLeftLimit, screenRightLimit);
        newPosition.y = Mathf.Clamp(newPosition.y, screenBottomLimit, screenTopLimit);

        transform.position = newPosition;
    }

    
    //Yes I put the cloud collide code here because I was having IsTrigger issues at the time
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cloud"))
        {
            var cloud = collision.GetComponent<Cloud>();
            if (cloud != null)
            {
                speed *= cloud.slowSpeed;
                print(speed);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cloud"))
        {
            var cloud = collision.GetComponent<Cloud>();
            if (cloud != null)
            {
                speed = originalSpeed;
            }
        }
    }
}
