using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reseter : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Sausage;
    public Transform Chips;
    public Transform Scissors;
    private GameObject tempObject;
    public GameObject CF;
    public GameObject SF;
    void Start()
    {
        EventCenter.GetInstance().AddEventListener<GameObject>("Reset", Reset);
    }

    void Reset(GameObject target)
    {
        tempObject = target;
        if(tempObject.name == "Chips")
            CF.SetActive(true);
        if(tempObject.name == "Sausage")
            SF.SetActive(true);
        Invoke("ResetAction", 2f);
    }
    
    void ResetAction()
    {
        if(tempObject!=null)
        {
            if(tempObject.name == "Chips")
            {
                CF.SetActive(false);
                tempObject.transform.position = Chips.position;
            }
            else if(tempObject.name == "Sausage")
            {
                SF.SetActive(false);
                tempObject.transform.position = Sausage.position;
            }
                
            else if(tempObject.name == "Scissors")
                tempObject.transform.position = Scissors.position;
        }
        tempObject = null;
        
    }
}
