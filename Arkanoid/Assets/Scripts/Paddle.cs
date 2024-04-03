using System;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed;
    public GameObject playArea;
    public GameObject ball;

    private bool isTimerRunning = false;
    private float timeRemaining;
    public string CurrentPowerUp { get; set; } = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float boader = playArea.transform.localScale.x * 10 * 0.5f - transform.localScale.x;
        float direction = Input.GetAxis("Horizontal");
        float xNew = transform.position.x + direction * speed * Time.deltaTime;
        float xClamped = Mathf.Clamp(xNew, -boader, boader);
        transform.position = new Vector3(xClamped, transform.position.y, transform.position.z);

        if (ball.GetComponent<Ball>().StickToPaddle /*&& ball.GetComponent<Ball>().StartPosition.y == ball.transform.position.y*/)
        {
            ball.transform.position = new Vector3(xClamped, ball.GetComponent<Ball>().StartPosition.y,
                ball.GetComponent<Ball>().StartPosition.z);
        }

        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                switch (CurrentPowerUp)
                {
                    case "PowerUp_Longer-Paddle":
                        CurrentPowerUp = "";
                        PowerUpLongPaddle();
                        break;
                    case "PowerUp_Speed-Up-Paddle":
                        CurrentPowerUp = "";
                        PowerUpSpeedUpPaddle();
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            if (other.gameObject.name.Contains("PowerUp_LongPaddle"))
            {
                CurrentPowerUp = "PowerUp_Longer-Paddle";
                PowerUpLongPaddle();
            }
            else if (other.gameObject.name.Contains("PowerUp_SpeedUpPaddle"))
            {
                CurrentPowerUp = "PowerUp_Speed-Up-Paddle";
                PowerUpSpeedUpPaddle();
            }
            else if (other.gameObject.name.Contains("PowerUp_StickyPaddle"))
            {
                CurrentPowerUp = "PowerUp_Sticky-Paddle";
                PowerUpStickyPaddle();
            }
            
            Destroy(other.gameObject);
        }
    }

    private void PowerUpLongPaddle()
    {
        if (isTimerRunning)
        {
            transform.localScale = new Vector3(1,1,1);
            isTimerRunning = false;
        }
        else
        {
            timeRemaining = 60;
            transform.localScale = new Vector3(1,2,1);
            isTimerRunning = true;
        }
    }

    private void PowerUpStickyPaddle()
    {
        ball.GetComponent<Ball>().StickToPaddle = true;
    }

    private void PowerUpSpeedUpPaddle()
    {
        if (isTimerRunning)
        {
            speed = 5;
            isTimerRunning = false;
        }
        else
        {
            timeRemaining = 60;
            speed = 10;
            isTimerRunning = true;
        }
    }
}