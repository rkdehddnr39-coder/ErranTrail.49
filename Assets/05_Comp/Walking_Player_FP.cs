    using UnityEngine;

    public class Walking_Player_FP : MonoBehaviour
    {
        public float speed;
        public float returnSpeed;
        private Rigidbody rb;
        private Vector3 moveVec = Vector3.zero;
        private Animator anime;

        public LayerMask maskLayer;
        public float maxDistance;
        void Start()
        {
            anime = GetComponent<Animator>();
            rb = this.GetComponent<Rigidbody>();

            anime.SetBool("isWalk", false);
           returnSpeed = speed;

        }
        private void Update()
        {
            float hAxis = Input.GetAxisRaw("Horizontal");
            float vAxis = Input.GetAxisRaw("Vertical");

            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
            anime.SetBool("isWalk", moveVec != Vector3.zero);

            Vector3 origin = this.transform.position + Vector3.up * 0.7f;
            Vector3 direction = this.transform.forward.normalized;

            Debug.DrawRay(origin, direction * maxDistance, Color.red);

            if (Physics.Raycast(origin, direction, maxDistance, maskLayer))
            {
                this.speed = 0f;
            }

            else
        {
            speed = returnSpeed;
        }

        }
        void FixedUpdate()
        {
        Vector3 move = speed * Time.fixedDeltaTime * transform.TransformDirection(moveVec);

        rb.MovePosition(rb.position + move);
    }
        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Restricted_area"))
            {
                speed = 0f;
            }
        }
    }