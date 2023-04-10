using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [HideInInspector] public float bossHealth;
    private float skillCounterAmount;
    private float stoppingDistance;
    private float skillRange;
    private float skillDistance;
    private float hitDistance;

    private float distanceToPlayer;
    private int skillCounter;
    private float currentSpeed;
    private Vector3 basePoint;

    [SerializeField] private GameObject gate;
    private bool isDead;

    public Image enemyProgress;

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
        bossHealth = BossSO.bossHealth;

        currentSpeed = enemyAgent.speed;

        basePoint = transform.position;
    }

    void Update()
    {
        if (bossHealth <= 0)
        {
            animator.SetBool("is" + bossName + "BossSkill", false);
            animator.SetBool("isPunch", false);
            animator.SetBool("isHit", false);
            animator.SetBool("isWalk", false);
            animator.SetBool("isDie", true);
            isDead = true;
        }

        enemyProgress.fillAmount = bossHealth / 100;
        print("TESTTTTT" + bossHealth);

        if (isDead)
        {
            gate.SetActive(true);
        }

        if (bossName == "Air")
        {
            Singleton.Instance.isDeadAir = true;
        }
        else if (bossName == "Earth")
        {
            Singleton.Instance.isDeadEarth = true;
        }
        else if (bossName == "Fire")
        {
            Singleton.Instance.isDeadFire = true;
        }
        else if (bossName == "Water")
        {
            Singleton.Instance.isDeadWater = true;
        }


        if (bossHealth > 0)
        {
            EnemyControl();
        }
       
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

        if (distanceToPlayer > skillRange || skillCounter == skillCounterAmount)
        {
            EnemyTurnAround();
        }
    }

    private void EnemyTurnAround()
    {
        if (transform.position.x == basePoint.x)
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
            animator.SetBool("isHit", false);
            enemyAgent.SetDestination(basePoint);
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
            animator.SetBool("isHit", false);
            enemyAgent.stoppingDistance = hitDistance;
            animator.SetBool("isPunch", true);
        }

        if (skillCounter == skillCounterAmount && distanceToPlayer <= skillRange)
        {
            animator.SetBool("is" + bossName + "BossSkill", false);
            animator.SetBool("isPunch", false);
            animator.SetBool("isHit", false);
            transform.LookAt(player.transform);
            enemyAgent.stoppingDistance = skillDistance;
            animator.SetBool("isWalk", false);
            animator.SetBool("is" + bossName + "BossSkill", true);
        }
        Debug.Log("Enemy Attacking");
    }

    private void EnemyChasing()
    {
        enemyAgent.SetDestination(player.transform.position);
        animator.SetBool("isHit", false);
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
        skill.transform.localScale /= 2;
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

    private void EnemyFreeze()
    {
        enemyAgent.speed = 0;
    }
    
    private void EnemyUnfreeze()
    {
        enemyAgent.speed = currentSpeed;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            animator.SetBool("isHit", true);
        }
    }
}
