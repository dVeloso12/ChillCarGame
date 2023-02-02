using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehiclePhysics;
using VehiclePhysics.UI;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;
using System;

public class UIController : MonoBehaviour
{
    enum SettingMenuState
    {
        Screen, Sound, Gameplay, None
    }
    SettingMenuState Currentstatemenu;

    [SerializeField] Car_Controller controller;
    [SerializeField] InputMonitor input;
    [SerializeField] Dashboard dash;
    [SerializeField] AidsPanel aids;
    [SerializeField] IgnitionKey igntion;
    [Header("Menu")]
    [SerializeField] MenuSettings data;
    [SerializeField] TMP_Dropdown resolution, graphics;
    [SerializeField] Toggle fullcreen;
    Resolution[] resolutions;
    [SerializeField] AudioMixer Effects, Music;
    [SerializeField] Slider MusicSlider, EffectSlide;

    [SerializeField] GameObject ScreenMenu;
    [SerializeField] GameObject SoundMenu;
    [SerializeField] GameObject GameplayMenu;
    [SerializeField] GameObject Menu;
    bool isActiveMenu;

    private void Start()
    {
        getResolutions();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Load();
    }
    private void Update()
    {
        UpdateCar();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isActiveMenu)
            {
                Menu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                SaveOnFile();
                SaveSettings();
                Cursor.visible = false;
                isActiveMenu = false;
                Time.timeScale = 1;
            }
            else
            {
                Menu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isActiveMenu = true;
                Time.timeScale = 0;
            }
        }
    }
    void UpdateCar()
    {
        VehicleBase vb = controller.currentCar.GetComponent<VehicleBase>();
        igntion.vehicle = vb;
        input.vehicle = vb;
        dash.vehicle = vb;
        aids.vehicle = vb;
    }
    #region Pause Menu
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
    public void SetFullscreen(bool isFull)
    {
        Screen.fullScreen = isFull;
    }
    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
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
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    public void SliderMoving(GameObject obj)
    {
        obj.transform.eulerAngles += new Vector3(0, 0, 20f);
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
            isActiveMenu = true;
        }
        else if (stateToChange == SettingMenuState.Gameplay)
        {
            GameplayMenu.SetActive(true);
            Currentstatemenu = SettingMenuState.Gameplay;
            isActiveMenu = true;
        }
    }
    #endregion
    #region Save
    void SaveOnFile()
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(data.filePath, FileMode.Create)))
        {
            writer.Write("Effect Volume :" + data.EffectVolume);
            writer.Write("Music Volume :" + data.MusicVolume);
            writer.Write("Resolution Width :" + data.Resolution.x);
            writer.Write("Resolution Height :" + data.Resolution.y);
            writer.Write("Fullscreen : " + data.isFullscreen);
            writer.Write("Fov : " + data.FovValue);
        }
    }
    void Load()
    {
        Screen.fullScreen = data.isFullscreen;
        Screen.SetResolution((int)data.Resolution.x, (int)data.Resolution.y, Screen.fullScreen);
        Effects.SetFloat("volume", data.EffectVolume);
        Music.SetFloat("volume", data.MusicVolume);
        QualitySettings.SetQualityLevel(data.QualityIndex);
        float vol = (data.MusicVolume + 80) /20;
        Debug.Log(vol);
        MusicSlider.value = vol;
        vol = (data.EffectVolume + 80) / 20;
        EffectSlide.value = vol;

    }
    void SaveSettings()
    {
        data.isFullscreen = Screen.fullScreen;
        data.QualityIndex = QualitySettings.GetQualityLevel();
        data.Resolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        Effects.GetFloat("volume", out data.EffectVolume);
        Music.GetFloat("volume", out data.MusicVolume);
    }
    #endregion
}
