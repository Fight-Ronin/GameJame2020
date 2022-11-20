using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsController : MonoBehaviour
{
    [SerializeField] private float horizontal, vertical;
    [SerializeField] private float horRaw, verRaw;
    private float turnSmoothVelocity;

    [Header("Components")]
    public Rigidbody rb;
    public BoxCollider coll;

    [Header("Params")]
    public float moveSpeed;
    public float dashForce;
    public bool onGround;
    public bool canDash;
    public LayerMask goundLayer;
    public Transform groundChecker;
    public Transform m_cam;

    [Header("Gizmos")]
    public Vector3 groundExtent;
    public Quaternion groundqauter;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponentInChildren<BoxCollider>();
        //groundChecker = transform.parent.Find("TP_GroundChecker");
        m_cam = GameManager.instance.mainCam.transform;
        EventCenter.GetInstance().AddEventListener<(float hor,float ver)>("ScissorsAxis", (movement) => {
            horizontal = movement.hor;
            vertical = movement.ver;
        });
        EventCenter.GetInstance().AddEventListener<(float hor, float ver)>("ScissorsAxisRaw", (direction) => {
            horRaw = direction.hor;
            verRaw = direction.ver;
        });
        EventCenter.GetInstance().AddEventListener("ScissorsDashButtonDown",Hold);
        EventCenter.GetInstance().AddEventListener("ScissorsDashButtonUp",Dash);
    }

    void Update()
    {
        onGround = Physics.CheckBox(groundChecker.position, groundExtent, groundqauter, goundLayer);
        Roll();

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundChecker.position, groundExtent);
    }

    void Roll()
    {
        Vector3 motion = Vector3.right * horRaw + Vector3.forward * verRaw;
        Vector3 direction = new Vector3(horRaw, 0f, verRaw).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + m_cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothVelocity, 0.1f);
            this.transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            rb.velocity += moveDir.normalized * moveSpeed * Time.deltaTime;
        }
        else
        {
            rb.velocity = new Vector3(Mathf.Lerp(rb.velocity.x, 0, 0.5f), rb.velocity.y, Mathf.Lerp(rb.velocity.z, 0, 0.5f));
        }
    }


    void Hold()
    {
        Debug.Log("Hold");
        if(onGround)
        {
            canDash = true;
            rb.isKinematic = true;
        }
        

    }

    void Dash()
    {
        if(canDash)
        {
            Debug.Log("Jump");
            canDash = false;
            rb.isKinematic = false;
            Vector3 direction = new Vector3(horRaw, 0f, verRaw).normalized;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + m_cam.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            rb.velocity += moveDir.normalized * dashForce * Time.deltaTime;
        
        }
        
    }
}
