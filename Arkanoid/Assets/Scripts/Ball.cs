using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Serialization;

public class Ball : MonoBehaviour
{
    public float xMax;
    public float yMax;
    public GameObject Paddle;

    private Vector3 _velocity;
    private bool _stickToPaddle;
    private Vector3 _startPosition;
    private GameObject _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _velocity = new Vector3(0, yMax, 0);
        _startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _stickToPaddle = true;
        
        _gameManager = GameObject.Find("GameManager_Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _stickToPaddle = false;
            transform.Translate(Time.deltaTime * _velocity);
        }
        else if (!_stickToPaddle)
        {
            transform.Translate(Time.deltaTime * _velocity);
        }
        else if (_stickToPaddle)
        {
            //TODO: The ball doesn't move with the paddle
            transform.position = new Vector3(Paddle.transform.position.x, _startPosition.y,
                Paddle.transform.position.z);
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
                transform.position = _startPosition;
                _stickToPaddle = true;
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
            // case :
            //     break;
        }
    }
}