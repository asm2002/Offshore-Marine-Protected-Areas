using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruvRespawner : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject bruv;
    public int respawnThreshold = 0;
    public int waterNoiseThreshold = 16;
    // Start is called before the first frame update
    public DavitController davitController;
    public AudioSource waterSound;
    
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
        if(bruv.transform.position.y <= waterNoiseThreshold && !waterSound.isPlaying)
        {
            if(bruv.activeSelf)
            {
                waterSound.volume = Random.Range(0.5f, 1.0f);
                waterSound.pitch = Random.Range(0.8f, 1.2f);
                waterSound.Play();
            }
        }
        if (bruv.transform.position.y <= respawnThreshold)
        {

            bruv.transform.position = respawnPoint.position;

            bruv.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }
    }

}
