using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public int totalSlots = 3;
    private int matchedSlots = 0;

    public void IncrementMatches()
    {
        matchedSlots++;
        if (matchedSlots >= totalSlots) EndMinigame();
    }

    void EndMinigame()
    {
        Debug.Log("Minigame complete!");
    }
}