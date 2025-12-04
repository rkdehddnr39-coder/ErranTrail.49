using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Walking_Npc : MonoBehaviour
{
    public Transform target;

    private NavMeshAgent npc;
    private Animator anime;
    private float acceleration = 10f;

    void Start()
    {
        npc = GetComponent<NavMeshAgent>();
        anime = GetComponent<Animator>();

        transform.position = target.position - target.forward * npc.stoppingDistance;
        transform.rotation = target.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > npc.stoppingDistance + 0.1f )
        {
            npc.SetDestination(target.position);
            anime.SetBool("isWalk", true);
        }
        else
        {
            npc.velocity = Vector3.zero;
            anime.SetBool("isWalk", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            npc.speed = 15f;
            npc.acceleration = 20f;
        }
        else
        {
            npc.speed = 10f;
            npc.acceleration = 10f;
        }
    }
}
