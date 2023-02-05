using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManagement;

public class AudioComponent : MonoBehaviour
{
    public void Play(string _name)
    {
        AudioManager.Instance.Play(_name);
    }

    public void PlayRandom(List<string> _names)
    {
        AudioManager.Instance.PlayRandom(_names);
    }

    public void Pause(string _name)
    {
        AudioManager.Instance.Pause(_name);
    }

    public void PauseSFX()
    {
        AudioManager.Instance.PauseSFX();
    }

    public void PauseMusic()
    {
        AudioManager.Instance.PauseMusic();
    }

    public void ChangeVolume(string _name, float _volume)
    {
        AudioManager.Instance.ChangeVolume(_name, _volume);
    }

    public void ChangeVolume(SoundType _type, float _volume)
    {
        AudioManager.Instance.ChangeVolume(_type, _volume);
    }
}