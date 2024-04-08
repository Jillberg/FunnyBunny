using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    public AudioSource scoringSFX;
    public int highestScore;
    public Text Record;

    
    void Start()
    {
        highestScore = LoadRecord();
        Record.text = highestScore.ToString();
    }

    [ContextMenu("increase score")]
    public void addScore(int scoreToAdd){
        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();
        scoringSFX.Play();
        if (playerScore >= highestScore)
        {
            highestScore=playerScore;
            SaveRecord(highestScore);
            Record.text = highestScore.ToString();
        }

     }

    public void reStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     

    }

    public void gameOver()
    {
        SaveRecord(highestScore);
        gameOverScreen.SetActive(true);
    }

    public void BackToMenu()
    {
        
        SceneManager.LoadSceneAsync("TitleScene");
    }

    public void SaveRecord(int input)
    {
        PlayerPrefs.SetInt("highestScore", input);
        Debug.Log("record saved");

    }

    public int LoadRecord()
    {
        Debug.Log("record loaded");
        Debug.Log(PlayerPrefs.GetInt("highestScore"));
        return PlayerPrefs.GetInt("highestScore");
        
    }
}
