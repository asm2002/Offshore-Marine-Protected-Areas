using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class OutroPin : MonoBehaviour
{
    public string areaName;
    [SerializeField] private TextMeshProUGUI areaLabel;
    [SerializeField] private GameObject canvas;
    private Material material;
    [SerializeField] private GameObject pinManager;
    [SerializeField] private Subtitles subtitleManager;
    [SerializeField] private Narration[] afterSelectNarration;
    // public TransitionEffect transitionEffect;
    

    // Start is called before the first frame update
    void Start() {
        areaLabel.text = areaName;
        canvas.SetActive(true);
        material = GetComponent<Renderer>().material;
    }

    public void selected(SelectEnterEventArgs args) {
        material.shader = Shader.Find("Universal Render Pipeline/Lit");
        material.SetColor("_BaseColor", Color.green);
        pinManager.GetComponent<OutroPinManager>().DisableAllPins();
        Debug.Log("Selected");
        
        foreach (Narration narr in afterSelectNarration) {
            subtitleManager.enqueueNarration(narr);
        }

        subtitleManager.isOutroScene = true;
        //transitionEffect.FadeToBlackAndLoadScene(index);
    }

}
