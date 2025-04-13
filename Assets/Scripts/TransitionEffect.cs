using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionEffect : MonoBehaviour
{
    public Image fadeScreen;
    public float fadeDuration = 2f;
    public Color fadeColor = Color.black;

    void Start()
    {
        // Optional: fade in from black on game start
        fadeColor.a = 1f;
        fadeScreen.color = fadeColor;
        StartCoroutine(FadeRoutine(1f, 0f));
    }

    public void FadeToBlackAndLoadScene(int sceneIndex)
    {
        StartCoroutine(FadeOutAndLoad(sceneIndex));
    }

    private IEnumerator FadeOutAndLoad(int sceneIndex)
    {
        // Fade from clear to black
        yield return StartCoroutine(FadeRoutine(0f, 1f));
        // Load the new scene by index
        SceneManager.LoadScene(sceneIndex);
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            Color newColor = fadeColor;
            newColor.a = alpha;
            fadeScreen.color = newColor;

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure final alpha is set
        Color finalColor = fadeColor;
        finalColor.a = alphaOut;
        fadeScreen.color = finalColor;
    }
}
