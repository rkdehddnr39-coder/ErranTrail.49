using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements.Experimental;
using static UnityEngine.UI.Image;
public class Walking_Player : MonoBehaviour
{
    float hAxis;
    float vAxis;
    public float speed;
    private Rigidbody rb;
    public float rotSpeed;
    private Vector3 moveVec = Vector3.zero;
    private Animator anime;
    public Transform cam;


    public float maxSpeed = 5f;
    public float accacceleration = 5f;

    public LayerMask maskLayer;
    public float maxDistance;
    void Start()
    {
        anime = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();

        anime.SetBool("isWalk", false);
    }
    private void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        anime.SetBool("isWalk", moveVec != Vector3.zero);

        Vector3 origin = this.transform.position + Vector3.up * 0.7f;
        Vector3 direction = this.transform.forward.normalized;

        Debug.DrawRay(origin, direction * maxDistance, Color.red);

        if (Physics.Raycast(origin, direction, maxDistance, maskLayer))
        {
            this.speed = 0f;
        }

        /*
        direction = this.transform.forward;
        maskLayer = 

        bool Physics.Raycast(Vector3 origin, Vector3 direction, float maxDistance, int maskLayer); 
        */
    }
    void FixedUpdate()
    {

        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        moveVec = (camForward * vAxis + camRight * hAxis).normalized;

        if (hAxis != 0 || vAxis != 0)
        {
            speed = Mathf.Lerp(speed, maxSpeed, accacceleration * Time.deltaTime);
        }

        else
        {
            moveVec = Vector3.zero;
            speed = 0f;
        }
        
        Vector3 nextPosition = rb.position + moveVec * speed * Time.deltaTime;
        rb.MovePosition(nextPosition);

        //transform.position += moveVec * speed * Time.deltaTime;//

        /* if (moveVec != Vector3.zero)
        {
            if (Mathf.Sign(transform.forward.x) != Mathf.Sign(moveVec.x) || Mathf.Sign(transform.forward.z) != Mathf.Sign(moveVec.z))
            {
                transform.Rotate(0, 1, 0);
            }
            transform.forward = Vector3.Lerp(transform.forward, moveVec, rotSpeed * Time.deltaTime);
        } */

        if (moveVec != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVec);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = 10f;
            anime.speed = Mathf.Lerp(anime.speed, 2f, accacceleration * Time.deltaTime);
        }
        else
        {
            maxSpeed = 5f;
            anime.speed = Mathf.Lerp(anime.speed, 1f, accacceleration * Time.deltaTime);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Restricted_area"))
        {
            speed = 0f;
        }
    }
}