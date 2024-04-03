using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Sprite[] healthBallSprites = new Sprite[3];
    public Image[] healthBallImages = new Image[3];
    public TextMeshProUGUI highScoreNumber_InGame;
    public TextMeshProUGUI highScoreNumber_GameEnd;
    public Canvas gameEndScreen;
    public TextMeshProUGUI titleGameEndScreen;
    public Sprite[] powerUpList = new Sprite[3];
    public Image currentPowerUp;

    public GameObject paddle;
    
    public int Score { get; set; }
    public int Lives { get; set; } = 3;

    void Start()
    {
        gameEndScreen.enabled = false;
        
        foreach (Image image in healthBallImages)
        {
            image.sprite = healthBallSprites[2];
        }
    }

    void Update()
    {
        highScoreNumber_InGame.text = string.Format("{0:N}", Score.ToString());

        switch (paddle.GetComponent<Paddle>().CurrentPowerUp)
        {
            case "":
                currentPowerUp.enabled = false;
                break;
            case "PowerUp_Longer-Paddle":
                currentPowerUp.enabled = true;
                currentPowerUp.sprite = powerUpList[0];
                break;
            case "PowerUp_Speed-Up-Paddle":
                currentPowerUp.enabled = true;
                currentPowerUp.sprite = powerUpList[1];
                break;
            case "PowerUp_Sticky-Paddle":
                currentPowerUp.enabled = true;
                currentPowerUp.sprite = powerUpList[2];
                break;
        }

        switch (Lives)
        {
            case 3:
                foreach (Image image in healthBallImages)
                {
                    image.sprite = healthBallSprites[2];
                }

                break;
            case 2:
                healthBallImages[0].enabled = false;
                healthBallImages[1].sprite = healthBallSprites[1];
                healthBallImages[2].sprite = healthBallSprites[1];
                break;
            case 1:
                healthBallImages[0].enabled = false;
                healthBallImages[1].enabled = false;
                healthBallImages[2].sprite = healthBallSprites[0];
                break;
            case 0:
                healthBallImages[0].enabled = false;
                healthBallImages[1].enabled = false;
                healthBallImages[2].enabled = false;
                break;
            case -1:
                highScoreNumber_GameEnd.text = string.Format("{0:N}", Score.ToString());
                titleGameEndScreen.text = "Game Over";
                gameEndScreen.enabled = true;
                break;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Scenes/GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}