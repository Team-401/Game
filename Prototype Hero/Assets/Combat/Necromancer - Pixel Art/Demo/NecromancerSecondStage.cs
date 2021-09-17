using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NecromancerSecondStage : MonoBehaviour
{
    [SerializeField] bool m_noBlood = false;
    [SerializeField] private Transform pfBolt;

    public UIBossHPBarr BossHPBarrUI;
    public UIShowBossHPBar ShowBossHPBarUI;
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Necromancer m_groundSensor;
    private BoxCollider2D m_boxCollider;
    private bool m_grounded = false;
    private float m_delayToIdle = 0.0f;

    public int MaxHealth = 100;
    public float detectionRange;
    public float smallestApproachDistance;
    public float attackStartDistance;
    public LayerMask playerLayer;
    public Transform attackPointBasic;
    public int attackDamageMelee = 40;
    public float greenBoltDelay;
    public Transform BoltSpawnPoint;

    private float meleeAttackDelay = 0.67f;
    private float timeBetweenAttacks = 1.5f;
    private float attackHitRange = 1.4f;
    private bool m_isDead = false;
    private int _currentHealth;
    private Transform player;
    private float m_timeSinceMeleeAttack = 2.0f;

    // Use this for initialization
    void Start()
    {
        /*BossHPBarrUI.SetBossMaxHealth(MaxHealth);*/
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Necromancer>();
        m_boxCollider = GetComponent<BoxCollider2D>();

        _currentHealth = MaxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isDead)
        {
            return;
        }

        m_timeSinceMeleeAttack += Time.deltaTime;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // ========== Direction Decisions ==========

        //Calculating direction variables
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        float playerDirection = this.transform.position.x - player.position.x;


        if (m_timeSinceMeleeAttack < 1.2) { }
        else if (playerDirection > 0)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); ;
        }
        else if (playerDirection < 0)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        if (m_timeSinceMeleeAttack < 1.2) { }
        //Hurt
        //else if (Input.GetKeyDown("q"))
        //{
        //    m_animator.SetTrigger("Hurt");
        //}

        //Attack
        else if (distanceFromPlayer < attackStartDistance && m_timeSinceMeleeAttack > timeBetweenAttacks)
        {
            m_timeSinceMeleeAttack = 0.0f;
            Debug.Log("Attack triggered");
            m_animator.SetTrigger("Attack");

            //Call the sword attack slightly after animation starts
            Invoke("HandleAttackSwordHardwired", meleeAttackDelay);
        }

        else if (distanceFromPlayer > attackStartDistance && m_timeSinceMeleeAttack > timeBetweenAttacks)
        {
            m_timeSinceMeleeAttack = 0.0f;
            Debug.Log("Ranged attack triggered");
            m_animator.SetTrigger("Spellcast");

            Invoke("GreenBolt", greenBoltDelay);
            //If Hero is in range, display Boss Health Bar on UI 
            /*ShowBossHPBarUI.showBossHealthBar();*/
        }

        //Spellcast
                else if (Input.GetMouseButtonDown(1))
        {
            m_animator.SetTrigger("Spellcast");
            Debug.Log("zap");
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }
    }

    public void TakeDamage(int damage)
    {

        // Put in a text or something like that to show damage over the bandits head
        _currentHealth -= damage;
        BossHPBarrUI.SetBossHealth(_currentHealth);
        Debug.Log("Enemy health at " + _currentHealth);

        if (m_timeSinceMeleeAttack > 1.2)
        {
            m_animator.SetTrigger("Hurt");
        }

        if (_currentHealth <= 0)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
            Die();
        }
    }

    public void HandleAttackSwordHardwired()
    {
        if (!m_isDead)
        {
            // Check if player in target area
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPointBasic.position, attackHitRange, playerLayer);

            // If so call enemy method somehow
            foreach (Collider2D player in hitPlayers)
            {
                Debug.Log("The player" + player.name + " was hit");
                player.GetComponent<PrototypeHero>().TakeDamage(attackDamageMelee);
            }
        }
    }

    void GreenBolt()
    {
        if (!m_isDead)
        {
            Transform boltTransform = Instantiate(pfBolt, BoltSpawnPoint.position, Quaternion.identity);

            Vector3 flightTrajectory = (player.position - BoltSpawnPoint.position).normalized;

            boltTransform.GetComponent<GreenBoltScript>().Setup(flightTrajectory);
        }
    }

    void Die()
    {
        m_isDead = true;
        Debug.Log("The necromancer is dead!");


        Destroy(m_body2d);
        Destroy(m_boxCollider);

        Invoke("startCredits", 5f);
    }

    void startCredits()
    {
        Debug.Log("Entered CreditsDelay Method");
        SceneManager.LoadScene(3);
    }

/*    void cleanupDeath()
    {
        Destroy(this.gameObject);
    }*/

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    //private void BlastEm(object sender, )
}
