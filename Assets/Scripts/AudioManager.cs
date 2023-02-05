using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

/*
THANK YOU BRACKEYS
*/

namespace AudioManagement
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager instance;
        public static AudioManager Instance { get { return instance; } }
        public bool dontDestroyOnLoad = false;
        public List<Sound> sounds = new List<Sound>();

        void Awake()
        {
            if (AudioManager.instance != null)
            {
                Destroy(gameObject);
                Debug.LogError("There can only be one AudioManager");
                return;
            }

            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);

            AudioManager.instance = this;

            foreach (Sound sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.Volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
                sound.source.playOnAwake = sound.playOnAwake;
                if (sound.playOnAwake)
                    sound.source.Play();
            }
        }

        public void Play(string _name)
        {
            sounds.FindAll(sounds => sounds.name == _name)
                    .ForEach(sound => sound.source.Play());
        }

        public void Play(SoundType _type)
        {
            sounds.FindAll(sounds => sounds.soundType == _type)
                    .ForEach(sound => sound.source.Play());
        }

        public void PlayRandom(List<string> _names)
        {
            if (_names.Count <= 0) return;

            int random = Random.Range(0, _names.Count);
            Play(_names[random]);
        }

        public void Pause(string _name)
        {
            sounds.FindAll(sounds => sounds.name == _name)
                    .ForEach(sound => sound.source.Pause());
        }

        public void PauseSounds(List<string> _sounds)
        {
            if (_sounds.Count <= 0) return;

            foreach (string sound in _sounds)
            {
                sounds.FindAll(sounds => sounds.name == sound)
                    .ForEach(sound => sound.source.Pause());
            }
        }

        public void Pause(SoundType _type)
        {
            sounds.FindAll(sounds => sounds.soundType == _type)
                    .ForEach(sound => sound.source.Pause());
        }

        public void PauseSFX()
        {
            Pause(SoundType.SFX);
        }

        public void PauseMusic()
        {
            Pause(SoundType.Music);
        }

        public void ChangeVolume(string _name, float _volume)
        {
            sounds.FindAll(sounds => sounds.name == _name)
                    .ForEach(sound => sound.source.volume = _volume);
        }

        public void ChangeVolume(SoundType _soundType, float _volume)
        {
            sounds.FindAll(sounds => sounds.soundType == _soundType)
                    .ForEach(sound => sound.source.volume = _volume);
        }
    }
}