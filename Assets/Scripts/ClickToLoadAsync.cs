using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToLoadAsync : MonoBehaviour {

	public Slider loadingBar;
	public GameObject loadingImage;
    
	

	public void ClickAsync (int level){
		loadingImage.SetActive (true);
		StartCoroutine (LoadLevelWithBar (level));
	}

    public void StartGame(int level)
    {
       
            ClickAsync(level);
        
    }

	IEnumerator LoadLevelWithBar (int level){
       AsyncOperation async = SceneManager.LoadSceneAsync(level);
		while (!async.isDone) {
			loadingBar.value = async.progress;
			yield return null;
		}
	}

    public void EndGame()
    {
        Application.Quit();
    }

    
	
}

