using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PinsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pins;

    // Start is called before the first frame update
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt(sceneName, 1);
        PlayerPrefs.SetInt("scenes_left", PlayerPrefs.GetInt("scenes_left") - 1);

        enablePins();
    }

    void enablePins()
    {
        // if this is the Bruv Launch Minigame and no more scenes are left
        if (SceneManager.GetActiveScene().name == "Bruv Launch" && PlayerPrefs.GetInt("scenes_left") <= 0)
        {
            pins[0].SetActive(false);
            pins[1].SetActive(false);
            pins[2].SetActive(false);

            pins[3].SetActive(true);
        }
        else
        {
            if (PlayerPrefs.GetInt("Shark Tagging") == 1)
                pins[0].SetActive(false);
            if (PlayerPrefs.GetInt("Bruv Launch") == 1)
                pins[1].SetActive(false);
            if (PlayerPrefs.GetInt("EDNA Sampling") == 1)
                pins[2].SetActive(false);
        }


    }
}
