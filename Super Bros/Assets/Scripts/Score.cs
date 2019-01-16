using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float timer = 120;
    private int playerScore = 0;
    public Text timerUI;
    public Text playerScoreUI;
	
    void Start()
    {
        DataManagement.dm.LoadData();
    }

	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;
        timerUI.text = ("Time left: " + (int)timer);
        playerScoreUI.text = ("Score: " + playerScore);

        if (timer < 0.1f)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
	}

    void OnTriggerEnter2D (Collider2D trig)
    {
        if(trig.gameObject.name == "EndLevel")
        {
            CountScore();
        }
        if(trig.gameObject.tag == "Coin")
        {
            playerScore += 100;
            Destroy(trig.gameObject);
        }
    }

    void CountScore ()
    {
        playerScore = playerScore + (int)(timer * 10);
        DataManagement.dm.highScore = playerScore;
        DataManagement.dm.SaveData();
    }
}
