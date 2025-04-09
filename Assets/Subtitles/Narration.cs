using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Narration", menuName = "Narration V2")]
public class Narration : ScriptableObject
{
    public AudioClip audio; // narration audio
    public string subtitle; // line heard the audio clip
    public float duration; // where in the audio does the corresponding subtitle end
}
