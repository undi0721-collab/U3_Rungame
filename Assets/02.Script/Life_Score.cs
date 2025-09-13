using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Life_Score : MonoBehaviour
{
    public PlayerController player;
    public Text scoreText;
    public LifeMangerd life;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int score = updateScore();
        scoreText.text = "Score :" + score + "m";

        life.UpdateHeart(player.Life());
        if ( player.Life() <= 0)
        {
            enabled = false;
            Invoke("ReturnGameOver", 2.0f);

            if (PlayerPrefs.GetInt("Highscore") < score)
            {
                PlayerPrefs.SetInt("Highscore", score);
            }
        }

    }
    int updateScore()
    {
        return (int)player.transform.position.z;
    }
    void ReturnGameover()
    {
        SceneManager.LoadScene("GameOver");
    }
}
