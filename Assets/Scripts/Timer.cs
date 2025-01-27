using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private float timerSeconds = 60f;
    private bool timerOn = true;
    private bool levelCompleted = false;
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timerSeconds = gameManager.levelRequirements.Time;
        Debug.Log(timerSeconds);
    }

    void Update()
    {
        if (timerSeconds <= 0)
        {
            timerOn = false;
            if (!levelCompleted) 
            {
                LevelFailed();  
            }
        }

        if (timerOn)
        {
            timerSeconds -= Time.deltaTime;
            UpdateTimer();
        }
        else if (!levelCompleted)
        {
            string gameOver = "Time is up!";
            timerText.text = gameOver;
        }
    }

    void UpdateTimer()
    {
        int timerRounded = (int)Mathf.Round(timerSeconds);
        timerText.text = timerRounded.ToString();
    }

    public void FreezeTimer()
    {
        levelCompleted = true;
        timerOn = false;
    }

    public void DecreaseTime(float seconds)
    {
        timerSeconds -= seconds;
        if (timerSeconds < 0) 
        {
            timerSeconds = 0;
        }
    }

    private void LevelFailed()
    {
        levelCompleted = true; 
        SceneManager.LoadScene("Death");  
    }
}