using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SlotMatcher : MonoBehaviour
{
    public string requiredTag; // "Fish"
    public MinigameManager gameManager;
    public GameObject model;
    public GameObject incompleteDna;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.transform.position = transform.position; // Snap
            other.transform.rotation = transform.rotation;
            other.GetComponent<Rigidbody>().isKinematic = true;

            var grabInteractable = other.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
                grabInteractable.enabled = false;

            model.SetActive(false);
            incompleteDna.SetActive(false);
            other.GetComponent<Rigidbody>().isKinematic = true;
            gameManager.IncrementMatches();
        }
    }
}