using System;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed;
    public GameObject playArea;
    public GameObject ball;

    private bool _isTimerRunning = false;
    private float _timeRemaining = 0;
    // private bool changePowerUp = false;
    public string CurrentPowerUp { get; set; } = "";

    void Update()
    {
        float boader = playArea.transform.localScale.x * 10 * 0.5f - transform.localScale.x;
        float direction = Input.GetAxis("Horizontal");
        float xNew = transform.position.x + direction * speed * Time.deltaTime;
        float xClamped = Mathf.Clamp(xNew, -boader, boader);
        transform.position = new Vector3(xClamped, transform.position.y, transform.position.z);

        if (_isTimerRunning)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                ResetPowerUps();
            }
        }

        if (CurrentPowerUp.Equals("PowerUp_Sticky-Paddle") &&
            (Convert.ToInt32(ball.GetComponent<Ball>().StartPosition.y) ==
             Convert.ToInt32(ball.transform.position.y)) &&
            (Convert.ToInt32(ball.transform.position.x) == Convert.ToInt32(transform.position.x) ||
             Convert.ToInt32(ball.transform.position.x) == Convert.ToInt32(transform.position.x + 1) ||
             Convert.ToInt32(ball.transform.position.x) == Convert.ToInt32(transform.position.x - 1)))
        {
            ball.GetComponent<Ball>().StickToPaddle = true;
            CurrentPowerUp = "";
        }

        if (ball.GetComponent<Ball>().StickToPaddle)
        {
            ball.transform.position = new Vector3(xClamped, ball.GetComponent<Ball>().StartPosition.y,
                ball.GetComponent<Ball>().StartPosition.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            if (other.gameObject.name.Contains("PowerUp_LongPaddle"))
            {
                PowerUpLongPaddle();
            }
            else if (other.gameObject.name.Contains("PowerUp_SpeedUpPaddle"))
            {
                PowerUpSpeedUpPaddle();
            }
            else if (other.gameObject.name.Contains("PowerUp_StickyPaddle"))
            {
                ResetPowerUps();
                CurrentPowerUp = "PowerUp_Sticky-Paddle";
            }

            Destroy(other.gameObject);
        }
    }

    private void PowerUpLongPaddle()
    {
        if (CurrentPowerUp.Equals("PowerUp_Longer-Paddle"))
        {
            _timeRemaining += 15;
            return;
        }

        ResetPowerUps();

        CurrentPowerUp = "PowerUp_Longer-Paddle";

        // if (_isTimerRunning)
        // {
        //     transform.localScale = new Vector3(1, 1, 1);
        //     _isTimerRunning = false;
        // }
        // else
        // {
            _timeRemaining = 15;
            transform.localScale = new Vector3(1, 2, 1);
            _isTimerRunning = true;
        // }
    }

    private void PowerUpSpeedUpPaddle()
    {
        if (CurrentPowerUp.Equals("PowerUp_Speed-Up-Paddle"))
        {
            _timeRemaining += 15;
            return;
        }

        ResetPowerUps();

        CurrentPowerUp = "PowerUp_Speed-Up-Paddle";

        // if (_isTimerRunning)
        // {
        //     speed = 5;
        //     _isTimerRunning = false;
        // }
        // else
        // {
            _timeRemaining = 15;
            speed = 10;
            _isTimerRunning = true;
        // }
    }

    private void ResetPowerUps()
    {
        CurrentPowerUp = "";
        transform.localScale = new Vector3(1, 1, 1);
        speed = 5;
        _isTimerRunning = false;
        _timeRemaining = 0;
    }
}