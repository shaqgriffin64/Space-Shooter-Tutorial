using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text score;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private int scoreValue;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        scoreValue = 0;
        UpdateScore ();
        StartCoroutine (SpawnWaves());
        //score.text = "Score: " + scoreValue.ToString();
    }

    private void Update()
    {
        if (restart)
            {
                if (Input.GetKeyDown (KeyCode.R))
                    {
                        SceneManager.LoadScene("SampleScene");
                    }
            }

        if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
    }

    IEnumerator SpawnWaves ()
        {
            yield return new WaitForSeconds(startWait);
            while (true)
            {
                for (int i = 0; i < hazardCount; i++)
                    {
                        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(hazard, spawnPosition, spawnRotation);
                        yield return new WaitForSeconds(spawnWait);

                        if (gameOver)
                            {
                                restartText.text = "Press 'R' for Restart";
                                restart = true;
                                break;
                            }
                    }
                yield return new WaitForSeconds(waveWait);
            }
        }

    public void AddScore (int newScoreValue)
        {
            scoreValue += newScoreValue;
            UpdateScore();
        }

    void UpdateScore ()
        {
        score.text = "Score: " + scoreValue.ToString();
        }

    public void GameOver ()
        {
            gameOverText.text = "Game Over! Game made by Shaquille Griffin";
            gameOver = true;
        }
}
