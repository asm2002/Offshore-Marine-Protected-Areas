using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public int totalSlots = 3;
    public Narration n;
    public int matchedSlots = 0;
    private bool gameOver = false;
    public void IncrementMatches()
    {
        matchedSlots++;
        if (matchedSlots >= totalSlots && !gameOver) EndMinigame();
    }

    void EndMinigame()
    {
        FindObjectOfType<Subtitles>().enqueueNarration(n);
        FindObjectOfType<SceneFader>().FadeToScene("OutroHub");

        Debug.Log("Minigame complete!");
        gameOver = true;
    }
}