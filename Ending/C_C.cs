using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_C : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerStay(Collider coll)
    {
        if(coll.gameObject.name == "Chips")
        {
            EventCenter.GetInstance().EventTrigger("ChipsAC");
        }
    }
}
