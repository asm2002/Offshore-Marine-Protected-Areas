using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class Pin : MonoBehaviour
{
    public int sceneIndex;
    public string activityName;
    [SerializeField] private TextMeshProUGUI minigameText;
    [SerializeField] private GameObject canvas;
    [SerializeField] private MainHub mainHub;

    [SerializeField] private XRGrabInteractable xrGrabbin;

    public TransitionEffect transitionEffect;

    private Rigidbody body;

    const int ON_MAP = 0, HOVERING = 1, GRABBED = 2, PICKED = 3; // states of the pin

    private int state;

    float focusOnTime = -1;

    // Start is called before the first frame update
    void Start()
    {
        state = ON_MAP;
        minigameText.text = activityName;
        canvas.SetActive(false);

        body = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        xrGrabbin.selectEntered.AddListener(grabbed);
        xrGrabbin.selectExited.AddListener(released);
        
    }

    private void OnDisable()
    {
        xrGrabbin.selectEntered.RemoveListener(grabbed);
        xrGrabbin.selectExited.RemoveListener(released);
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.activeSelf && focusOnTime > 0)
            focusOnTime -= Time.deltaTime;
        if (focusOnTime <= 0 && !canvas.activeSelf)
            canvas.SetActive(false);


    }

    public void hoveringOver()
    {
        focusOnTime = 5;
        canvas.SetActive(true);
    }

    public void hoveringEnded()
    {
        canvas.SetActive(false);
    }

    public void grabbed(SelectEnterEventArgs args)
    {
        canvas.SetActive(false);
        body.useGravity = true;
        transitionEffect.FadeToBlackAndLoadScene(sceneIndex);

        //mainHub.changeScene(sceneIndex);
    }

    void released(SelectExitEventArgs args)
    {
        body.useGravity = true; // turn on gravity when the object is let go of

    }

}
