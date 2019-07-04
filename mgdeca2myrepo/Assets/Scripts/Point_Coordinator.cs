using System.Collections;
using System.Collections.Generic;
using TMPro; //using text mesh pro because it is fancier
using UnityEngine.UI;
using UnityEngine;

public class Point_Coordinator : MonoBehaviour
{
    //This script works in tandem with game manager so send scoring to the UI and keep track of coins on the field
    
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI coinsUI;
    public TextMeshProUGUI timerUI;
    public Image currentEnergy;

    public bool PlayerWins = false;

    private int score = 0;
    private int coins = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Updates the score for the score UI
public  void updateScore(int currentscore)
    {
        score = currentscore;
        scoreUI.text = "Score " + score.ToString();
    }
    //Update score by 1;
public  void updateScore()
    {
        score++;
        scoreUI.text = "Score  " + score.ToString();
    }

    //updates how many coins are left on the UI
public void UpdateCoinsLeft()
    {
      coins  = GameObject.FindGameObjectsWithTag("Coins").Length;

        coinsUI.text = "Coins Left: " + coins.ToString();
        if (coins == 0) { PlayerWins = true; } //player wins when all coins are false, this value is sent to gamemanger to display end screen

    }


    public void UpdateTime( float time)
    {
        timerUI.text = "Timer: " + System.Math.Round(time, 2).ToString() ;
    }

    public void UpdateEnergyBar(float currentTime, float maxduration)
    {
        float ratio = currentTime / maxduration;
        currentEnergy.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }
}
