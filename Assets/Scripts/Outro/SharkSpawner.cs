using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spawn an amount of sharks on the map randomly
public class SharkSpawner : MonoBehaviour {
    
    public GameObject mapShark;

    // Update is called once per frame
    void Start() {
        for (int i = 0; i < 10; i++) {
            Vector3 randomSpawnPos = new Vector3(483.4f, Random.Range(22.0f, 21.0f), Random.Range(364.8f, 365.6f));
            Instantiate(mapShark, randomSpawnPos, Quaternion.identity).transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
    }
}
