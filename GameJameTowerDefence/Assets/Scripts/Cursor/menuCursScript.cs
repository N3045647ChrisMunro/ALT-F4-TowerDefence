using UnityEngine;
using System.Collections;

public class menuCursScript : MonoBehaviour {

    public bool MenuOn = true;

    //Button manipulation
    public GameObject[] buttons;
    public int currentButton;

    //Joystick response
    public float sensetivity = 0.3f;

    //Time interval between joystick inputs
    public float timeInterval = 0.5f;
    private float currentTime = 0;

    //Waves
    public WaveManager waveManager;

    //Audio
    public GameObject audioMangr;

    //Joystick script
    public joystickControl cursorScript;

	// Use this for initialization
	void Start () 
    {
        currentButton = -10;
	}
	
	// Update is called once per frame
    void Update()
    {
        waveManager.spawnNewWave = false;

        if (MenuOn)
        {
            if (currentTime <= 0)
            {
                buttonSelect();
                currentTime = timeInterval;
            }
            else
            {
                currentTime -= Time.deltaTime;
            }

            buttonClick();
        }
    }

    //Current Button
    void buttonSelect()
    {
        float cursorVert = 0;
        cursorVert = Input.GetAxisRaw("Vertical");

        //joystick up
        if (cursorVert < -sensetivity)
        {
            if (currentButton + 1 < buttons.Length)
            {
                currentButton++;
                overMenuButton(currentButton - 1);
            }
            else
            {
                currentButton = 0;
                overMenuButton(buttons.Length - 1);
            }

            //Sound
            //audioManager.clickSource.Play();
        }

        //Jotstick down
        if (cursorVert > sensetivity)
        {
            if (currentButton > 0)                          //can go down
            {
                currentButton--;
                overMenuButton(currentButton + 1);
            }
            else                                               //cant go down
            {
                currentButton = buttons.Length - 1;              //Start from top
                overMenuButton(0);
            }

            //Sound
           // audioMangr.clickSource.Play();
        }
    }

    void overMenuButton(int previousButton)
    {
        if (currentButton >= 0)
        {
            buttons[currentButton].SetActive(false);
            buttons[previousButton].SetActive(true);
        }

    }

    void buttonClick()
    {
        float triggerPressed = Input.GetAxis("TriggerAnalogue");        //Get value from analogue
        if (triggerPressed != 0 && currentButton>=0)                                          //If LT is pressed
        {

            //WAVE SPAWN
            if (buttons[currentButton].name == "waveSpawn")
            {
                waveManager.spawnNewWave = true;

                //Sound
                audioMangr = GameObject.FindGameObjectWithTag("Audio");
                if (audioMangr.activeSelf)
                {
                    inGameAudio gameAudio = audioMangr.GetComponent<inGameAudio>();
                    gameAudio.waveSource.Play();
                }

                MenuOn = true;

                buttons[currentButton].SetActive(true);
            }

            //BUY TURRET
            if (buttons[currentButton].name == "buyTurret")
            {
                cursorScript.buyTurretHit = true;

                buttons[currentButton].SetActive(true);
            }

            //UPDATE TURRET
            if (buttons[currentButton].name == "upgrade")
            {
                cursorScript.upgradeTurretHit = true;

                buttons[currentButton].SetActive(true);
            }

        }

    }

    //If sound settings are manipulated
    //void soundChange()
    //{
    //    if (buttons[currentButton].name == "SoundText")
    //    {
    //        //Get horizontal input
    //        float cursorHor = 0;
    //        cursorHor = Input.GetAxisRaw("Horizontal");

    //        //Change SOUND ON to opposite value and display visual feedback
    //        if (cursorHor < -sensetivity || cursorHor > sensetivity)
    //        {
    //            if (currentSettings.soundOn)
    //            {
    //                currentSettings.soundOn = false;
    //                onSoundText.SetActive(false);
    //                offSoundText.SetActive(true);
    //            }
    //            else
    //            {
    //                currentSettings.soundOn = true;
    //                onSoundText.SetActive(true);
    //                offSoundText.SetActive(false);
    //            }
    //        }
    //    }
    //}
}
