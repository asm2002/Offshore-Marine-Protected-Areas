using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaggingShark : MonoBehaviour
{

    public event Action TagPlaced;

    private void OnTriggerEnter(Collider collider)
    {
        TagPlaced?.Invoke();
        Debug.Log("tag placed invoked");
    }

}
