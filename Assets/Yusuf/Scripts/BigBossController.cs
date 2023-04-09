using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigBossController : MonoBehaviour
{
    private NavMeshAgent enemyAgent;
    private Animator animator;

    [SerializeField] private GameObject player;

    [SerializeField] private List<string> bossNames;
    [SerializeField] private List<GameObject> bossSkills;

    [SerializeField] private float skillRange;
    [SerializeField] private float skillDistance;

    private float distanceToPlayer;
    private int randomSkill;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        EnemyControl();
    }

    private void EnemyControl()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        print("distanceToPlayer" + distanceToPlayer);

        EnemyAttacking();

        if (distanceToPlayer >= skillDistance && distanceToPlayer <= skillRange)
        {
            EnemyChasing();
        }

        if (distanceToPlayer > skillRange)
        {
            EnemyTurnAround();
        }
    }

    private void EnemyTurnAround()
    {
        if (transform.position.x == 0)
        {
            animator.SetBool("isWalk", false);
            EnemyAttacking();
        }
        else
        {
            for (int i = 0; i < bossNames.Count; i++)
            {
                animator.SetBool("is" + bossNames[i] + "BossSkill", false);
            }

            enemyAgent.SetDestination(Vector3.zero);
            animator.SetBool("isWalk", true);
            enemyAgent.stoppingDistance = 0;
        }

        Debug.Log("Enemy Turn Around");
    }

    private void EnemyChasing()
    {
        enemyAgent.SetDestination(player.transform.position);
        animator.SetBool("isWalk", true);
        Debug.Log("Enemy Chasing");
        enemyAgent.stoppingDistance = 0;
    }

    private void EnemyAttacking()
    {
        if (distanceToPlayer <= skillRange)
        {
            for (int i = 0; i < bossNames.Count; i++)
            {
                animator.SetBool("is" + bossNames[i] + "BossSkill", false);
            }
            transform.LookAt(player.transform.position);
            enemyAgent.stoppingDistance = skillDistance;
            animator.SetBool("isWalk", false);
            animator.SetBool("is" + bossNames[GetRandomIntValue()] + "BossSkill", true);
        }
        Debug.Log("Enemy Attacking");
    }

    private int GetRandomIntValue()
    {
        randomSkill = Random.Range(0, bossNames.Count);
        return randomSkill;
    }

    //Called by animation event
    private void BossSkill(int destroyDelay)
    {
        GameObject skill = Instantiate(bossSkills[GetRandomIntValue()], transform.position, transform.rotation, transform);
        skill.transform.parent = null;
        skill.transform.localScale /= 2;
        Destroy(skill, destroyDelay);
    }
}
