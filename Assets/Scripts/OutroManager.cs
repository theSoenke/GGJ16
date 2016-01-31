using UnityEngine;

public class OutroManager : MonoBehaviour
{
    public GameObject outro;
    public GameObject outroH;

    private VideoPlayer videoPlayer;

    void Start()
    {
        if (GameManager.instance.GameOver)
        {
            if (GameManager.instance.Won)
            {
                outro.SetActive(false);
                outroH.SetActive(true);

                videoPlayer = outroH.GetComponent<VideoPlayer>();
            }
            else
            {
                outro.SetActive(true);
                outroH.SetActive(false);

                videoPlayer = outro.GetComponent<VideoPlayer>();
            }

            videoPlayer.PlayMovie();
        }
    }
}
