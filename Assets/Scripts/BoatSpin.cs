using UnityEngine;

public class BoatSpin : MonoBehaviour
{
    public float rotationSpeed = 180f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(-90, Time.time * rotationSpeed, 0);
    }
}
