using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "SO/Audio/Sound", order = 0)]
public class SoundSo : ScriptableObject 
{
    public AudioStruct Sound;
}

[Serializable]
public struct AudioStruct
{
    public AudioType AudioType;
    public AudioClip Clip;
}