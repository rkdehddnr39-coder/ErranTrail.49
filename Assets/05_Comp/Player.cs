using UnityEngine;

public class Player : MonoBehaviour
{
    float hAxis;
    float vAxis;
    public float speed;
    private Rigidbody rb;
    public float rotSpeed;
    private Vector3 moveVec = Vector3.zero;

    public Transform cam;


    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        if (cam == null && Camera.main != null)
        {
            cam = Camera.main.transform;
        }
    }

    private void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 camForward = Vector3.zero;
        Vector3 camRight = Vector3.zero;

        if (cam != null)
        {
            camForward = cam.forward;
            camForward.y = 0f;      // 위/아래 기울기 제거
            camForward.Normalize();

            camRight = cam.right;
            camRight.y = 0f;
            camRight.Normalize();
        }
        else
        {
            // 혹시 cam이 없으면 월드 기준으로라도 움직이게 fallback
            camForward = Vector3.forward;
            camRight = Vector3.right;
        }

        moveVec = (camForward * vAxis + camRight * hAxis).normalized;

        Vector3 move = moveVec * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        if (moveVec.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveVec);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.fixedDeltaTime);
        }
    }
}
