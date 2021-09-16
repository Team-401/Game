using UnityEngine;
using System.Collections;

public class PrototypeHero : MonoBehaviour {

    public UIHeroHPBar UIHpBar;
    public UIPotion potionUI;
    public float      m_runSpeed = 4.5f;
    public float      m_walkSpeed = 2.0f;
    public float      m_jumpForce = 7.5f;
    public float      m_dodgeForce = 8.0f;
    public float      m_parryKnockbackForce = 4.0f; 
    public bool       m_noBlood = false;
    public bool       m_hideSword = false;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private SpriteRenderer      m_SR;
    private Sensor_Prototype    m_groundSensor;
    private bool                m_grounded = false;
    private bool                m_moving = false;
    private bool                m_dead = false;
    private bool                m_dodging = false;
    private bool                m_wallSlide = false;
    private bool                m_ledgeGrab = false;
    private bool                m_ledgeClimb = false;
    private bool                m_crouching = false;
    private int                 m_facingDirection = 1;
    private float               m_disableMovementTimer = 0.0f;
    private float               m_parryTimer = 0.0f;
    private float               m_respawnTimer = 0.0f;
    private Vector3             m_respawnPosition = Vector3.zero;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_gravity;
    private float stillTimer = 0.58f;
    private int currentHealth;


    // Public Variables
    public float m_maxSpeed = 4.5f;
    public int maxHealth = 100;

    //Attack stuff
    public LayerMask enemyLayer;
    public int attackDamage = 10;

    //Basic Attack
    public float attackRangeBasic = 0.5f;
    public Transform attackPointBasic;
    private int attackDamageBasic = 1;

    //Upwards Attacks
    public float attackRangeUpPrimary = 0.75f;
    public float attackRangeUpSecondary = 0.5f;
    public Transform attackPointUpPrimary;
    public Transform attackPointUpSecondary;
    private int attackDamageUp = 1;

    //Downward Attack
    public float attackRangeDown = 0.5f;
    public Transform attackPointDown;
    private int attackDamageDown = 4;

    private bool _slamming;

    // Use this for initialization
    void Start ()
    {
        currentHealth = maxHealth;

        // Sets the Players Max Health
        UIHpBar.SetMaxHealth(maxHealth);

        m_animator = GetComponentInChildren<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_SR = GetComponentInChildren<SpriteRenderer>();
        m_gravity = m_body2d.gravityScale;

        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Prototype>();
    }

    // Update is called once per frame
    void Update ()
    {
        // Conditions for using health postions and healing from using them
        if (Input.GetKeyDown(KeyCode.E) && potionUI.potionCount > 0 && currentHealth < 100) 
        {
            currentHealth = Mathf.Clamp(potionUI.drinkPotion(currentHealth), 0, 100);
            potionUI.decreasePotion();
            UIHpBar.SetHealth(currentHealth);
        }

        // Decrease death respawn timer 
        m_respawnTimer -= Time.deltaTime;

        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Decrease timer that checks if we are in parry stance
        m_parryTimer -= Time.deltaTime;

        // Decrease timer that disables input movement. Used when attacking
        m_disableMovementTimer -= Time.deltaTime;

        // Respawn Hero if dead
        if (m_dead && m_respawnTimer < 0.0f)
            RespawnHero();

        //If the player is dead ignore the rest of the update function
        if (m_dead)
            return;

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

        // -- Handle input and movement --
        float inputX = 0.0f;

        if (m_disableMovementTimer < 0.0f)
            inputX = Input.GetAxis("Horizontal");

        // GetAxisRaw returns either -1, 0 or 1
        float inputRaw = Input.GetAxisRaw("Horizontal");

        // Check if character is currently moving
        if (Mathf.Abs(inputRaw) > Mathf.Epsilon && Mathf.Sign(inputRaw) == m_facingDirection)
            m_moving = true;
        else
            m_moving = false;
            
        // Swap direction of sprite depending on move direction
        if (inputRaw > 0 && !m_dodging && !m_wallSlide && !m_ledgeGrab && !m_ledgeClimb && m_parryTimer < 0)
        {
            m_SR.flipX = false;
            m_facingDirection = 1;
            Vector2 temp = new Vector2(0.56f, 0.21f);
            Vector2 playerPos = m_body2d.position;
            attackPointBasic.transform.position = temp+playerPos;
        }

        else if (inputRaw < 0 && !m_dodging && !m_wallSlide && !m_ledgeGrab && !m_ledgeClimb && m_parryTimer < 0)
        {
            m_SR.flipX = true;
            m_facingDirection = -1;
            Vector2 temp = new Vector2(-0.56f, 0.21f);
            Vector2 playerPos = m_body2d.position;
            attackPointBasic.transform.position = temp + playerPos;
        }
     
        // SlowDownSpeed helps decelerate the characters when stopping
        float SlowDownSpeed = m_moving ? 1.0f : 0.5f;
        // Set movement

        // REMOVED: !m_ledgeGrab && !m_ledgeClimb &&
        if (!m_dodging && !m_crouching && m_parryTimer < 0.0f)
            m_body2d.velocity = new Vector2(inputX * m_maxSpeed * SlowDownSpeed, m_body2d.velocity.y);

        // Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // Set Animation layer for hiding sword
        int boolInt = m_hideSword ? 1 : 0;
        m_animator.SetLayerWeight(1, boolInt);

        // -- Handle Animations --
        //1: Death
        if (Input.GetKeyDown("x") && !m_dodging)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
            m_respawnTimer = 2.5f;
            m_dead = true;
        }

