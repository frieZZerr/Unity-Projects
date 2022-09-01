using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerMovement movement;
    public TPPMovement tpp;
    public CharacterStats myStats;
    public CharacterCombat combat;
    public Animator animator;
    public GameObject attackRange;

    public Transform cam;
    public Transform player;

    public LayerMask enemyMask;

    float attackCooldown = 1f;
    float comboCooldown  = 3f;
    float animCooldown   = 1.5f;
    int comboCount       = 0;
    public float attack_Range = 3f;

    public bool isAttacking;
    private bool enemyInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        attackCooldown = 0f;
        comboCooldown  = 0f;
        animCooldown   = 0f;
        comboCount     = 0;
        isAttacking    = false;
    }

    // Update is called once per frame
    void Update()
    {
        enemyInAttackRange = Physics.CheckSphere(transform.position, attack_Range, enemyMask);

        attackCooldown -= Time.deltaTime;
        comboCooldown  -= Time.deltaTime;
        animCooldown   -= Time.deltaTime;

        if (attackCooldown <= 0f)
        {
            isAttacking = false;
        }

        if (comboCooldown <= 0f)
        {
            comboCount = 0;
        }

        if(animCooldown <= 0f)
        {
            tpp.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && movement.isGrounded && !isAttacking)
        {
            isAttacking = true;
            tpp.enabled = false;
            player.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);

            //  If enemies are in attack range they take damage
            if (enemyInAttackRange)
            {
                Collider[] hitEnemies = Physics.OverlapSphere(attackRange.transform.position, attack_Range, enemyMask);

                foreach(Collider enemy in hitEnemies)
                {
                    combat.Attack(enemy.GetComponent<CharacterStats>());
                    StartCoroutine(HurtAnimation(enemy));
                }

                if (combat != null)
                {
                    if (comboCount == 0)
                        combat.attackDelay = 0.6f;
                    if (comboCount == 1)
                        combat.attackDelay = 0.8f;
                    if (comboCount == 2)
                        combat.attackDelay = 1f;
                    if (comboCount == 3)
                        combat.attackDelay = 1.2f;
                }
            }

            switch (comboCount)
            {
                default:
                    Attack_1();
                    break;

                case 1:
                    Attack_2();
                    break;

                case 2:
                    Attack_3();
                    break;

                case 3:
                    Attack_4();
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackRange.transform.position, attack_Range);
    }

    //  Enemy hurt animation
    IEnumerator HurtAnimation(Collider collider)
    {
        yield return new WaitForSeconds(combat.attackDelay);

        collider.GetComponent<Animator>().SetTrigger("Hurt");
    }

    void Attack_1()
    {
        animator.SetTrigger("Attack_1");
        attackCooldown = 0.8f * combat.attackSpeed;
        comboCooldown  = 3f;
        animCooldown   = 1.5f * combat.attackSpeed;
        comboCount     = 1;
    }

    void Attack_2()
    {
        animator.SetTrigger("Attack_2");
        attackCooldown = 0.8f * combat.attackSpeed;
        comboCooldown  = 3f;
        animCooldown   = 1.5f * combat.attackSpeed;
        comboCount     = 2;
    }

    void Attack_3()
    {
        animator.SetTrigger("Attack_3");
        attackCooldown = 0.8f * combat.attackSpeed;
        comboCooldown  = 3f;
        animCooldown   = 1.5f * combat.attackSpeed;
        comboCount     = 3;
    }

    void Attack_4()
    {
        animator.SetTrigger("Attack_4");
        attackCooldown = 2f * combat.attackSpeed;
        animCooldown   = 2f * combat.attackSpeed;
        comboCooldown  = 0f;
    }
}
