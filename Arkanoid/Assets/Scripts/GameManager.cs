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

    public int Score { get; set; }

    public int Lives { get; set; } = 3;

    // Start is called before the first frame update
    void Start()
    {
        gameEndScreen.enabled = false;
        
        foreach (Image image in healthBallImages)
        {
            image.sprite = healthBallSprites[2];
        }
    }

    // Update is called once per frame
    void Update()
    {
        highScoreNumber_InGame.text = string.Format("{0:N}", Score.ToString());

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