using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZone : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
            EventCenter.GetInstance().EventTrigger<GameObject>("Reset", coll.gameObject);
    }
}
