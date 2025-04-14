using UnityEngine;

public class FillPetri : MonoBehaviour
{
    public GameObject waterFill;
    public bool isFilled = false; // Track if tube is already filled

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
        }
        Debug.Log("Petri dish filled with water!");
    }

    /* Not currently using
    public void EmptyTube()
    {
        isFilled = false;
        if (waterFill != null)
        {
            waterFill.SetActive(false); // Hide the water fill object
        }
        Debug.Log("Test tube emptied!");
    }
    */
}