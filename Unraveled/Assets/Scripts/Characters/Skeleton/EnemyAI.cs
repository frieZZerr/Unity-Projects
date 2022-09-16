using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundMask, playerMask;
    public PlayerManager playerManager;

    public Animator animator;
    public CharacterCombat combat;

    //  Patrolling
    public Vector3 walkPoint;
    public float walkPointRange;
    bool walkPointSet;

    //  Attacking
    bool alreadyAttacked;

    //  States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = playerManager.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //  Check if player is in sight/attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        //  3 States
        if (!playerInAttackRange && !playerInSightRange) Patrolling();
        if (!playerInAttackRange && playerInSightRange)  ChasePlayer();
        if (playerInAttackRange && playerInSightRange)   AttackPlayer();
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //  WalkPoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
        }
        else
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Idle", false);
        }

    }

    private void SearchWalkPoint()
    {
        //  Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask)) walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //  Disable enemy's movement
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            //  Attack
            animator.SetTrigger("canAttack");
            combat.Attack(player.GetComponent<CharacterStats>());
            //

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), combat.attackSpeed);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
