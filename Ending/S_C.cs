using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_C : MonoBehaviour
{
    void OnTriggerStay(Collider coll)
    {
        if(coll.gameObject.name == "Sausage")
        {
            EventCenter.GetInstance().EventTrigger("SausageAC");
        }
    }
}
