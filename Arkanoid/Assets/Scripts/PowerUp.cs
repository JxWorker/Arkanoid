using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float yMax;
    private Vector3 _velocity;
    
    void Start()
    {
        _velocity = new Vector3(0, -yMax, 0);
    }

    void Update()
    {
        transform.Translate(Time.deltaTime * _velocity);
    }
}
