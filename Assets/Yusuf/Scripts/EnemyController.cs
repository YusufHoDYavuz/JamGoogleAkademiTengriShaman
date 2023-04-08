using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private BossSO BossSO;
    [SerializeField] private GameObject player;

    private NavMeshAgent enemyAgent;
    private Animator animator;

    private string bossName;
    private GameObject bossSkill;

    private float distanceToPlayer;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        bossName = BossSO.bossName;
        bossSkill = BossSO.skill;
    }

    void Update()
    {
        EnemyControl();
    }

    private void EnemyControl()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer >= enemyAgent.stoppingDistance)
        {
            EnemyChasing();
        }
        else
        {
            animator.SetBool("isWalk", false);
            EnemyAttacking();
        }
    }

    private void EnemyAttacking()
    {
        animator.SetBool("is" + bossName + "BossSkill", true);
    }

    private void EnemyChasing()
    {
        enemyAgent.SetDestination(player.transform.position);
        animator.SetBool("isWalk", true);
    }

    //Called by animation event
    private void EarthSkill()
    {
        GameObject skill = Instantiate(bossSkill, transform.position, transform.rotation, transform);
        Destroy(skill, 5);
    }
}
