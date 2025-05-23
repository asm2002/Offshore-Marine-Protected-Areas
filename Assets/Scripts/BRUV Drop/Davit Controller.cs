using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;


public class DavitController : MonoBehaviour
{
    public GameObject davit;
    public GameObject snapZone;
    public float rotationSpeed = 30f; // Degrees per second
    public float dropDelay = 0.5f;
    public GameObject tvScreen;
    public GameObject bruv;
    public XRGrabInteractable grab;
    public bool hasLaunched;
    public Subtitles subtitleManager;
    public Narration[] narrations;
    public AudioSource metalNoise;

    private void Start()
    {
        if(grab == null)
        {
            grab = bruv.GetComponent<XRGrabInteractable>();
        }
    }

    void StartRotation()
    {
        StartCoroutine(RotateDavit());
    }

    IEnumerator RotateDavit()
    {
        hasLaunched = true;
        foreach (Narration n in narrations)
        {
            subtitleManager.enqueueNarration(n);
        }

        grab.interactionLayers = InteractionLayerMask.GetMask("BRUV");
        yield return new WaitForSeconds(dropDelay); // Wait for 1 second before deactivating snapZone

        Quaternion startRotation = davit.transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, 90, 0);
        float elapsedTime = 0f;
        float duration = 90f / rotationSpeed; // Time to complete rotation
        
        metalNoise.Play();
        while (elapsedTime < duration)
        {
            davit.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        davit.transform.rotation = targetRotation; // Ensure exact rotation
        yield return new WaitForSeconds(dropDelay); // Wait for 1 second before deactivating snapZone
        snapZone.SetActive(false); // Disable snapZone

        yield return new WaitForSeconds(dropDelay); // Wait for 1 second before rotating back
        bruv.SetActive(false);

        elapsedTime = 0f;

        metalNoise.Play();
        while (elapsedTime < duration)
        {
            davit.transform.rotation = Quaternion.Slerp(targetRotation, startRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        davit.transform.rotation = startRotation; // Ensure exact reset rotation


        yield return new WaitForSeconds(dropDelay);
        tvScreen.SetActive(true);
    }
}
