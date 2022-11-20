using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour
{
    private void Start()
    {
        InputManager.GetInstance().SetActive(false);
        Invoke("shutdown", 18f);
    }
    public void shutdown()
    {
        this.gameObject.SetActive(false);
        InputManager.GetInstance().SetActive(true);
    }
}
