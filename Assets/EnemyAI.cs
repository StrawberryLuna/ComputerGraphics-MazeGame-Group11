using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float bounceStrength = 10f; // Adjust this as needed
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void BounceOff(Vector3 bounceDirection)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(bounceDirection.normalized * bounceStrength, ForceMode.Impulse);
        }
    }
}
