using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Chips;
    public Transform Paper;
    public Transform Sausage;
    public Image SS;
    public Image S;
    public Image C;
    public float alpha = 0.3f;
    public KeyCode activeKey = KeyCode.Alpha3;
    void Start()
    {
        alpha = 0.3f;
        EventCenter.GetInstance().AddEventListener<KeyCode>("KeyDown", (targetKey) => {
            activeKey = targetKey;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(activeKey == KeyCode.Alpha1)
        {            
            GameManager.instance.freeCam.LookAt = Chips;
            GameManager.instance.freeCam.Follow = Chips;
            InputManager.GetInstance().SetActiveMode(1);
            C.color = new Color(C.color.r, C.color.g, C.color.b, 0.9f);
            S.color = new Color(S.color.r, S.color.g, S.color.b, alpha);
            SS.color = new Color(SS.color.r, SS.color.g, SS.color.b, alpha);
            // 打开纸巾的输入控制，关闭其他的
        }
        else if(activeKey == KeyCode.Alpha2)
        {
            GameManager.instance.freeCam.LookAt = Sausage;
            GameManager.instance.freeCam.Follow = Sausage;
            InputManager.GetInstance().SetActiveMode(2);
            C.color = new Color(C.color.r, C.color.g, C.color.b, alpha);
            S.color = new Color(S.color.r, S.color.g, S.color.b, 0.9f);
            SS.color = new Color(SS.color.r, SS.color.g, SS.color.b, alpha);
            // 打开香肠的输入控制，关闭其他的
        }
        else if(activeKey == KeyCode.Alpha3)
        {
            GameManager.instance.freeCam.LookAt = Paper;
            GameManager.instance.freeCam.Follow = Paper;
            InputManager.GetInstance().SetActiveMode(3);
            C.color = new Color(C.color.r, C.color.g, C.color.b, alpha);
            S.color = new Color(S.color.r, S.color.g, S.color.b, alpha);
            SS.color = new Color(SS.color.r, SS.color.g, SS.color.b, 0.9f);
        }
    }
}
