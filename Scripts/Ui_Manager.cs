using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Manager : MonoBehaviour
{
   public Sprite[] lives;
   public Image livesImageDisplay;
    public Text scoreText;
    public int score;
    public GameObject titleScreen;
    public GameObject gameObjectlives;
    public GameObject gameObjectscore;

    void Start()
    {
        
        gameObjectlives.SetActive(false);
        gameObjectscore.SetActive(false);
    }

    public void UpdateLives(int currentLives)
    {
        
        livesImageDisplay.sprite = lives[currentLives];
        if (currentLives == 0)
        {

        }
    }

   public void UpdateScore()
    {
        score += 10;

        scoreText.text = "Score: " + score;
    }
    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
        gameObjectlives.SetActive(false);
        gameObjectscore.SetActive(false);
    }
   public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        gameObjectlives.SetActive(true);
        gameObjectscore.SetActive(true);
        scoreText.text = "Score: ";
        score = 0;
    }
}
