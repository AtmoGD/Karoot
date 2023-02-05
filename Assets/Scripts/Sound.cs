using UnityEngine.Audio;
using UnityEngine;
using System;

namespace AudioManagement
{
    public enum SoundType
    {
        Music,
        SFX
    }

    // [System.Serializable, CreateAssetMenu(fileName = "New Sound", menuName = "Audio/Sound")]
    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        private float volume = 1f;
        [Range(0.0f, 10.0f)] public float volumeMultiplier = 1f;
        public float Volume { get { return volume * volumeMultiplier; } }

        [Range(0.1f, 3.0f)] public float pitch = 1f;
        public bool loop = false;
        public bool playOnAwake = false;
        public SoundType soundType;
        [HideInInspector] public AudioSource source;
    }
}