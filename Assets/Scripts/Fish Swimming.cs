using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSwimming : MonoBehaviour
{
    public Vector3 velocity;
    private Vector3 startPos;
    private Rigidbody body;

    // Start is called before the first frame update
    void Awake()
    {
        startPos = transform.position;
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11) // layer == "Reset Position Zone"
            transform.position = startPos;
    }
}
