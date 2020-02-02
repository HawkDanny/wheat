using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New CoolSoundGroup", menuName = "CoolSoundGroup")]
public class CoolSoundGroup : ScriptableObject
{
    [Header("Cool Sounds")]
    public CoolSound[] sounds;
    [Header("Mixer Group")]
    public AudioMixerGroup audioMixerGroup;
}
