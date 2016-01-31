using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class VideoPlayer : MonoBehaviour
{
    public MovieTexture _movie;
    public float videoDuration = 10f;

    private AudioSource _audio;
    private RawImage _image;
    private float _timer;
    private bool videoStarted = false;

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

        _timer = videoDuration;
    }

    public void PlayMovie()
    {
        if (_movie != null)
        {
            _movie.Play();
            _audio.Play();
            //_timer = _movie.duration;
            videoStarted = true;
        }
    }

    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0 && videoStarted)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync("prototyp");
        }
    }
}
