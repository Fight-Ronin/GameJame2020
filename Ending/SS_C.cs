using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_C : MonoBehaviour
{
    void OnTriggerStay(Collider coll)
    {
        if(coll.gameObject.name == "Scissors")
        {
            EventCenter.GetInstance().EventTrigger("ScissorsAC");
        }
    }
}
