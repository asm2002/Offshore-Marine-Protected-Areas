using UnityEngine;

public class BoatSway : MonoBehaviour
{
    [SerializeField]

    public bool sway;

    public float swayAmplitude = 1f;
    public float swayFrequency = 0.1f;

    private Vector3 initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (sway)
        {
            float xSway = swayAmplitude * Mathf.Sin(Time.time * swayFrequency * Mathf.PI * 2f);
            float ySway = swayAmplitude * Mathf.Sin((Time.time * swayFrequency * Mathf.PI * 2f) + Mathf.PI / 2);
            transform.eulerAngles = initialRotation + new Vector3(xSway, 0f, ySway);
        }
    }

    public void toggleSway() { 
        sway = !sway; 
    }

}
