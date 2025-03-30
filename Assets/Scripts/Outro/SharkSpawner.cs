using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// spawn an amount of sharks on the map randomly
public class SharkSpawner : MonoBehaviour {
    
    [SerializeField] private GameObject mapShark;
    [SerializeField] private GameObject[] spawnAreas;
    public int taggedSharks;

    void Start() {
        for (int i = 0; i < taggedSharks; i++) {

            // select random spawn area
            int randomSpawnArea = Random.Range(0, spawnAreas.Length);
            Bounds spawnArea = spawnAreas[randomSpawnArea].GetComponent<Renderer>().bounds;

            // spawn shark 483.4f
            Vector3 randomSpawnPos = new Vector3(spawnArea.max.x, Random.Range(spawnArea.min.y, spawnArea.max.y), Random.Range(spawnArea.min.z, spawnArea.max.z));
            Instantiate(mapShark, randomSpawnPos, Quaternion.identity).transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
    }
}
