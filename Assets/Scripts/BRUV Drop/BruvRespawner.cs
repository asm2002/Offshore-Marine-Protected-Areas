using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruvRespawner : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject bruv;
    public int respawnThreshold = 0;
    // Start is called before the first frame update
    
    void Start()
    {
        if (respawnPoint == null)
        {
            respawnPoint = GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bruv.transform.position.y <= respawnThreshold)
        {
            Debug.Log("derp");

            bruv.transform.position = respawnPoint.position;

            bruv.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }
    }

}
