using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    enum SettingMenuState
    {
        Screen,Sound,Gameplay,None
    }
    SettingMenuState Currentstatemenu;
    bool onSettings;
    bool isActiveMenu;
    bool onChooseCar;
    [SerializeField]MenuSettings data;
    [Header("MainMenu Stuff")]
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject ScreenMenu;
    [SerializeField] GameObject SoundMenu;
    [SerializeField] GameObject GameplayMenu;
    [SerializeField] GameObject ChooseCarMenu;
    [Header("Camera Animator")]
    [SerializeField] Animator camAnim;
    [Header("Cars")]
    public int CurrentcarIndex;
   [SerializeField] GameObject currentCarAppear;

    void Start()
    {
        Currentstatemenu = SettingMenuState.None;
        CurrentcarIndex = currentCarAppear.GetComponent<CarIndex>().Index;
    }

    private void Update()
    {
       if(onChooseCar)
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                ChooseCarMenu.SetActive(false);
                MainMenu.SetActive(true);  
                onChooseCar = false;
                camAnim.SetBool("isPlay", false);
            }
        }
        if (onSettings)
        {

            if (Input.GetKey(KeyCode.Escape))
            {
                BackToMainMenu();
            }
        }
    }
    // Update is called once per frame

    public void openSettings()
    {
        onSettings = true;
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);

    }
    public void SliderMoving(GameObject obj)
    {
        obj.transform.eulerAngles += new Vector3(0,0,20f);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ChooseCar(GameObject car)
    {
        currentCarAppear.SetActive(false);
        currentCarAppear = car;
        currentCarAppear.SetActive(true);
        CurrentcarIndex = currentCarAppear.GetComponent<CarIndex>().Index;
        data.carIndex = currentCarAppear.GetComponent<CarIndex>().Index;
    }
    public void OnPlay()
    {
        camAnim.SetBool("isPlay", true);
        MainMenu.SetActive(false);
        ChooseCarMenu.SetActive(true);
        onChooseCar = true;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void OnScreen()
    {
        if (isActiveMenu && Currentstatemenu == SettingMenuState.Screen)
        {
            ScreenMenu.SetActive(false);
            Currentstatemenu = SettingMenuState.None;
            isActiveMenu = false;
        }
        else
        {
            CheckWhatScreenIs(SettingMenuState.Screen);
        }
    }
    public void BackToMainMenu()
    {
        SettingsMenu.SetActive(false);
        MainMenu.SetActive(true);
        onSettings = false;
    }
    public void onSound()
    {
        if (isActiveMenu && Currentstatemenu == SettingMenuState.Sound)
        {
            SoundMenu.SetActive(false);
            Currentstatemenu = SettingMenuState.None;
            isActiveMenu = false;
        }
        else
        {
            CheckWhatScreenIs(SettingMenuState.Sound);
        }
            
    }
    public void onGameplay()
    {
        if (isActiveMenu && Currentstatemenu == SettingMenuState.Gameplay)
        {
            GameplayMenu.SetActive(false);
            Currentstatemenu = SettingMenuState.None;
            isActiveMenu = false;
        }
        else
        {
            CheckWhatScreenIs(SettingMenuState.Gameplay);
        }
    }
    void CheckWhatScreenIs(SettingMenuState stateToChange)
    {
            if (Currentstatemenu == SettingMenuState.Screen)
            {
                ScreenMenu.SetActive(false);
            }
            else if (Currentstatemenu == SettingMenuState.Sound)
            {
                SoundMenu.SetActive(false);
            }
            else if (Currentstatemenu == SettingMenuState.Gameplay)
            {
                GameplayMenu.SetActive(false);
            }

            if (stateToChange == SettingMenuState.Screen)
            {
                ScreenMenu.SetActive(true);
                Currentstatemenu = SettingMenuState.Screen;
                isActiveMenu = true;

            }
            else if (stateToChange == SettingMenuState.Sound)
            {
                SoundMenu.SetActive(true);
                Currentstatemenu = SettingMenuState.Sound;
                isActiveMenu=true;
            }
            else if (stateToChange == SettingMenuState.Gameplay)
            {
                GameplayMenu.SetActive(true);
                Currentstatemenu = SettingMenuState.Gameplay;
                isActiveMenu = true;
            }
    }
     
    
}

