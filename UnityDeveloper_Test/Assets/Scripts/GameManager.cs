using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timeLeft;
    public bool TimerOn = false;

    public static GameManager Instance;
    int points = 0;
    int pointsOnScene;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] GameObject resultMenu;
    [SerializeField] GameObject startMenu;
    [SerializeField] TextMeshProUGUI timer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        pointsOnScene = GameObject.FindGameObjectsWithTag("Points").Length;
        resultMenu.SetActive(false);
        Time.timeScale = 0;
        score.text = "Score : " + points;
    }

    private void Update()
    {
        if (TimerOn)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                GameOver(false);
            }
        }
    }

    void UpdateTimer(float currentTime)  //Decrement timer constantly after game starts
    {
        currentTime += 1;
        
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void IncreamentScore() //Increment score after cubes are collected
    {
        points++;
        score.text = "Score : " + points;
        if(pointsOnScene == points)
        {
            GameOver(true);
        } 
    }

    public void GameOver(bool isSuccess) //Display result
    {
        if (isSuccess)
            resultText.text = "You Win";
        else
            resultText.text = "You Loose";
        Time.timeScale = 0;
        resultMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartTimer()  //Start game method
    {
        TimerOn = true;
        startMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PlayAgain() //Restart Game
    {
        SceneManager.LoadScene(0);
    }

    public void Quit() //Quit applicaiton
    {
        Application.Quit();
    }
}
