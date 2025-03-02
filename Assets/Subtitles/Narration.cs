using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNarration", menuName = "Narration")]
public class Narration : ScriptableObject
{
    public AudioClip audio; // narration audio
    public List<string> subtitles; // line(s) heard the audio clip (audio)
    public List<float> durations; // where in the audio does the corresponding subtitle end
}
