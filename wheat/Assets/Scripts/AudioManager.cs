using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AudioManagers are intended to be placed directly on an object,
//and to only contain the audio of that object. All of the audio
//sources will be placed as children objects
public class AudioManager : MonoBehaviour
{
    //The sounds scriptable object is an array of CoolSounds, each of
    //which will be put into the _sounds dictionary for fast lookup later
    public CoolSoundGroup sounds;
    private Dictionary<string, CoolSound> _sounds;

    //TODO: Streams functionality? Play() already acts like a stream, so maybe add PlayOneShot() functionality

    private void Awake()
    {
        _sounds = new Dictionary<string, CoolSound>();

        //Add each of the cool sounds from the group to the array, for easy lookup
        foreach (CoolSound cs in sounds.sounds)
        {
            CoolSound localCoolSound = ScriptableObject.CreateInstance<CoolSound>();

            GameObject g = new GameObject("audio source: " + cs.name);
            g.transform.SetParent(this.transform);
            g.transform.localPosition = Vector3.zero;
            localCoolSound.source = g.AddComponent<AudioSource>();

            //Take all of the values from the CoolSound and apply them to the audiosource
            localCoolSound.name = cs.name;
            localCoolSound.clip = cs.clip;
            localCoolSound.alts = cs.alts;
            localCoolSound.source.outputAudioMixerGroup = sounds.audioMixerGroup;
            localCoolSound.source.playOnAwake = cs.playOnAwake;
            localCoolSound.source.clip = cs.clip;
            localCoolSound.source.volume = cs.volume;
            localCoolSound.source.pitch = cs.pitch;
            localCoolSound.source.loop = cs.loop;
            localCoolSound.source.panStereo = cs.stereoPan;
            localCoolSound.source.spatialBlend = cs.spatialBlend;
            localCoolSound.source.rolloffMode = cs.volumeRolloff;
            localCoolSound.source.minDistance = cs.minDistance;
            localCoolSound.source.maxDistance = cs.maxDistance;

            //Play the sound immediately if desired
            if (cs.playOnAwake)
                localCoolSound.source.Play();

            //Add the sound to the dictionary for fast lookup later
            _sounds.Add(localCoolSound.name, localCoolSound);
        }
    }

    /// <summary>
    /// Plays the cool sound that corresponds to the given name
    /// </summary>
    /// <param name="name">The name value of one of the cool sounds in the cool sound group</param>
    public void Play(string name)
    {
        //        print("Playing: " + name);
        CoolSound cs = _sounds[name];

        //If there is only one sound just play it
        //If there are more, randomly pick one
        if (cs.alts.Length == 0)
            cs.source.Play();
        else
        {
            //Since there are multiple alts, the clip in the source will be changing
            //all the time. So we need to always pick a random sound
            //TODO: Make this a more tetris-like random
            int randomClipIndex = Random.Range(0, cs.alts.Length + 1);

            //We have to still include the original sound, so the random range is one number bigger than the number of alts
            if (randomClipIndex == cs.alts.Length)
                cs.source.clip = cs.clip;
            else
                cs.source.clip = cs.alts[randomClipIndex];

            //Play it
            cs.source.Play();
        }
    }

    /// <summary>
    /// Plays the cool sound that corresponds to the given name ONLY if source isPlaying == false
    /// </summary>
    /// <param name="name">The name value of one of the cool sounds in the cool sound group</param>
    public void PlaySafe(string name)
    {
        if (!_sounds[name].source.isPlaying)
            Play(name);
    }

    /// <summary>
    /// Plays all the sound clips managed through this AudioManager
    /// </summary>
    public void PlayAll()
    {
        foreach (CoolSound cs in _sounds.Values)
            Play(cs.name);
    }


    /// <summary>
    /// Stops the sound clip that corresponds to the name provided
    /// </summary>
    /// <param name="name">The name value of one of the cool sounds in the cool sounds group</param>
    public void Stop(string name)
    {
        _sounds[name].source.Stop();
    }

    /// <summary>
    /// Stops all the sound clips managed through this AudioManager
    /// </summary>
    public void StopAll()
    {
        foreach (CoolSound cs in _sounds.Values)
            Stop(cs.name);
    }

    /// <summary>
    /// Set the volume of a specific audio clip
    /// </summary>
    /// <param name="name">The name value of one of the sounds in the array</param>
    /// <param name="volume">The volume from 0 to 1</param>
    public void SetVolume(string name, float volume)
    {
        _sounds[name].source.volume = volume;
    }

    /// <summary>
    /// Get the volume of a specific audio clip
    /// </summary>
    /// <param name="name">The name value of one of the sounds in the array</param>
    public float GetVolume(string name)
    {
        return _sounds[name].source.volume;// = volume;
    }

    /// <summary>
    /// Set the pitch of a specific audio clip
    /// </summary>
    /// <param name="name">The name value of one of the sounds in the array</param>
    /// <param name="pitch">The pitch from 0.1 to 3</param>
    public void SetPitch(string name, float pitch)
    {
        _sounds[name].source.pitch = pitch;
    }
}
