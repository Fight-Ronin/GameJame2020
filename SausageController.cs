 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageController : MonoBehaviour
{
    [SerializeField] private float horizontal, vertical;
    [SerializeField] private float horRaw, verRaw;
    private float turnSmoothVelocity;

    [Header("Components")]
    public Rigidbody rb;
    public CapsuleCollider coll;

    [Header("Params")]
    public float moveSpeed;
    public float jumpForce;
    public float jumpMultipier;
    public float holdTime;
    public bool onGround;
    public bool canJump;
    public LayerMask goundLayer;
    public Transform groundChecker;
    public Transform m_cam;

    [Header("Gizmos")]
    public Vector3 groundExtent;
    public Quaternion groundqauter;
    
    void Start()
    {
        canJump = false;
        rb = GetComponent<Rigidbody>();
        coll = GetComponentInChildren<CapsuleCollider>();
        //groundChecker = transform.Find("S_GroundChecker");
        m_cam = GameManager.instance.mainCam.transform;
        EventCenter.GetInstance().AddEventListener<(float hor,float ver)>("SausageAxis", (movement) => {
            horizontal = movement.hor;
            vertical = movement.ver;
        });
        EventCenter.GetInstance().AddEventListener<(float hor, float ver)>("SausageAxisRaw", (direction) => {
            horRaw = direction.hor;
            verRaw = direction.ver;
        });
        EventCenter.GetInstance().AddEventListener("SausageJumpButtonDown",Hold);
        EventCenter.GetInstance().AddEventListener("SausageJumpButtonUp",Jump);
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
            this.transform.rotation = Quaternion.Euler(0f, smoothAngle, 90f);
            Vector3 moveDir = Quaternion.Euler(0f, angle, 90f) * Vector3.forward;
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
            canJump = true;
            rb.isKinematic = true;
        }
        

        // 搞点蓄力动画

    }

    void Jump()
    {
        if(canJump)
        {
            Debug.Log("Jump");
            canJump = false;
            rb.isKinematic = false;
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
        
    }
}
