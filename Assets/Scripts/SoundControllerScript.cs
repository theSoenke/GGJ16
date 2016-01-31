using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SoundControllerScript : MonoBehaviour
{
    public float _fadeDuration;
    public AudioMixerSnapshot[] _channels;
    public int _currentTrack;
    public static SoundControllerScript _instance;

    void Awake ()
    {
        _currentTrack = 1;
        _instance = this;
    }

    public void SwitchTrack(int nr)
    {
        if (nr <= _channels.Length)
        {
            _channels[nr - 1].TransitionTo(_fadeDuration);
            _currentTrack++;
        }
        
    }
    
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    _currentTrack++;
        //    SwitchTrack(_currentTrack);
        //}
    }



}
