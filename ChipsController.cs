using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChipsController : MonoBehaviour
{
    // Start is called before the first frame update[SerializeField] private float horizontal, vertical;
    [SerializeField] private float horizontal, vertical;
    [SerializeField] private float horRaw, verRaw;
    private float turnSmoothVelocity;

    [Header("Components")]
    public Rigidbody rb;
    public BoxCollider coll;
    [SerializeField] private Ray ray;

    [Header("Params")]
    public float moveSpeed;
    public float maxVelocityThreshold;
    public float maxVelocityY;
    public float jumpForce;
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
        maxVelocityY = 0;
        rb = GetComponent<Rigidbody>();
        coll = GetComponentInChildren<BoxCollider>();
        //groundChecker = transform.parent.Find("TP_GroundChecker");
        m_cam = GameManager.instance.mainCam.transform;
        EventCenter.GetInstance().AddEventListener<(float hor,float ver)>("ChipsAxis", (movement) => {
            horizontal = movement.hor;
            vertical = movement.ver;
        });
        EventCenter.GetInstance().AddEventListener<(float hor, float ver)>("ChipsAxisRaw", (direction) => {
            horRaw = direction.hor;
            verRaw = direction.ver;
        });
        EventCenter.GetInstance().AddEventListener("ChipsJumpButtonDown",Jump);
    }

    void Update()
    {
        
        if(rb.velocity.y < maxVelocityY)
            maxVelocityY = rb.velocity.y;
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
            AudioManager.GetInstance().PlaySound("roll.mp3",(x)=>{
              x.Play();  
            });
        }
        else
        {
            rb.velocity = new Vector3(Mathf.Lerp(rb.velocity.x, 0, 0.5f), rb.velocity.y, Mathf.Lerp(rb.velocity.z, 0, 0.5f));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Chip" && !onGround)
        {
            Debug.Log("Shit!");
            ContactPoint contact = collision.contacts[0];
            Vector3 pos = contact.point;
            // AudioManager
            // FindEvent
            EventCenter.GetInstance().EventTrigger<Vector3>("Noise", pos);
        } 
        else if(collision.gameObject.tag != "Chip" && maxVelocityY < maxVelocityThreshold)
        {
            Debug.Log("Dead");
            EventCenter.GetInstance().EventTrigger<GameObject>("Reset", this.gameObject);
        }
        maxVelocityY = 0f;
    }

    void Jump()
    {

        if(onGround)
        {
            Debug.Log("Jump");
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }
    
}
