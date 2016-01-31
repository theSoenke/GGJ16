using UnityEngine;

public class OutroManager : MonoBehaviour
{
    public GameObject outro;
    public GameObject outroH;
    public GameObject credits;

    private VideoPlayer videoPlayer;
    private bool _creditsStarted;

    void Start()
    {
        if (GameManager.instance.GameOver)
        {
            if (GameManager.instance.Won)
            {
                outro.SetActive(false);
                outroH.SetActive(true);
                credits.SetActive(false);

                videoPlayer = outroH.GetComponent<VideoPlayer>();
            }
            else
            {
                outro.SetActive(true);
                outroH.SetActive(false);
                credits.SetActive(false);

                videoPlayer = outro.GetComponent<VideoPlayer>();
            }

            videoPlayer.PlayMovie();
        }
    }

    void Update()
    {
        if(videoPlayer.Finished && !_creditsStarted)
        {
            // play credits
            outro.SetActive(false);
            outroH.SetActive(false);
            credits.SetActive(true);
            _creditsStarted = true;
            videoPlayer = credits.GetComponent<VideoPlayer>();
            videoPlayer.PlayMovieAndLoadScene(0);
        }
    }
}