        //3: Parry & parry stance
        else if (Input.GetMouseButtonDown(1) && !m_dodging && !m_ledgeGrab && !m_ledgeClimb && !m_crouching && m_grounded)
        {
            // Parry
            // Used when you are in parry stance and something hits you
            //if (m_parryTimer > 0.0f)
            //{
            //    m_animator.SetTrigger("Parry");
            //    m_body2d.velocity = new Vector2(-m_facingDirection * m_parryKnockbackForce, m_body2d.velocity.y);
            //}
                
            // Parry Stance
            // Ready to parry in case something hits you
            //else
            //{
                m_animator.SetTrigger("ParryStance");
                m_parryTimer = stillTimer;
            //}
        }

        //4.1: Up Attack
        else if (Input.GetMouseButtonDown(0) && Input.GetKey("w") && !m_dodging && !m_ledgeGrab && !m_ledgeClimb && !m_crouching && m_grounded && m_timeSinceAttack > 0.2f)
        {
            m_animator.SetTrigger("UpAttack");

            // Reset timer
            m_timeSinceAttack = 0.0f;

            // Disable movement 
            m_disableMovementTimer = 0.35f;

            HandleAttack(attackPointUpPrimary, attackRangeUpPrimary, attackDamageUp);
            HandleAttack(attackPointBasic, attackRangeUpSecondary, attackDamageBasic);
        }

        //4.2: Basic Attack
        else if (Input.GetMouseButtonDown(0) && !m_dodging && !m_ledgeGrab && !m_ledgeClimb && !m_crouching && m_grounded && m_timeSinceAttack > 0.2f)
        {
            // Reset timer
            m_timeSinceAttack = 0.0f;

            m_currentAttack++;

            // Loop back to one after second attack
            if (m_currentAttack > 2)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of the two attack animations "Attack1" or "Attack2"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Disable movement 
            m_disableMovementTimer = 0.35f;

            HandleAttack(attackPointBasic, attackRangeBasic, attackDamageBasic);

        }

        //4.3: Air Slam Attack
        else if (Input.GetMouseButtonDown(0) && Input.GetKey("s") && !m_ledgeGrab && !m_ledgeClimb && !m_grounded)
        {
            m_animator.SetTrigger("AttackAirSlam");
            m_body2d.velocity = new Vector2(0.0f, -m_jumpForce);
            m_disableMovementTimer = 0.8f;

            // Reset timer
            m_timeSinceAttack = 0.0f;

            _slamming = true;
        }

        //4.4: Air Attack Up
        else if (Input.GetMouseButtonDown(0) && Input.GetKey("w") && !m_dodging && !m_ledgeGrab && !m_ledgeClimb && !m_crouching && !m_grounded && m_timeSinceAttack > 0.2f)
        {
            Debug.Log("Air attack up");
            m_animator.SetTrigger("AirAttackUp");

            // Reset timer
            m_timeSinceAttack = 0.0f;

            HandleAttack(attackPointUpPrimary, attackRangeUpPrimary, attackDamageUp);
            HandleAttack(attackPointBasic, attackRangeUpSecondary, attackDamageBasic);
        }

        //4.5: Air Basic Attack
        else if (Input.GetMouseButtonDown(0) && !m_dodging && !m_ledgeGrab && !m_ledgeClimb && !m_crouching && !m_grounded && m_timeSinceAttack > 0.2f)
        {
            m_animator.SetTrigger("AirAttack");

            // Reset timer
            m_timeSinceAttack = 0.0f;

            HandleAttack(attackPointBasic, attackRangeBasic, attackDamageBasic);

        }

        // 5: Dodge
        else if (Input.GetKeyDown("left shift") && m_grounded && !m_dodging && !m_ledgeGrab && !m_ledgeClimb)
        {
            m_dodging = true;
            m_crouching = false;
            m_animator.SetBool("Crouching", false);
            m_animator.SetTrigger("Dodge");
            m_body2d.velocity = new Vector2(m_facingDirection * m_dodgeForce, m_body2d.velocity.y);
        }

