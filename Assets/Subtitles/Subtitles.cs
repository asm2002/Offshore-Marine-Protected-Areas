using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Subtitles : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleText; // text element of the TextMeshProUGUI object

    public Narration[] narrations;

    private Queue<Narration> narrationsQueue = new Queue<Narration>();

    Narration playing;
    string text = "";
    float timePassed = -1f, waitTime = -1;

    AudioSource audioSource;

    public float delay = 0.5f;

    // MODES
    private const int NOTHING_PLAYING = 0, PLAYING = 1, WAITING = 2;
    private int mode = NOTHING_PLAYING;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        foreach (Narration n in narrations)
            narrationsQueue.Enqueue(n);

        subtitleText.text = "";

        playing = null;
    }

    private void Update()
    {

        //if (timePassed >= 0f)
        //{

        //    timePassed += Time.deltaTime;

        //    if (timePassed >= playing.duration)
        //    {
        //        playNextNarration();
        //    }
        //}

        //if (playing == null && narrationsQueue.Count > 0)
        //{
        //    playNextNarration();
        //}


        if (mode == NOTHING_PLAYING)
        {
            if (narrationsQueue.Count > 0)
                playNextNarration();
        }
        else if (mode == PLAYING)
        {
            // the amount of time passed for this narration has reach it's duration length
            if (timePassed >= playing.duration)
            {
                subtitleText.text = "";
                
                mode = WAITING;
            }
            else // the clip isn't finished
            {
                timePassed += Time.deltaTime;
            }
        }
        else if (mode == WAITING)
        {
            if (waitTime >= 0.5f)
            {
                mode = NOTHING_PLAYING;
                waitTime = 0;
            }
            else
            {
                waitTime += Time.deltaTime;
            }

            // if wait time >= 1 second: reset wait time set mode to nothing
            // else increase wait time
        }

    }

    public void enqueueNarration(Narration n)
    {
        narrationsQueue.Enqueue(n);
    }

    private void playNextNarration()
    {
        //timePassed = -1;
        //playing = null;
        //subtitleText.text = "";

        mode = PLAYING;

        if (narrationsQueue.Count <= 0)
        {
            Debug.Log("HOW DID WE GET HERE");
            return;
        }

        Debug.Log("loading the next narration");

        playing = narrationsQueue.Dequeue() as Narration;

        Debug.Log(playing.name + " for " + playing.duration);

        audioSource.clip = playing.audio;
        audioSource.Play();

        timePassed = 0;
        subtitleText.text = playing.subtitle;
    }



}