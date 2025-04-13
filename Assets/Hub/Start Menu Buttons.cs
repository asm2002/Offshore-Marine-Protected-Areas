using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuButtons : MonoBehaviour
{
    [SerializeField] TransitionEffect transitioner;
    [SerializeField] Toggle subtitlesToggle, motionSickToggle;


    bool subtitlesOn, motionSickOn;

    private void Start()
    {
        if (PlayerPrefs.HasKey("subtitles"))
        {
            int x = PlayerPrefs.GetInt("subtitles");

            if (x == 0) subtitlesOn = false;
            else if (x == 1) subtitlesOn = true;
            else //  this shouldn't be a possible case, as the key should be 0, 1, or not initiated
            {
                subtitlesOn = true;
                PlayerPrefs.SetInt("subtitles", 1);
            }
        }
        else // establish default value of "subtitles" key as true
        {
            subtitlesOn = true;
            PlayerPrefs.SetInt("subtitles", 1);
        }

        if (PlayerPrefs.HasKey("motionSick"))
        {
            int x = PlayerPrefs.GetInt("motionSick");

            if (x == 0) motionSickOn = false;
            else if (x == 1) motionSickOn = true;
            else //  this shouldn't be a possible case, as the key should be 0, 1, or not initiated
            {
                PlayerPrefs.SetInt("motionSick", 0);
                motionSickOn = false;
            }
        }
        else // establish default value of "motion sickness" key as false
        {
            PlayerPrefs.SetInt("motionSick", 0);
            motionSickOn = false;
        }


        subtitlesToggle.isOn = subtitlesOn;
        motionSickToggle.isOn = motionSickOn;
    }


    public void startButton() {
        transitioner.FadeToBlackAndLoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void toggleSubtitles()
    {
        int x = PlayerPrefs.GetInt("subtitles");
        if (x == 0) // subtitles were off, and should now be turned on;
        {
            subtitlesToggle.isOn = true;
            subtitlesOn = true;
            PlayerPrefs.SetInt("subtitles", 1);
        }
        else if (x == 1) // subtitles were on, and now should be turned off
        {
            subtitlesToggle.isOn = false;
            subtitlesOn = false;
            PlayerPrefs.SetInt("subtitles", 0);
        }
    }

    public void toggleMotionSickness()
    {
        int x = PlayerPrefs.GetInt("motionSick");
        if (x == 0) // motionSickness was off, and should now be turned on;
        {
            motionSickToggle.isOn = true;
            motionSickOn = true;
            PlayerPrefs.SetInt("motionSick", 1);
        }
        else if (x == 1) // motionSickness was on, and now should be turned off
        {
            motionSickToggle.isOn = false;
            motionSickOn = false;
            PlayerPrefs.SetInt("motionSick", 0);
        }
    }


}
