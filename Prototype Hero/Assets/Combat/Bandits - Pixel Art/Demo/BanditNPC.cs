using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BanditNPC : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    //[SerializeField] float      m_jumpForce = 7.5f;

    public UICoin coinUI;
    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private BoxCollider2D m_boxCollider;
    private Sensor_Bandit       m_groundSensor;
    private bool                m_grounded = false;
    private bool                m_isDead = false;
    private float m_timeSinceAttack = 0.0f; 
    private Transform player;

    // Bandit Internal Properties
    public int _MaxHealth;
    int currentHealth;
    public float detectionRange;
    public float smallestApproachDistance;
    public float attackStartDistance;

    //Bandit Attack Properties
    public LayerMask playerLayer;
    public Transform attackPointBasic;
    public float swordAttackDelay = 0.3f;
    public float attackHitRange;
    public float timeBetweenAttacks;
    public int attackDamageBasic;


    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_boxCollider = GetComponent<BoxCollider2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        currentHealth = _MaxHealth;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(player.position);
    }

    public void TakeDamage(int damage)
    {

        // Put in a text or something like that to show damage over the bandits head
        currentHealth -= damage;
        Debug.Log("Enemy health at " + currentHealth);

        m_animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        m_isDead = true;
        Debug.Log("The enemy is dead!");

        Destroy(m_body2d);
        Destroy(m_boxCollider);

        Invoke("cleanupDeath", 2.0f);
        coinUI.BanditLoot();
    }

    void cleanupDeath()
    {
        Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {

        // ========== Setting States ==========
        m_timeSinceAttack += Time.deltaTime;

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

        // Swap direction of sprite depending on walk direction
        if (playerDirection < 0)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (playerDirection > 0)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        // ========== Animation Stack ==========

        //1: Death
        if (m_isDead)
        {
            m_animator.SetTrigger("Death");
        }

        //2: Hurt
        // Put it into TakeDamage

        //3: Attack
        // Comditions: Within attackStartDistance range and haven't attacked in timeBetweenAttacks time
        else if (distanceFromPlayer < attackStartDistance && m_timeSinceAttack > timeBetweenAttacks)
        {
            m_timeSinceAttack = 0.0f;
            Debug.Log("Attack triggered");
            m_animator.SetTrigger("Attack");

            //Call the sword attack slightly after animation starts
            Invoke("HandleAttackSwordHardwired", swordAttackDelay) ;
        }

        //4: Run
        //Trigger if player in range
        else if (distanceFromPlayer < detectionRange && distanceFromPlayer > smallestApproachDistance)
        {
            m_animator.SetInteger("AnimState", 2);

            //This is not animation, this is the actual movement of the player
            transform.position = newLocation;

        }

        //5: Combat Idle
        // removed because they only try and attack you, never idle

        //6: Idle
        //If nothing else in the animation stack triggered
        else
        {
            m_animator.SetInteger("AnimState", 0);
        }

        /*
        // ========== Unused ==========

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        
        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded) {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }
        */

    }

    public void HandleAttack(Transform attackTarget, float attackRange )
    {
        // Check if player in target area
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackTarget.position, attackRange, playerLayer);

        // If so call enemy method somehow
        foreach (Collider2D player in hitPlayers)
        {
            Debug.Log("The player" + player.name + " was hit");
            player.GetComponent<PrototypeHero>().TakeDamage(attackDamageBasic);
        }
    }

    //This version exists so we can invoke it
    public void HandleAttackSwordHardwired()
    {
        // Check if player in target area
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPointBasic.position, attackHitRange, playerLayer);

        // If so call enemy method somehow
        foreach (Collider2D player in hitPlayers)
        {
            Debug.Log("The player" + player.name + " was hit");
            player.GetComponent<PrototypeHero>().TakeDamage(attackDamageBasic);
        }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPointBasic.position, attackHitRange);
    }


}
