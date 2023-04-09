using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private BossSO BossSO;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject punchCollider;

    private NavMeshAgent enemyAgent;
    private Animator animator;

    private string bossName;
    private GameObject bossSkill;
    [HideInInspector] public float damageAmount;
    private float skillCounterAmount;
    private float stoppingDistance;
    private float skillRange;
    private float skillDistance;
    private float hitDistance;

    private float distanceToPlayer;
    private int skillCounter;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        bossName = BossSO.bossName;
        bossSkill = BossSO.skill;
        damageAmount = BossSO.damageAmount;
        skillCounterAmount = BossSO.skillCounterAmount;
        skillRange = BossSO.skillRange;
        skillDistance = BossSO.skillDistance;
        hitDistance = BossSO.hitDistance;
        stoppingDistance = BossSO.stoppingDistance;
    }

    void Update()
    {
        EnemyControl();
    }

    private void EnemyControl()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        print("distanceToPlayer" + distanceToPlayer);

        if (skillCounter != skillCounterAmount)
        {
            EnemyAttacking();
        }

        if (distanceToPlayer >= stoppingDistance && distanceToPlayer <= skillRange)
        {
            EnemyChasing();
        }
       
        if(distanceToPlayer > skillRange || skillCounter == skillCounterAmount)
        {
            EnemyTurnAround();
        }
    }

    private void EnemyTurnAround()
    {
        if (transform.position.x == 0)
        {
            animator.SetBool("isWalk", false);

            if (skillCounter == skillCounterAmount)
            {
                EnemyAttacking();
            }
        }
        else
        {
            animator.SetBool("is" + bossName + "BossSkill", false);
            animator.SetBool("isPunch", false);
            enemyAgent.SetDestination(Vector3.zero);
            animator.SetBool("isWalk", true);
            enemyAgent.stoppingDistance = 0;
        }

        Debug.Log("Enemy Turn Around");
    }

    private void EnemyAttacking()
    {
        if (skillCounter != skillCounterAmount && distanceToPlayer <= hitDistance)
        {
            animator.SetBool("is" + bossName + "BossSkill", false);
            animator.SetBool("isWalk", false);
            enemyAgent.stoppingDistance = hitDistance;
            animator.SetBool("isPunch", true);
        }

        if (skillCounter == skillCounterAmount && distanceToPlayer <= skillRange)
        {
            transform.LookAt(player.transform);
            enemyAgent.stoppingDistance = skillDistance;
            animator.SetBool("is" + bossName + "BossSkill", true);
        }
        Debug.Log("Enemy Attacking");
    }
  
    private void EnemyChasing()
    {
        enemyAgent.SetDestination(player.transform.position);
        animator.SetBool("isWalk", true);
        Debug.Log("Enemy Chasing");
        enemyAgent.stoppingDistance = 0;
    }

    //Called by animation event
    private void BossSkill(int destroyDelay)
    {
        ReturnSkillCounter();
        GameObject skill = Instantiate(bossSkill, transform.position, transform.rotation, transform);
        skill.transform.parent = null;
        Destroy(skill, destroyDelay);
    }

    /*
    //Called by animation event
    private void FireSkill(int destroyDelay)
    {
        ReturnSkillCounter();
        GameObject skill = Instantiate(bossSkill, player.transform.position, transform.rotation, transform);
        skill.transform.parent = null;
        Destroy(skill, destroyDelay);
    }*/

    //Called by animation event
    private void PunchSkill()
    {
        ReturnSkillCounter();
        Debug.Log("skillCounter" + skillCounter);
    }

    private IEnumerator PunchEffect()
    {
        punchCollider.SetActive(true);
        yield return new WaitForSeconds(.05f);
        punchCollider.SetActive(false);
    }

    private int ReturnSkillCounter()
    {
        if (skillCounter < skillCounterAmount)
        {
            skillCounter++;
        }
        else if (skillCounter >= skillCounterAmount)
        {
            skillCounter = 0;
        }

        return skillCounter;
    }
}
