using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class settingsManager : MonoBehaviour {

    //Button manipulation
    public GameObject[] buttons;
    private int currentButton;

    //Joystick response
    public float sensetivity = 0.3f;

    //Time interval between joystick inputs
    public float timeInterval = 0.5f;
    private float currentTime = 0;

    //Setting script 
    settingsGame currentSettings;

    //Sound
    public GameObject onSoundText;
    public GameObject offSoundText;

    //Audio
    private audioScript audioManager;

    // Use this for initialization
    void Start()
    {
        //Initial button
        currentButton = 0;

        //Initial button visual feedback
        //Renderer rend = buttons[currentButton].GetComponent<Renderer>();
        //rend.material.color = Color.green;
        buttons[currentButton].SetActive(false);

        //Setting script 
        currentSettings = GameObject.FindGameObjectWithTag("Settings").GetComponent<settingsGame>();

        //Get Audio manager
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime <= 0)
        {
            buttonSelect();
            soundChange();
            currentTime = timeInterval;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }

        buttonClick();
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
            audioManager.clickSource.Play();
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
            audioManager.clickSource.Play();
        }
    }

    void overMenuButton(int previousButton)
    {
        //Renderer rend = buttons[currentButton].GetComponent<Renderer>();
       // rend.material.color = Color.green;

        //Renderer perviousRend = buttons[previousButton].GetComponent<Renderer>();
       // perviousRend.material.color = Color.white;

        buttons[currentButton].SetActive(false);
        buttons[previousButton].SetActive(true);

    }

    void buttonClick()
    {
        float triggerPressed = Input.GetAxis("TriggerAnalogue");        //Get value from analogue
        if (triggerPressed != 0)                                          //If LT is pressed
        {

            if (buttons[currentButton].name == "Accept")
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    //If sound settings are manipulated
    void soundChange()
    {
        if (buttons[currentButton].name == "SoundText")
        {
            //Get horizontal input
            float cursorHor = 0;
            cursorHor = Input.GetAxisRaw("Horizontal");

            //Change SOUND ON to opposite value and display visual feedback
            if (cursorHor < -sensetivity || cursorHor > sensetivity)
            {
                if (currentSettings.soundOn)
                {
                    currentSettings.soundOn = false;
                    onSoundText.SetActive(false);
                    offSoundText.SetActive(true);
                }
                else
                {
                    currentSettings.soundOn = true;
                    onSoundText.SetActive(true);
                    offSoundText.SetActive(false);
                }
            }
        }
    }
}
