using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed;
    public GameObject playArea;
    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float boader = playArea.transform.localScale.x * 10 * 0.5f - transform.localScale.x * 0.5f;
        float direction = Input.GetAxis("Horizontal");
        float xNew = transform.position.x + direction * speed * Time.deltaTime;
        float xClamped = Mathf.Clamp(xNew, -boader, boader);
        transform.position = new Vector3(xClamped, transform.position.y, transform.position.z);

        if (ball.GetComponent<Ball>().StickToPaddle)
        {
            ball.transform.position = new Vector3(xClamped, ball.GetComponent<Ball>().StartPosition.y,
                ball.GetComponent<Ball>().StartPosition.z);
        }
    }
}