using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreSystem : MonoBehaviour {

	// Use this for initialization
    public Text scoreValue;
    public Text goldValue;
    public Text notifText;

    //Notifications
    public bool UpdateScore=false;
    public bool UpdateGold = false;

    //Current values
    private int score = 0;
    private int gold = 90;

    //Normal increase rate
    private int scorePerWave=10;
    private int goldPerEnemy = 10;

    //Turret
    private int turretPrice = 50;

	void Start () 
    {
        goldUpdate();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(UpdateScore)
        {
            scoreUpdate();
        }

        if (UpdateGold)
        {
            goldUpdate();
        }
        
	}

    //UPDATE SCORE
    void scoreUpdate()
    {
        UpdateScore = false;
        score += scorePerWave;
        scoreValue.text = score.ToString();
    }

    //UPDATE GOLD
    void goldUpdate()
    {
        UpdateGold = false;
        gold += goldPerEnemy;
        goldValue.text = gold.ToString();
    }

    //BUY A TURRET
    //Is called when a player tries to buy a turret
    //Checks gold ad price
    public bool buyTurret()
    {
        if(gold>=turretPrice)
        {
            gold -= turretPrice;
            goldValue.text = gold.ToString();
            notifText.text = "Turret bought";
            return true;
        }
        else
        {
            notifText.text="Not enough gold";
            return false;
        }
    }

}
