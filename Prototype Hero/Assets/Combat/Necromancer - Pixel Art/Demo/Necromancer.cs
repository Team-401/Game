using UnityEngine;
using System.Collections;

public class Necromancer : MonoBehaviour {

    [SerializeField] float      m_speed = 3f;
    //[SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] bool       m_noBlood = false;

    public UIBossHPBarr        BossHPBarrUI;
    public UIShowBossHPBar     ShowBossHPBarUI;
    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Necromancer  m_groundSensor;
    private BoxCollider2D       m_boxCollider;
    private bool                m_grounded = false;
    private float               m_delayToIdle = 0.0f;

    public int MaxHealth = 100;
    public float detectionRange;
    public float smallestApproachDistance;
    public float attackStartDistance;
    public LayerMask playerLayer;
    public Transform attackPointBasic;
    public float attackHitRange;
    public int attackDamageMelee = 40;
    public float meleeAttackDelay;
    public float timeBetweenAttacks;

    private bool m_isDead =false;
    private int _currentHealth;
    private Transform player;
    private float m_timeSinceMeleeAttack = 2.0f;

    // Use this for initialization
    void Start ()
    {
        BossHPBarrUI.SetBossMaxHealth(MaxHealth);
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Necromancer>();
        m_boxCollider = GetComponent<BoxCollider2D>();

        _currentHealth = MaxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update ()
    {
        BossHPBarrUI.SetBossHealth(_currentHealth);
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
        Vector2 newLocation = Vector2.MoveTowards(this.transform.position, player.position, m_speed * Time.deltaTime);
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        float playerDirection = this.transform.position.x - player.position.x;


        if (m_timeSinceMeleeAttack < 1.2) { }
        else if (playerDirection > 0)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);;
        }
        else if (playerDirection < 0)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        // Swap direction of sprite depending on walk direction

        // Move
        //m_body2d.velocity = new Vector2(playerDirection * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        //m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --
        //Death


        if (m_timeSinceMeleeAttack < 1.2) { }
        //Hurt
        else if (Input.GetKeyDown("q"))
        {
            m_animator.SetTrigger("Hurt");
        }

        //Attack
        else if (distanceFromPlayer < attackStartDistance && m_timeSinceMeleeAttack > timeBetweenAttacks)
        {
            m_timeSinceMeleeAttack = 0.0f;
            Debug.Log("Attack triggered");
            m_animator.SetTrigger("Attack");

            //Call the sword attack slightly after animation starts
            Invoke("HandleAttackSwordHardwired", meleeAttackDelay);
        }

        //Spellcast
        else if (Input.GetMouseButtonDown(1))
        {
            m_animator.SetTrigger("Spellcast");
            Debug.Log("zap");
        }

        //Jump
        //else if (Input.GetKeyDown("space") && m_grounded)
        //{
        //    m_animator.SetTrigger("Jump");
        //    m_grounded = false;
        //    m_animator.SetBool("Grounded", m_grounded);
        //    m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        //    m_groundSensor.Disable(0.2f);
        //}

        //4: Run
        //Trigger if player in range
        else if (distanceFromPlayer < detectionRange && distanceFromPlayer > smallestApproachDistance)
        {
            m_animator.SetInteger("AnimState", 1);

            //This is not animation, this is the actual movement of the player
            transform.position = newLocation;
            //If Hero is in range, display Boss Health Bar on UI 
            ShowBossHPBarUI.showBossHealthBar();
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            /*Debug.Log("idling");*/
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }
    }

    public void TakeDamage(int damage)
    {

        // Put in a text or something like that to show damage over the bandits head
        _currentHealth -= damage;
        Debug.Log("Enemy health at " + _currentHealth);

        m_animator.SetTrigger("Hurt");

        if (_currentHealth <= 0)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
            Die();
        }
    }

    public void HandleAttackSwordHardwired()
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

    void Die()
    {
        m_isDead = true;
        Debug.Log("The enemy is dead!");
        

        Destroy(m_body2d);
        Destroy(m_boxCollider);

        Invoke("cleanupDeath", 2.0f);
        //Hides Boss health bar upon defeat
    }

    void cleanupDeath()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPointBasic.position, attackHitRange);
    }
}
