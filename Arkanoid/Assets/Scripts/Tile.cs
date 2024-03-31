using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool hasPowerUp;
    public bool isStoneTile;
    public GameObject[] powerUps = new GameObject[3];
    public Material[] colors = new Material[5];
    public Material[] stoneSteps = new Material[3];

    private GameObject _gameManager;
    private int lives;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager_Canvas");

        if (isStoneTile)
        {
            lives = 3;
            gameObject.GetComponent<MeshRenderer>().material = stoneSteps[0];
        }
        else
        {
            lives = 1;
            var random = UnityEngine.Random.Range(0, 5);
            gameObject.GetComponent<MeshRenderer>().material = colors[random];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            lives -= 1;

            switch (lives)
            {
                case 2:
                    gameObject.GetComponent<MeshRenderer>().material = stoneSteps[1];
                    break;
                case 1:
                    gameObject.GetComponent<MeshRenderer>().material = stoneSteps[2];
                    break;
                default:
                    _gameManager.GetComponent<GameManager>().Score += 10;
                    DropPowerUp();
                    Destroy(this.gameObject);
                    break;
            }
        }
    }

    private void DropPowerUp()
    {
        if (hasPowerUp)
        {
            var random = UnityEngine.Random.Range(0, 3);
            var powerUpInstantiate = Instantiate<GameObject>(powerUps[random]);
            powerUpInstantiate.transform.position = transform.position;
        }
    }
}