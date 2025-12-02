using UnityEngine;
using UnityEngine.EventSystems;
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

    void Start()
    {
        anime = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();

        anime.SetBool("isWalk",false);
    }
    private void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        anime.SetBool("isWalk", moveVec != Vector3.zero);
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        moveVec = (camForward * vAxis + camRight * hAxis).normalized;
        transform.position += moveVec * speed * Time.deltaTime;

        if (moveVec != Vector3.zero)
        {
            if (Mathf.Sign(transform.forward.x) != Mathf.Sign(moveVec.x) || Mathf.Sign(transform.forward.z) != Mathf.Sign(moveVec.z))
            {
                transform.Rotate(0, 1, 0);
            }
            transform.forward = Vector3.Lerp(transform.forward, moveVec, rotSpeed * Time.deltaTime);
        }
    }
}
