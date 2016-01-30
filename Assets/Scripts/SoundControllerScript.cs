using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SoundControllerScript : MonoBehaviour
{
    public float _fadeDuration;
    public AudioMixerSnapshot[] _channels;
    public int _currentTrack;

    void Start ()
    {
        _currentTrack = 1;

    }

    public void SwitchTrack(int nr)
    {
        if(nr <= _channels.Length)
            _channels[nr - 1].TransitionTo(_fadeDuration);
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
