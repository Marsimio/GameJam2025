using System;
using System.Collections;
using UnityEngine;

public class PlayerMotors : MonoBehaviour
{
    private Vector3 playerVelocity;
    Vector2 moveDirection = Vector2.zero;
    public float speed = 5.0f;
    public float rightBound = 1f;
    private float screenLeftLimit;
    private float screenRightLimit;
    private float screenTopLimit;
    private float screenBottomLimit;
    private float originalSpeed;
    public float speedPenaltyPerHealthLoss = 0.20f;
    
    private void Awake()
    {
        originalSpeed = speed;
        screenLeftLimit = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + 1f;
        screenRightLimit = Camera.main.ViewportToWorldPoint(new Vector3(rightBound, 0, 0)).x;
        screenTopLimit = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 1f;
    }

    public void ProcessMove(Vector2 input)
    {
        moveDirection.x = input.x;
        moveDirection.y = input.y;
        
        int currentHealth = Mathf.Clamp(GameManager.Instance.PlayerHealth, 0, 3);
        float speedModifier = 1f - (speedPenaltyPerHealthLoss * (currentHealth));

        float adjustedSpeed = (moveDirection.y < 0) ? speed * 1.3f : speed;
        playerVelocity = (adjustedSpeed * speedModifier) * moveDirection;
        Vector3 newPosition = transform.position + (playerVelocity * Time.deltaTime);

        newPosition.x = Mathf.Clamp(newPosition.x, screenLeftLimit, screenRightLimit);
        newPosition.y = Mathf.Min(newPosition.y, screenTopLimit);

        transform.position = newPosition;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cloud"))
        {
            var cloud = collision.GetComponent<Cloud>();
            if (cloud != null)
            {
                speed *= cloud.slowSpeed;
            }
        }
        
        if (collision.CompareTag("SpeedPowerUp"))
        {
            var speedPowerUp = collision.GetComponent<PowerUps>();
            if (speedPowerUp != null)
            {
                speed *= speedPowerUp.speedPowerUpAmount;
                
                StartCoroutine(ResetSpeedAfterDelay(3f));
                speedPowerUp.gameObject.SetActive(false);
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
    
    IEnumerator ResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        speed = originalSpeed;
        print("Speed reset to default: " + speed);
    }
}
