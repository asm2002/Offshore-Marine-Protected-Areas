using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine;

public class SnapPetri : MonoBehaviour
{
    public Transform socket; // Drag microscope socket here
    public float snapDistance = 0.1f;
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;
    public GameObject ednaMinigame;
    public ParticleSystem particleSystem1, particleSystem2, particleSystem3;
    private bool effectHappened = false;
    public GameObject particleParent;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, socket.position) < snapDistance)
        {
            transform.position = socket.position;
            transform.rotation = socket.rotation;
            if(effectHappened == false)
            {
                TriggerEffect();
            }
            rb.isKinematic = true;
            ednaMinigame.SetActive(true);
        }
    }

    public void TriggerEffect()
    {
        effectHappened = true;
        particleParent.SetActive(true);
    }
}