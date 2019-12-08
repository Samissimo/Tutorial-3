﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text RestartText;
    public Text GameOverText;
    public Text winText;
    public Text creditWin;
    public Text creditLose;

    private bool gameOver;
    private bool restart;
    private int score;

    public AudioSource musicSource;
    public AudioClip background;
    public AudioClip win;
    public AudioClip lose;
    
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        restart = false;
        RestartText.text = "";
        GameOverText.text = "";
        winText.text = "";
        creditLose.text = "";
        creditWin.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.X))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                RestartText.text = "Press 'X' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            winText.text = "You win!";
            gameOver = true;
            restart = true;
            musicSource.Stop();
            musicSource.clip = win;
            musicSource.Play();
            creditWin.text = "Song: 'Can't Stop Winning' by Jonathan Shaw (www.jshaw.co.uk)";
            GameOver();
        }
    }

    public void GameOver ()
    {
        if (score < 100)
        {
            musicSource.Stop();
            musicSource.clip = lose;
            musicSource.Play();
            creditLose.text = "Song: 'Lost Signal' by PetterTheSturgeon on OpenGameArt";
        }
        GameOverText.text = "Game Over! Game created by Samantha Sander!";
        gameOver = true;
    }
}
