using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class VideoPlayer : MonoBehaviour
{
    public MovieTexture _movie;

    private AudioSource _audio;
    private RawImage _image;

    void Start()
    {
        _image = GetComponent<RawImage>();
        _audio = GetComponent<AudioSource>();

        _image.texture = _movie;
        _audio.clip = _movie.audioClip;

        _movie.Play();
        _audio.Play();
    }

    void Update()
    {

    }
}
