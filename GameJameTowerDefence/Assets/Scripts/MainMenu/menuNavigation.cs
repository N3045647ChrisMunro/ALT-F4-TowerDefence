using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuNavigation : MonoBehaviour {

    public GameObject[] buttons;
    private int currentButton;

    public float sensetivity = 0.3f;

    public float timeInterval = 0.5f;
    private float currentTime = 0;

    //Audio
    private audioScript audioManager;

	// Use this for initialization
	void Start ()
    {
        currentButton = 0;

        //Initial button visual feedback
        buttons[currentButton].SetActive(false);

        //Get Audio manager
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioScript>();
    }
	
	// Update is called once per frame
	void Update ()
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

    //Current Button
    void buttonSelect()
    {
        float cursorVert = 0;
        cursorVert = Input.GetAxisRaw("Vertical");

        //joystick up
        if(cursorVert < -sensetivity)     
        {
            if(currentButton+1<buttons.Length)
            {
                currentButton++;
                overMenuButton(currentButton-1);
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
            if (currentButton  > 0)                          //can go down
            {
                currentButton--;                    
                overMenuButton(currentButton+1);
            }
            else                                               //cant go down
            {
                currentButton = buttons.Length-1;              //Start from top
                overMenuButton(0);
            }

            //Sound
            audioManager.clickSource.Play();
        }
    }

    void overMenuButton(int previousButton)
    {
       // Renderer rend = buttons[currentButton].GetComponent<Renderer>();
       // rend.material.color = Color.green;

       // Renderer perviousRend = buttons[previousButton].GetComponent<Renderer>();
       // perviousRend.material.color = Color.grey;

        buttons[currentButton].SetActive(false);
        buttons[previousButton].SetActive(true);

    }

    void buttonClick()
    {
        float triggerPressed = Input.GetAxis("TriggerAnalogue");        //Get value from analogue
        if (triggerPressed != 0)                                        //If LT is pressed
        {
            if(buttons[currentButton].name== "StartButton")
            {
                SceneManager.LoadScene(1);
            }

            if (buttons[currentButton].name == "SettingsButton")
            {
                SceneManager.LoadScene(2);
            }

            //Sound
            audioManager.selectSource.Play();
        }
    }
}
