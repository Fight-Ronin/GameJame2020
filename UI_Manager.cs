using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    public GameObject pauseProp;
    public GameObject sliderProp;
    public GameObject buttonProp;
    public Text mastserSliderText;
    public Text bgmSliderText;
    public Text soundSliderText;
    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            InputManager.GetInstance().SetActive(false);
            pauseProp.SetActive(true);
		    Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }


    public void ContinueButton()
    {
        Time.timeScale = 1;
        pauseProp.SetActive(false);
        InputManager.GetInstance().SetActive(true);
    }

    public void GameToMenuButton()
    {
        //ScenesManager.GetInstance().LoadScene("MenuScene", ()=>{});
        Application.Quit();
    }

    public  void SettingButton()
    {
        buttonProp.SetActive(false);
        sliderProp.SetActive(true);
    }

    public void BackSettingButton()
    {
        buttonProp.SetActive(true);
        sliderProp.SetActive(false);
    }

    public void ChangerBGMSlider(Slider slider)
    {
        float value = slider.value;
        bgmSliderText.text = value.ToString();
        AudioManager.GetInstance().ChangeBGMVolume(value / 100);
        Debug.Log(value);
    }

    public void ChangerMasterSlider(Slider slider)
    {
        float value = slider.value;
        mastserSliderText.text = value.ToString();
        AudioManager.GetInstance().ChangeBGMVolume(value / 100);
        AudioManager.GetInstance().ChangeSoundVolume(value / 100);
        Debug.Log(value);
    }

    public void ChangerSoundSlider(Slider slider)
    {
        float value = slider.value;
        soundSliderText.text = value.ToString();
        AudioManager.GetInstance().ChangeSoundVolume(value  / 100);
        Debug.Log(value);
    }
}
