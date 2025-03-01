using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class Subtitles : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleText; // text element of the TextMeshProUGUI object

    public Narration[] narrations;

    private Queue<Narration> narrationsQueue = new Queue<Narration>();

    Narration playing;
    int i = -1; // index of the subtitle/duration we are on
    string text = "";
    float timePassed = -1f, timeOut = -1;

    AudioSource audioSource;

    public float delay = 0.5f;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();

        foreach (Narration n in narrations)
            narrationsQueue.Enqueue(n);

        subtitleText.text = "";

        playNextNarration();
    }

    /**
     * Structure:
     *      While the queue is not empty:
     *          dequeue narration and set it to playing
     *              within this narration: a current subtitle will be playing
     *              once the timers >= duration of said subtitle, it goes to the next one
     *              once all subtitles have been displayed and thus the narration is over, we can restart this loop 
     */
    private void Update()
    {

        //if (timePassed >= 0f)
        //{
        //    timePassed -= Time.deltaTime;
        //    if (timePassed < 0f)
        //        timePassed = 0f;

        //    if (timePassed == 0f) // change subtitles
        //    {
        //        if (playing.subtitles.Count > 0)
        //        {
        //            i++;
        //            subtitleText.text = playing.subtitles[i];
        //            timePassed = playing.durations[i];
        //        }
        //        else
        //            playNextNarration();
        //    }
        //}

        if (timePassed >= 0f)
        {
            timePassed += Time.deltaTime;

            if (timePassed >= playing.durations[i] - delay)
            {
                if (i < playing.durations.Count - 1)
                {
                    i++;
                    subtitleText.text = playing.subtitles[i];
                }
                else
                {
                    playNextNarration();
                }
            }
        }

    }

    public void queueNarration(Narration n)
    {
        narrationsQueue.Enqueue(n);
    }

    private void playNextNarration()
    {
        if (narrationsQueue.Count <= 0)
        {
            i = -1;
            timePassed = -1;
            subtitleText.text = "";
            return;
        }

        playing = narrationsQueue.Dequeue() as Narration;

        audioSource.clip = playing.audio;
        audioSource.Play();

        i = 0;
        timePassed = 0;
        subtitleText.text = playing.subtitles[i];

    }

    public void setSubtitle(string text, float duration)
    {

    }

    public void setSubtitleSize()
    {

    }

    public void setSubtitleColor()
    {

    }


}