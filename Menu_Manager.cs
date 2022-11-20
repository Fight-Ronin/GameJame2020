using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Manager : MonoBehaviour
{
    public GameObject creditProp;
    public GameObject mainProp;

    public void MenuToGame()
    {
        ScenesManager.GetInstance().LoadScene("GameScene", ()=>{});
    }

    public void Main2Credit()
    {
        creditProp.SetActive(true);
        mainProp.SetActive(false);
    }

    public void Credit2Main()
    {
        creditProp.SetActive(false);
        mainProp.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
