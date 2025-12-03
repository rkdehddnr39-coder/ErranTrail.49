using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Walking_Npc : MonoBehaviour
{
    public Transform target;

    private NavMeshAgent npc;
    private Animator anime;


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

        if (distance > npc.stoppingDistance)
        {
            npc.SetDestination(target.position);
            anime.SetBool("isWalk", true);
        }
        else
        {
            npc.ResetPath();
            anime.SetBool("isWalk", false);
            npc.velocity = Vector3.zero;
        }
    }
}
