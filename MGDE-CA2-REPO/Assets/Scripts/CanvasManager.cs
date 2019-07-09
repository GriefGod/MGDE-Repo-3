using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{//This canvas creates a simpleton which guides canvas through all scenes, handles event logics such as scene change, 
    //set UI active and disable UI
    private static CanvasManager Instance;

    public GameObject pauseMenuUI; //pause menu in game level
    public GameObject pauseButtonUI; //button to pause
    public GameObject mainMenuUI; //main menu in start menu
    
    public GameObject gameOverUI; //game over when player is dead 
    public GameObject OptionsMenuUI; //options menu
    public GameObject GyroUnSupportedTxt; //for telling player gyro unsupported
    private GameObject joyStickUI; //joystick
    public TMP_Dropdown controltxt; 

    public bool GameIsPause = false;
    //public GameObject joystickUI;

        //Calibration variables
    public float  offsetCalX =0;
    public float  offsetCalY =0;
    public float  offsetCalZ =0;

    //Calibration variables accelrometer
    public Vector3 calibratedtilt = Vector3.zero;

    //control selection variables
    private bool toggle = true; //check if toggle is true
    private int level = 0; //use to keep track on what is the current level
    private int controlSelect; //what control options did the player use, links to controls in option
    
    
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            print("Instance set");
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }



    public void UpdateLevelChange(int level) //use on buttons that will change scene, excluding play button. Disables menu or pause depedning on scene
    {
        if (level != 0)
        {
            mainMenuUI.SetActive(false);
            pauseButtonUI.SetActive(true);
           
        }
        else
        {
            print("Main Menu : "  + mainMenuUI.activeSelf);
            mainMenuUI.SetActive(true);
            pauseButtonUI.SetActive(false);
            print("Main Menu : " + mainMenuUI.activeSelf);
            pauseMenuUI.SetActive(false);
        }

    }

    public void OPTIONSUpdateLevelChange() //Use this one, on buttons that close and open the option menu. 

    {
        //Note: Turning off option page is already set in Option buttion using Unity setactive.
        if (SceneManager.GetActiveScene().name == "Start Menu" && mainMenuUI.activeSelf == false) //in menu and menuPage is turned off
        {
         
            mainMenuUI.SetActive(true); //turn on menu page and turn off option page
            
        }

        if (SceneManager.GetActiveScene().name != "Start Menu" && pauseMenuUI.activeSelf == false) //in level and pause page is turned off, we want to set opposite
        {
            pauseMenuUI.SetActive(true); //same as above logic


        }
    }

    public  void setGameOverScreen() //sets game over screen to true
    {
        gameOverUI.SetActive(true);
    }

    public void updateSelectedControls(int selection )
    {
 
        controlSelect = selection;
        controltxt.Hide();
        if (selection == 2) { gyroSupportedCheck();  }
    }

    public int checkSelectedControls()
    {
        return controlSelect;
    }

    public void isVibrationOn( bool toggle) //let user check or uncheck vibration
    {
         this.toggle = toggle;
    }

    public bool getVibration() //use to check is virabtion is on;
    {
        return toggle;
    }

    public void gyroSupportedCheck() //check if phone supports gyro scope, if not enable sorry screen saying gyro unsupported
    {
        if (!SystemInfo.supportsGyroscope)
        {
            Destroy(controltxt.transform.Find("Dropdown List").gameObject); //this is to destroy dropdown list to remedy a unity bug where disabling menu will break the dropdown
            GyroUnSupportedTxt.SetActive(true);
            OptionsMenuUI.SetActive(false);
            controltxt.value = 0;
            controlSelect = 0;
            

        }
       
    }

    private Gyroscope gyro; //use class Gyroscope for calibration
    public  void Calibrator() //calibrates  phone to a specfic offset
    {

        if (SystemInfo.supportsGyroscope) // calibaration for  gyro
        {

            gyro = Input.gyro;
            gyro.enabled = true;

            print("Calibrated!!!!!");
            offsetCalX = gyro.attitude.x;
            offsetCalY = gyro.attitude.y;
            offsetCalZ = gyro.attitude.z;
            gyro.enabled = false;
        }

        {//calibration for acclerometer
            calibratedtilt.x = Input.acceleration.x;
            calibratedtilt.y = Input.acceleration.y;
            calibratedtilt.z = Input.acceleration.z;

            print("Cal values" + calibratedtilt.x);
        }
       
    }
   
    public void setTimeScale(int timeScale) //allows setting of desired timescale.
    {
        Time.timeScale = timeScale;
    }

    public void LevelSelection(string levelname) { SceneManager.LoadScene(levelname); } //change level by name

    public void LevelSelection(int levelindex) { SceneManager.LoadScene(levelindex); } //change level by scene index

    public void QuitGame() { Application.Quit(); } //quit app

    public void ReloadScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); pauseMenuUI.SetActive(false); } //reset scne
   

    public void Resume() // Resumes game play
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
        //try
        //{
        //    //joystickUI.SetActive(true);

        //    //GameObject.Find("Variable Joystick").SetActive(true);
        //    joyStickUI.SetActive(true);
        //}
        //catch (System.Exception)
        //{

        //}
        if (controlSelect == 0)
        {
            joyStickUI.SetActive(true);
        }

        pauseButtonUI.SetActive(true);

    }

    public void Pause() // Pauses game play
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
        //joystickUI.SetActive(false);
        //try
        //{
        //    joyStickUI = GameObject.Find("Variable Joystick");
        //    joyStickUI.SetActive(false);
        //}
        //catch (System.Exception)
        //{

        //}
        if (controlSelect == 0)
        {
            joyStickUI = GameObject.Find("Variable Joystick");
            joyStickUI.SetActive(false);
        }


        pauseButtonUI.SetActive(false);

    }

}
