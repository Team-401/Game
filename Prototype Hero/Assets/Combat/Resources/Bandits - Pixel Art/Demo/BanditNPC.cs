using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BanditNPC : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private BoxCollider2D m_boxCollider;
    private Sensor_Bandit       m_groundSensor;
    private bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;
    private float m_timeSinceAttack = 0.0f;
    private float m_timeSinceDeath = 0.0f;
    private Transform player;

    // Property area for health and such
    public int _MaxHealth;
    int currentHealth;
    public float detectionRange;
    public float attackStartDistance;
    public float attackDelay;

    public Transform attackPointBasic;
    public LayerMask playerLayer;
    public float attackHitRange;
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
        //Die animation
        m_animator.SetTrigger("Death");

        Destroy(m_body2d);
        Destroy(m_boxCollider);

        Invoke("cleanupDeath", 2.0f);
    }

    void cleanupDeath()
    {
        Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {

        m_timeSinceAttack += Time.deltaTime;

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        // Handle movement towards player
        if (distanceFromPlayer < detectionRange && !m_isDead )
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, m_speed * Time.deltaTime);
        }

        // Handle attack
        if (distanceFromPlayer < attackStartDistance && !m_isDead)
        {
            // If enemy attack delay time is reached, attack.
            if (m_timeSinceAttack > attackDelay)
            {
                m_timeSinceAttack = 0.0f;
                Debug.Log("Attack triggered");
                m_animator.SetTrigger("Attack");

                HandleAttack(attackPointBasic, attackHitRange);

            }
        }

        // Enemy movement script toward player

        /*
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if(m_grounded && !m_groundSensor.State()) {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // -- Handle Animations --
        //Death
        if (Input.GetKeyDown("e")) {
            if(!m_isDead)
                m_animator.SetTrigger("Death");
            else
                m_animator.SetTrigger("Recover");

            m_isDead = !m_isDead;
        }
            
        //Hurt
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        //Attack
        else if(Input.GetMouseButtonDown(0)) {
            m_animator.SetTrigger("Attack");
        }

        //Change between idle and combat idle
        else if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded) {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }
        */

        // //Run
        // else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        //     m_animator.SetInteger("AnimState", 2);

        // //Combat Idle
        // else if (m_combatIdle)
        //     m_animator.SetInteger("AnimState", 1);

        // //Idle
        // else
        //     m_animator.SetInteger("AnimState", 0);
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

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);    
    }


}
