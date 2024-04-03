using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float yMax;
    private Vector3 _velocity;
    // Start is called before the first frame update
    void Start()
    {
        _velocity = new Vector3(0, -yMax, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * _velocity);
    }
}
