using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class VideoPlayer : MonoBehaviour
{
    public MovieTexture _movie;

    private AudioSource _audio;
    private RawImage _image;
    private float _timer;

    void OnEnable()
    {
        _image = GetComponent<RawImage>();
        _audio = GetComponent<AudioSource>();

        _image.texture = _movie;

        if (_movie == null)
        {
            Debug.Log("No MovieTexture set");
        }
        else
        {
            _audio.clip = _movie.audioClip;
        }
    }

    public void PlayMovie()
    {
        if (_movie != null)
        {
            _movie.Play();
            _audio.Play();
            _timer = _movie.duration;
        }
    }

    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            // TODO start game
        }
    }
}
