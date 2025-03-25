using System.Collections;
using UnityEngine;

public class DavitController : MonoBehaviour
{
    public GameObject davit;
    public GameObject snapZone;
    public float rotationSpeed = 30f; // Degrees per second
    public float dropDelay = 0.5f;
    public GameObject tvScreen;


    void StartRotation()
    {
        StartCoroutine(RotateDavit());
    }

    IEnumerator RotateDavit()
    {
        yield return new WaitForSeconds(dropDelay); // Wait for 1 second before deactivating snapZone

        Quaternion startRotation = davit.transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, 90, 0);
        float elapsedTime = 0f;
        float duration = 90f / rotationSpeed; // Time to complete rotation

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
        elapsedTime = 0f;
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