        /*
        // 6: Throw
        else if(Input.GetKeyDown("f") && m_grounded && !m_dodging && !m_ledgeGrab && !m_ledgeClimb)
        {
            m_animator.SetTrigger("Throw");

            // Disable movement 
            m_disableMovementTimer = 0.20f;
        }
        */

        //9: Jump
        else if (Input.GetButtonDown("Jump") && (m_grounded || m_wallSlide) && !m_dodging && !m_ledgeGrab && !m_ledgeClimb && !m_crouching && m_disableMovementTimer < 0.0f)
        {
            // Check if it's a normal jump or a wall jump
            if(!m_wallSlide)
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            else
            {
                m_body2d.velocity = new Vector2(-m_facingDirection * m_jumpForce / 2.0f, m_jumpForce);
                m_facingDirection = -m_facingDirection;
                m_SR.flipX = !m_SR.flipX;
            }

            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_groundSensor.Disable(0.2f);
        }

        //10: Crouch 
        else if (Input.GetKeyDown("s") && m_grounded && !m_dodging && !m_ledgeGrab && !m_ledgeClimb && m_parryTimer < 0.0f)
        {
            m_crouching = true;
            m_animator.SetBool("Crouching", true);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x / 2.0f, m_body2d.velocity.y);
        }

        //11: Stand from Crouch
        else if (Input.GetKeyUp("s") && m_crouching)
        {
            m_crouching = false;
            m_animator.SetBool("Crouching", false);
        }

        //12: Walk
        else if (m_moving && Input.GetKey(KeyCode.LeftControl))
        {
            m_animator.SetInteger("AnimState", 2);
            m_maxSpeed = m_walkSpeed;
        }

        //13: Run
        else if(m_moving)
        {
            m_animator.SetInteger("AnimState", 1);
            m_maxSpeed = m_runSpeed;
        }

        //14: Idle
        else
            m_animator.SetInteger("AnimState", 0);

        //Slam Attack Area
        //This will make air slam attack do infinite damage
        if (_slamming)
        {
            HandleAttack(attackPointDown, attackRangeDown, attackDamageDown);
        } 

        if (_slamming && m_grounded)
        {
            _slamming = false;
        }
    }

    public void HandleAttack(Transform attackTarget, float attackRange, int damage )
    {
        // Check if enemy in target area
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackTarget.position, attackRange, enemyLayer);

        if (hitEnemies.Length != 0)
        {
            _slamming = false;
        }

        // If so call enemy method somehow
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("The enemy " + enemy.name + " was hit");
            if (enemy.name == "Necromancer")
            {
                enemy.GetComponent<Necromancer>().TakeDamage(damage * attackDamage);
            }
            else if (enemy.name == "Necromancer_Red")
            {
                enemy.GetComponent<NecromancerSecondStage>().TakeDamage(damage * attackDamage);
            }
            else if (enemy.name == "LightBandit" || enemy.name == "HeavyBandit") ;
            {
                enemy.GetComponent<BanditNPC>().TakeDamage(damage*attackDamage);
            }
        }
    }

    // Default dustXoffset is zero
    public void SpawnDustEffect(GameObject dust, float dustXOffset = 0, float dustYOffset = 0)
    {
        if (dust != null)
        {
            // Set dust spawn position
            Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * m_facingDirection, dustYOffset, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // Turn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(m_facingDirection, 1, 1);
        }
    }

    // Called in AE_resetDodge in PrototypeHeroAnimEvents
    public void ResetDodging()
    {
        m_dodging = false;
    }

    public bool IsWallSliding()
    {
        return m_wallSlide;
    }

    public void DisableMovement(float time = 0.0f)
    {
        m_disableMovementTimer = time;
    }

    void RespawnHero()
    {
        transform.position = Vector3.zero;
        m_dead = false;
        m_animator.Rebind();
        currentHealth = maxHealth;
        UIHpBar.SetHealth(currentHealth);
        m_dodging = false;

    }

    public void TakeDamage(int damage)
    {
        Debug.Log(m_parryTimer);
        m_dodging = false;

        if (!m_dead && m_parryTimer <= 0)
        {
            currentHealth -= damage;
            m_crouching = false;

            // Updates the Current Health to display on the UI HP Bar
            UIHpBar.SetHealth(currentHealth);
            
            Debug.Log("Player health at " + currentHealth);
            m_animator.SetTrigger("Hurt");
            if (currentHealth <= 0)
            {
                Die();
            }
        }

    }

    void Die()
    {
        Debug.Log("The player is dead!");
        m_animator.SetBool("noBlood", m_noBlood);
        m_animator.SetTrigger("Death");
        m_respawnTimer = 2.5f;
        //DisableWallSensors();
        m_dead = true;
        m_crouching = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            Die();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPointUpPrimary.position, attackRangeUpPrimary);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPointUpSecondary.position, attackRangeUpSecondary);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPointBasic.position, attackRangeBasic);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPointDown.position, attackRangeDown);
    }
}
