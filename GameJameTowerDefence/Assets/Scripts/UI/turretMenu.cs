using UnityEngine;
using System.Collections;

public class turretMenu : MonoBehaviour {

    //Buttons
    public GameObject[] buttons;
    int currentButton = 0;
    int previousButton = 0; //for visual purposes

    //Turrets
    public GameObject[] turrets;

    //Joystick sensetivity
    public float sensetivity = 0.5f;

    //Turret info Panels
    public GameObject basicTurretInfo;
    public GameObject snareTurretInfo;
    public GameObject heavyTurretInfo;

    //Turret menu
    public GameObject subMenu;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        onMenuOpen();
    }

    public void onMenuOpen()
    {
        selectButton();
    }

    void selectButton()
    {
        float cursorHor = 0;
        float cursorVert = 0;
        cursorHor = Input.GetAxisRaw("Horizontal");
        cursorVert = Input.GetAxisRaw("Vertical");

        //4 BUTTONS
        if (cursorHor > sensetivity)
        {
            previousButton = currentButton;
            currentButton = 1;
            visualButton("Heavy");
        }

        if (cursorHor < -sensetivity)
        {
            previousButton = currentButton;
            currentButton = 2;
            visualButton("Snare");
        }

        if (cursorVert < -sensetivity)
        {
            previousButton = currentButton;
            currentButton = 0;
            visualButton("Basic");
        }
    }

    void visualButton(string type)
    {
        if (subMenu.activeSelf)
        {

            Renderer rendPrev = buttons[previousButton].GetComponent<Renderer>();
            rendPrev.material.color = Color.white;

            Renderer rend = buttons[currentButton].GetComponent<Renderer>();
            rend.material.color = Color.green;

            if (type == "Basic")
            {
                basicTurretInfo.SetActive(true);
            }
            else
            {
                basicTurretInfo.SetActive(false);
            }

            if (type == "Snare")
            {
                snareTurretInfo.SetActive(true);
            }
            else
            {
                snareTurretInfo.SetActive(false);
            }

            if (type == "Heavy")
            {
                heavyTurretInfo.SetActive(true);
            }
            else
            {
                heavyTurretInfo.SetActive(false);
            }
        }
        else
        {
            heavyTurretInfo.SetActive(false);
            snareTurretInfo.SetActive(false);
            basicTurretInfo.SetActive(false);
        }

    }

    public GameObject getTurret()
    {
        return turrets[currentButton];
    }
}
