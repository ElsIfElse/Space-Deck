using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Collection", menuName = "SO/Audio/Sound Collection", order = 0)]
public class SoundClips : ScriptableObject 
{
    public List<SoundSo> soundClips = new();    
}