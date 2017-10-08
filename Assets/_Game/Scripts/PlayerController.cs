using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody body;
    public float moveSpeed = 15f;
    public float jumpMaxStrength = 5f;
    public float holdJumpIncrement = .5f;
    public float jumpMilliseconds = 100f;
    public bool holdToJump = false;
    public float jumpStrength = 0f;
    public float airModifier = 5f;
    [HideInInspector]
    public bool grounded = true;

    public int hp = 100;
    public int bullets = 20;

    private Vector3 moveDirection;    
    private float jumpMillisecondsLeft = 0;    
    private float _holdJumpMaxStrength = 0;    
    private float _holdJumpIncrement = 0;
    private float timeShootDelay = 1.0f;
    private float timeShooting;    
    private bool isHoldingJump;
    private bool isJumping;

    [Header("Reference")]
    public Animator anim;
    public Transform gunHolder;
    public EZObjectPools.EZObjectPool bulletPool;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void SetUp()
    {
        isJumping = false;
        grounded = true;
        jumpMillisecondsLeft = jumpMilliseconds;
        if (holdToJump)
        {
            jumpStrength = 0;
        }
        else
        {
            jumpStrength = jumpMaxStrength;
        }
        _holdJumpIncrement = holdJumpIncrement;
        _holdJumpMaxStrength = jumpMaxStrength;
    }

    void Start()
    {
        SetUp();
        anim.SetInteger("WeaponType_int", 1);
    }

    void Update()
    {
        timeShooting -= Time.deltaTime;
        if (bullets > 0)
        {
            if (timeShooting > 0)
            {
                timeShooting -= Time.deltaTime;
            }
            else
            {
                Shoot();
            }
        }                    

        if (Input.GetButton("Jump") || isHoldingJump == true)
        {            
            float timeStep = Time.deltaTime;
            float milliSecondsElapsed = timeStep * 100;
            jumpMillisecondsLeft -= milliSecondsElapsed;
            if (!holdToJump && (jumpMillisecondsLeft > 0))
            {
                Jump();
            }
            else if (jumpMillisecondsLeft > 0)
            {
                float jumpInc = _holdJumpIncrement * timeStep;
                jumpStrength += jumpInc;
                jumpStrength = Mathf.Clamp(jumpStrength, 0, _holdJumpMaxStrength);
            }
        }
        else if (holdToJump && (Input.GetButtonUp("Jump")))
        {           
            Jump();
            jumpStrength = 0;
        }
    }

    void FixedUpdate()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Planet"))
            SetUp();

        if (collision.transform.CompareTag("Crates"))
        {
            bullets += Random.Range(10, 20);
            collision.gameObject.SetActive(false);
        }

        if (collision.transform.CompareTag("Bullet"))
        {
            var bullet = collision.gameObject.GetComponent<BulletController>();
            hp -= bullet.dame;
            bullet.SetHit();
        }
    }    

    public void MoveToDirection(Vector3 direction)
    {
        moveDirection = direction;
        body.MovePosition(body.position + transform.TransformDirection(direction) * (grounded ? moveSpeed : (moveSpeed / airModifier)) * Time.deltaTime);
    }

    public void OnTouchJumpButton()
    {
        if (isJumping)
            return;

        isHoldingJump = true;
    }

    public void OnReleaseJumpButton()
    {
        if (isJumping)
            return;

        isHoldingJump = false;        
        Jump();
        jumpStrength = 0;
    }

    public void Jump()
    {
        isJumping = true;
        body.AddForce(transform.up * jumpStrength + moveDirection * 10, ForceMode.VelocityChange);
        grounded = false;        
    }

    public void Shoot()
    {
        if (timeShooting <= 0)
        {
            timeShooting = timeShootDelay;
            bullets--;

            GameObject bulletObject;
            if (bulletPool.TryGetNextObject(gunHolder.position, gunHolder.rotation, out bulletObject))
            {

            }
        }        
    }

    public void SetMovementAnim(float speed)
    {
        anim.SetFloat("Speed_f", speed);
    }
}
