using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour


{

    public static bool gameOver;
    public GameObject gameOverPanel;

    public static int numberOfCoins;

    public Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        numberOfCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        scoreText.text = "SCORE: " + numberOfCoins.ToString();

    }
}
