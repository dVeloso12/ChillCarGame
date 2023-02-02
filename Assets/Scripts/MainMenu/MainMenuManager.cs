using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;
using System;

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
    [SerializeField] RotateCar carR;
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

    [SerializeField] TMP_Dropdown resolution, graphics;
    [SerializeField] Toggle fullcreen;
    Resolution[] resolutions;
    [SerializeField] AudioMixer Effects, Music;

    [SerializeField] string filePath;

    void Start()
    {
        Currentstatemenu = SettingMenuState.None;
        CurrentcarIndex = currentCarAppear.GetComponent<CarIndex>().Index;
        getResolutions();
        data.FovValue = 75;
        //SaveSettings();
        filePath = Application.persistentDataPath + "/gamedata.gd";
        data.filePath = filePath;
        if(!File.Exists(filePath))
        {
            SaveOnFile();
            Debug.Log("File does exist");
        }
        else
        {
            LoadData();
        }
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
            carR.canRotate = true;
        }
       else
        {
            carR.canRotate = false;
        }
        if (onSettings)
        {

            if (Input.GetKey(KeyCode.Escape))
            {
                BackToMainMenu();
            }
        }
    }

    void getResolutions()
    {
        resolutions = Screen.resolutions;


        resolution.ClearOptions();

        int currentRes = 0;
        List<string> op = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            bool check = false;
            foreach (string x in op)
            {
                if (x == option)
                {
                    check = true;
                }
                else check = false;

            }
            if (!check)
            {
                op.Add(option);
            }


            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }
        resolution.AddOptions(op);
        resolution.value = currentRes;
        resolution.RefreshShownValue();
    }
    public void openSettings()
    {
        onSettings = true;
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);

    }
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
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
    public void SetFullscreen(bool isFull)
    {
        Screen.fullScreen = isFull;
       
    }
    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height,Screen.fullScreen);
    }
    public void setVolumeEffects(float vol)
    {
        float volume = vol * 20 - 80;
        Effects.SetFloat("volume", volume);
    }
    public void setVolumeMusic(float vol)
    {
        float volume = vol * 20 - 80;
        Music.SetFloat("volume", volume);
    }
    public void setFov(float value)
    {
        data.FovValue = value;
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
        SaveSettings();
        SaveOnFile();
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
    void SaveSettings()
    {
        data.isFullscreen = Screen.fullScreen;
        data.QualityIndex = QualitySettings.GetQualityLevel();
        data.Resolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        Effects.GetFloat("volume",out data.EffectVolume);
        Music.GetFloat("volume", out data.MusicVolume);
    }
    void SaveOnFile()
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            writer.Write("Effect Volume :" + data.EffectVolume);
            writer.Write("Music Volume :" + data.MusicVolume);
            writer.Write("Resolution Width :" + data.Resolution.x);
            writer.Write("Resolution Height :" + data.Resolution.y);
            writer.Write("Fullscreen : " + data.isFullscreen);
            writer.Write("Fov : " + data.FovValue);
        }
    }
    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                string effect, music, resX, resY, fov;
                string _fullscreen;

                effect = reader.ReadString();
                music = reader.ReadString();
                resX = reader.ReadString();
                resY = reader.ReadString();
                _fullscreen = reader.ReadString();
                fov = reader.ReadString();

                string[] splitData = effect.Split(':');
                data.EffectVolume = (float)Convert.ToDouble (splitData[1].Trim());
                 splitData = music.Split(':');
                data.MusicVolume = (float)Convert.ToDouble(splitData[1].Trim());
                splitData = resX.Split(':');
               float X = (float)Convert.ToDouble(splitData[1].Trim());
                splitData = resY.Split(':');
               float Y = (float)Convert.ToDouble(splitData[1].Trim());
                data.Resolution = new Vector2(X, Y);
                splitData = fov.Split(':');
                data.FovValue = (float)Convert.ToDouble(splitData[1].Trim());
                splitData = _fullscreen.Split(':');
                data.isFullscreen = Convert.ToBoolean(splitData[1].Trim());

            }
        }
        else
        {
            Debug.Log("File does not exist");
          
        }
    }
}

