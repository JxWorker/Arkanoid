using UnityEngine;

public class Ball : MonoBehaviour
{
    public float xMax;
    public float yMax;

    public bool StickToPaddle { get; set; }
    public Vector3 StartPosition { get; set; }
    
    private Vector3 _velocity;
    private GameObject _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _velocity = new Vector3(0, yMax, 0);
        StartPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        StickToPaddle = true;
        
        _gameManager = GameObject.Find("GameManager_Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StickToPaddle = false;
            transform.Translate(Time.deltaTime * _velocity);
        }
        else if (!StickToPaddle)
        {
            transform.Translate(Time.deltaTime * _velocity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        float maxDistance;
        float distance;
        float normalizedDistance;
        
        switch (other.tag)
        {
            case "Paddle":
                maxDistance = other.transform.localScale.x * 0.5f + transform.localScale.x * 0.5f;
                distance = transform.position.x - other.transform.position.x;
                normalizedDistance = distance / maxDistance;
                _velocity = new Vector3(normalizedDistance * xMax, -_velocity.y, _velocity.z);
                break;
            case "Wall":
                _velocity = new Vector3(-_velocity.x, _velocity.y, _velocity.z);
                break;
            case "Wall_Top":
                _velocity = new Vector3(_velocity.x, -_velocity.y, _velocity.z);
                break;
            case "Deathpit":
                _gameManager.GetComponent<GameManager>().Lives -= 1;
                transform.position = StartPosition;
                StickToPaddle = true;
                break;
            case "Tile":
                maxDistance = other.transform.localScale.x * 0.5f + transform.localScale.x * 0.5f;
                distance = transform.position.x - other.transform.position.x;
                normalizedDistance = distance / maxDistance;
                _velocity = new Vector3(normalizedDistance * xMax, -_velocity.y, _velocity.z);
                break;
            case "Tile_Stone":
                maxDistance = other.transform.localScale.x * 0.5f + transform.localScale.x * 0.5f;
                distance = transform.position.x - other.transform.position.x;
                normalizedDistance = distance / maxDistance;
                _velocity = new Vector3(normalizedDistance * xMax, -_velocity.y, _velocity.z);
                break;
        }
    }
}