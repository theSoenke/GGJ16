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
    private bool loadingStarted = false;
    private int levelToLoad;
    private bool loadLevel;

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

    public bool Finished
    {
        get
        {
            if (videoStarted && _timer <= 0)
            {
                return true;
            }
            return false;
        }
    }

    public void PlayMovie()
    {
        if (_movie != null)
        {
            _movie.Play();
            _audio.Play();
            videoStarted = true;
        }
    }

    public void PlayMovieAndLoadScene(int level)
    {
        if (_movie != null)
        {
            _movie.Play();
            _audio.Play();
            videoStarted = true;
            loadLevel = true;
            levelToLoad = level;
        }
    }

    void Update()
    {
        _timer -= Time.deltaTime;

        if (Finished && loadLevel)
        {
            LoadScene();
        }

        if (Input.GetKeyDown(KeyCode.Space) && loadLevel)
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        if (!loadingStarted)
        {
            _audio.Stop();
            SceneManager.LoadScene(levelToLoad);

            //SceneLoader.Instance.ClickAsync(levelToLoad);
            //loadingStarted = true;
        }
    }
}