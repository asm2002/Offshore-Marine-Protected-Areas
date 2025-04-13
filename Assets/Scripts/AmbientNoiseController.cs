using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientNoiseController : MonoBehaviour
{
    static AmbientNoiseController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
