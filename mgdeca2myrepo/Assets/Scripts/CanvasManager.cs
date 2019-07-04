using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{//This canvas creates a simpleton which guides canvas through all scenes, handles event logics such as scene change, 
    //set UI active and disable UI
    private static CanvasManager Instance;

    public GameObject pauseMenuUI;
    public GameObject pauseButtonUI;
    public GameObject mainMenuUI;
    
    public GameObject gameOverUI;
    public GameObject OptionsMenuUI;
    public GameObject SryText; //for telling player gyro unsupported
    private GameObject joystickui;
    public TMP_Dropdown controltxt;

    public string hello;
    public bool GameIsPause = false;
    //public GameObject joystickUI;

    public float  offsetCalX =0;
    public float  offsetCalY =0;
    public float  offsetCalZ =0;

    private bool toggle = true; //check if toggle is true
    private int level = 0; //use to keep track on what is the current level
    private int controlSelect; //what control options did the player use, links to controls in option

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
        //joystickUI.SetActive(true);
        Debug.Log("failed to set joystick to inactive");
        //GameObject.Find("Variable Joystick").SetActive(true);
        joystickui.SetActive(true);
        Debug.Log("Joystick active");

        pauseButtonUI.SetActive(true);
        Debug.Log("pause button shown");
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
        //joystickUI.SetActive(false);
        joystickui = GameObject.Find("Variable Joystick");
        

            joystickui.SetActive(false);
        

        Debug.Log("Joystick inactive");

        pauseButtonUI.SetActive(false);
        Debug.Log("pause button hidden");
    }
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
            
            mainMenuUI.SetActive(true);
            pauseButtonUI.SetActive(false);
           
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

    public  void setGameOverScreen()
    {
        gameOverUI.SetActive(true);
    }

    public void updateSelectedControls(int selection )
    {
 
        controlSelect = selection;
        controltxt.Hide();
        if (selection == 2) { gyroSurpportedCheck();  }
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

    public void gyroSurpportedCheck() //check if phone supports gyro scope, if not enable sory screen
    {
        if (!SystemInfo.supportsGyroscope)
        {
            Destroy(controltxt.transform.Find("Dropdown List").gameObject); //this is to destroy dropdown list to remedy a unity bug where disabling menu will break the dropdown
            SryText.SetActive(true);
            OptionsMenuUI.SetActive(false);
            controltxt.value = 0;
            controlSelect = 0;
            

        }
       
    }

    private Gyroscope gyro; //use class Gyroscope for calibration
    public  void Calibrator() //calibrates  phone to a specfic offset
    {

        if (SystemInfo.supportsGyroscope)
        {

            gyro = Input.gyro;
            gyro.enabled = true;

            print("Calibrated!!!!!");
            offsetCalX = gyro.attitude.x;
            offsetCalY = gyro.attitude.y;
            offsetCalZ = gyro.attitude.z;
            gyro.enabled = false;
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
    /*-----------------------------Rmb to use active scenes to check for loaded scene in next code revision
     *  Current build, uses a in class integer to keep track of scene change which will not be very good when there
     *  are multiple methods to switch scenes that are not all functioning in the class.
     *  Rmb to add exception to not only main menu but also selection screen. 
     *  Yay.
     * -----------------------------------*/
}
