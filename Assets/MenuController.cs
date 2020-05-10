using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public Text txtBestScore;

	void Start () {
//        SetSoundState();
        txtBestScore.text = PlayerPrefs.GetFloat("BestScore", 0).ToString("0");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");        
    }

}
