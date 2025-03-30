using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class OutroPinManager : MonoBehaviour {

    [SerializeField] private GameObject[] pins;

    public void DisableAllPins() {
        foreach (GameObject pin in pins) {
            pin.GetComponent<XRSimpleInteractable>().enabled = false;
        }
    }
}
