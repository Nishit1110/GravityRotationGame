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

    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] GameObject resultObject;
    [SerializeField] TextMeshProUGUI timer;

    private void Awake()
    {
        Instance = this;
        pointsOnScene = GameObject.FindGameObjectsWithTag("Points").Length;
        resultObject.SetActive(false);
        Time.timeScale = 1;
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

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void IncreamentScore()
    {
        points++;
        score.text = "Score : " + points;
        if(pointsOnScene == points)
        {
            GameOver(true);
        } 
    }

    public void GameOver(bool isSuccess)
    {
        if (isSuccess)
            resultText.text = "You Win";
        else
            resultText.text = "You Loose";
        Time.timeScale = 0;
        resultObject.SetActive(true);
    }

    public void StartTimer()
    {
        TimerOn = true;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
