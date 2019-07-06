using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    //Handles points system, pause system and object spawning, death animations;
    
    Point_Coordinator updateUI; //activate scoring script
    Trigger_Checkers trigger;

    public GameObject GameOverScreen; //Display when player dies, game over
    public GameObject  WinScreen; //Display when player wins

    
    public GameObject PlayerControls; //activate scoring script, add player objects
    public GameObject PlayerObjects;//for disabling accelerometer, gyro and joystick scripts
    public GameObject joystickUI; //for disabling and enabling canvas object joystick.
    public TextMeshProUGUI timetext; //displays power up durations
    private CanvasManager controltype; // check control type before game starts

    private float TimeTaken = 0; //controls time taken to bet the game;
    public int powerUpduration; //set duration of power up
    private bool end = false; //end the game and prevents further update in end screen
    private float timeleft = 0; 

    public AudioSource BGM; //Background Music
    void Awake()
    {

        Time.timeScale = 1;
        try {
            controltype = FindObjectOfType<CanvasManager>();
        }
        catch (System.Exception )
        {
            
        controltype.updateSelectedControls(1); 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
     BGM.Play();

     updateUI = GetComponent<Point_Coordinator>();  //initialse Point_Coordinator object
        Debug.Log("Point Coordinator triggered");
        trigger = PlayerObjects.GetComponent<Trigger_Checkers>(); //initialse trigger object
        Debug.Log("Trigger initialise");


     updateUI.UpdateCoinsLeft(); //set grand total coins left

     setPlayerControls(); //check control options before starting game


      
    }

    // Update is called once per frame
    void Update()
    {
     //d   Debug.Log("getpoint() = true");
        if (trigger.getPoint() == true) //when player gets a point
        {
            updateUI.updateScore();
            updateUI.UpdateCoinsLeft();
            trigger.setPoint(false);
        }
       // Debug.Log("getpoint successful");
        if (trigger.getPowerState() == true) //when player has a power up, set a count down
        {
           
            timeleft += Time.deltaTime;
            
           // updateUI.UpdateTime( powerUpduration - timeleft);
            updateUI.UpdateEnergyBar( powerUpduration - timeleft , powerUpduration  );
            if (timeleft >= powerUpduration)
            {
                trigger.setPowerStat(false);
                timeleft = 0;
            }
            if (timeleft <= powerUpduration && trigger.getResetPowerup() == true)
            {
                timeleft = 0;
                trigger.setResetPowerup(false);
            }

        }

        if (trigger.isDeadState() == true) //when player is dead, game over screen activates
        {
            //  print("Player is dead");
            GameOverScreen.SetActive(true);
            Time.timeScale = 0;

            if (controltype.getVibration())
            {
                Debug.Log("Vibrate");
                Handheld.Vibrate();
            }


        }

        if (updateUI.PlayerWins == true && end == false) //when player wins, stops game from running
        {
          //GetComponent<CanvasManager>().disablePauseMenu();
            WinScreen.SetActive(true);
            timetext.text = "Time Taken: " + TimeTaken.ToString("0.00");
            end = true;
            Time.timeScale = 0;
            
        }
        TimeTaken += Time.deltaTime; //time taken throughout the game
        




    }

    public void ReloadScene() // Reloads the Scene
    {
        controltype.ReloadScene();
    }

    public void ToMenu() //Goes back to Start Menu
    {
        controltype.UpdateLevelChange(0);
        controltype.LevelSelection("Start Menu");
        
    }


    public void setPlayerControls() //checks type of control selected and disable/enables scripts
    {
        int type = 0;
        if (controltype != null)
        {
            //take selection type from canvas script, 0 for joystick, 1 for accelerometer, 2 for gyroscope
            type = controltype.checkSelectedControls(); 
            print("Type is " + type);

            switch (type)
            {
                case 0: //disable accelerometer and gyroscope
                    PlayerControls.GetComponent<Accelerometer>().enabled = false;
                    PlayerControls.GetComponent<GyroScope>().enabled= false;
                    PlayerControls.GetComponent<JoystickPlayerExample>().enabled = true;
                    joystickUI.SetActive(true);

                    break;
                case 1: //disable joystick, and gyroscope
                    PlayerControls.GetComponent<Accelerometer>().enabled = true;
                    PlayerControls.GetComponent<GyroScope>().enabled = false;
                    PlayerControls.GetComponent<JoystickPlayerExample>().enabled = false;
                    joystickUI.SetActive(false);
                    break;
                case 2: //disable accelerometer and joystick
                    PlayerControls.GetComponent<Accelerometer>().enabled = false;
                    PlayerControls.GetComponent<GyroScope>().enabled = true;
                    PlayerControls.GetComponent<JoystickPlayerExample>().enabled = false;
                    joystickUI.SetActive(false);
                    break;
               
            }


        }
    }


}
