using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public int sceneIndex;
    public string activityName;

    const int ON_MAP = 0, HOVERING = 1, GRABBED = 2, PICKED = 3; // states of the pin

    private int state;

    // Start is called before the first frame update
    void Start()
    {
        state = ON_MAP;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == ON_MAP)
        {

        }
        else if (state == GRABBED)
        {

        }
        else if (state == PICKED)
        {
            
        }
        else
        {
            Debug.Log("LOGICAL ERROR: State of pin = " + state);
        }
    }

    public void hoveringOver()
    {
        Debug.Log(activityName);
    }
}
