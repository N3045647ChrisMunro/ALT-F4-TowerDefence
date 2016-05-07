using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreSystem : MonoBehaviour {

    // Use this for initialization
    public Text scoreValue;
    public Text goldValue;
    public Text notifText;
    public Text healthValue;

    //Notifications
    public bool UpdateScore = false;
    public bool UpdateGold = false;

    //Current values
    private int score = 0;
    public int gold = 9;

    //Normal increase rate
    private int scorePerWave = 5;
    private int goldPerEnemy = 1;

    //Turret
    private int turretPrice = 5;

    //Player health
    public int playerHealth = 200;

    //Death pop up
    public GameObject DeathPopUp;

    //Audio
    private GameObject audioMangr;
    bool soundPlayed = false;

    void Start()
    {
        //Audio
        audioMangr = GameObject.FindGameObjectWithTag("Audio");
        goldUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (UpdateScore)
        {
            scoreUpdate();
        }

        if (UpdateGold)
        {
            goldUpdate();
        }
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            Time.timeScale = 0;
            DeathPopUp.SetActive(true);

            //Destroy seetings
            GameObject theSettings = GameObject.FindGameObjectWithTag("Settings");

            //Sound
            if (audioMangr.activeSelf && !soundPlayed)
            {
                inGameAudio gameAudio = audioMangr.GetComponent<inGameAudio>();
                gameAudio.deathSource.Play();
                soundPlayed = true;
            }

            if (theSettings != null)
                Destroy(theSettings);

            float rightTriggerPressed = Input.GetAxis("TriggerAnalogue");
            if (rightTriggerPressed != 0)
            {
                Time.timeScale = 1;
                DeathPopUp.SetActive(false);
                SceneManager.LoadScene("Test", LoadSceneMode.Single);
            }
            float leftTriggerPressed = Input.GetAxis("LeftTriggerAnalogue");
            if (leftTriggerPressed != 0)
            {
                Time.timeScale = 1;
                DeathPopUp.SetActive(false);
                SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
            }
        }

        healthValue.text = playerHealth.ToString();

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
        if (gold >= turretPrice)
        {
            gold -= turretPrice;
            goldValue.text = gold.ToString();
            notifText.text = "Turret bought";
            return true;
        }
        else
        {
            notifText.text = "Not enough gold";
            return false;
        }
    }
}
