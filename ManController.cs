using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ManController : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent manAgent;
    public Transform targetPos;
    public Transform oriPos;
    void Start()
    {
        manAgent = GetComponent<NavMeshAgent>();
        EventCenter.GetInstance().AddEventListener<Vector3>("Noise", (pos) => {
            Debug.Log("Go Search");
            manAgent.SetDestination(pos);
            Invoke("Back",20f);
        });
    }

    void Back()
    {
        manAgent.SetDestination(oriPos.position);
    }

}
