using UnityEngine;
using TMPro;

public class Pin : MonoBehaviour
{
    public int sceneIndex;
    public string activityName;
    [SerializeField] private TextMeshProUGUI minigameText;
    [SerializeField] private GameObject canvas;

    const int ON_MAP = 0, HOVERING = 1, GRABBED = 2, PICKED = 3; // states of the pin

    private int state;

    // Start is called before the first frame update
    void Start()
    {
        state = ON_MAP;
        minigameText.text = activityName;
        canvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {



    }

    public void hoveringOver()
    {
        canvas.SetActive(true);
        // have some timer system possibly
    }

    public void grabbed()
    {
        canvas.SetActive(false);
        // contact Hub Manager
    }
}
