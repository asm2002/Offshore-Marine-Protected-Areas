using UnityEngine;

public class WaterInteraction : MonoBehaviour
{
    public ParticleSystem splashEffect; 

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("PlayerHand") || other.CompareTag("Interactable"))
        {
            // Trigger splash effect
            if (splashEffect != null)
            {
                Instantiate(splashEffect, other.transform.position, Quaternion.identity);
            }
        }
    }
}