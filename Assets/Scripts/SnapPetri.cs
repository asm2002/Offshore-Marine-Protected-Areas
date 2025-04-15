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
    public GameObject map;
    public GameObject pins;
    public GameObject blankMap;
    public GameObject waterFill;
    public bool isFilled = false;
    public bool isSnapped = false;
    public Narration n1, n2, n3, n4, n5;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the tube is in the water
        if (other.CompareTag("Interactable") && !isFilled)
        {
            FillTube();
        }
    }

    private void FillTube()
    {
        isFilled = true;
        if (waterFill != null)
        {
            waterFill.SetActive(true);
            FindObjectOfType<Subtitles>().enqueueNarration(n1);
        }
        Debug.Log("Petri dish filled with water!");
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, socket.position) < snapDistance)
        {
            if (isFilled)
            {
                transform.position = socket.position;
                transform.rotation = socket.rotation;
                if (effectHappened == false)
                {
                    TriggerEffect();
                }
                rb.isKinematic = true;
                if (!isSnapped)
                {
                    ednaMinigame.SetActive(true);
                    pins.SetActive(false);
                    map.SetActive(false);
                    blankMap.SetActive(true);
                    FindObjectOfType<Subtitles>().enqueueNarration(n2);
                    FindObjectOfType<Subtitles>().enqueueNarration(n3);
                    FindObjectOfType<Subtitles>().enqueueNarration(n4);
                    FindObjectOfType<Subtitles>().enqueueNarration(n5);
                    isSnapped = true;
                }
            }
        }
    }

    public void TriggerEffect()
    {
        effectHappened = true;
        particleParent.SetActive(true);
    }
}