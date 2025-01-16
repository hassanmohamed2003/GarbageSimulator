using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private float timerSeconds = 60f;
    private bool timerOn = true;
    private bool levelCompleted = false;
    [SerializeField] private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timerSeconds = gameManager.levelRequirements.Time;
        Debug.Log(timerSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        if(timerSeconds <= 0)
        {
            timerOn = false;
        }

        if (timerOn)
        {
            timerSeconds -= Time.deltaTime;
            UpdateTimer();
        }
        else if(!levelCompleted)
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
}
