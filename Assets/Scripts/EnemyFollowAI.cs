using UnityEngine;
using UnityEngine.AI; // Important for NavMeshAgent

public class EnemyFollowAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    void Start()
    {
        // Find the player GameObject by tag (ensure your player has the "Player" tag)
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Get the NavMeshAgent component attached to this GameObject
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null)
        {
            // Set the NavMeshAgent's destination to the player's position
            agent.SetDestination(player.position);
        }
    }
}