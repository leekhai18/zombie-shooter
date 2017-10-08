using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator anim;
    public Transform target;
    public float speed;
    public Collider radar;

    public int defaulHp = 30;
    public int hp;

    private Rigidbody body;
    private Vector3 direction;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        hp = defaulHp;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (target != null)
        {
            direction = (target.position - transform.position).normalized;
            MoveToDirection(direction);
            RotateToDirection(direction);
            
            if ((target.position - transform.position).sqrMagnitude > 1000)
                target = null;
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            var bullet = collision.gameObject.GetComponent<BulletController>();
            bullet.SetHit();
            hp -= bullet.dame;
            if (hp <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void MoveToDirection(Vector3 direction)
    {
        body.MovePosition(body.position + direction * speed * Time.deltaTime);
    }

    void RotateToDirection(Vector3 direction)
    {        
        Quaternion _lookRotation = Quaternion.LookRotation(direction);        
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 5);
    }
}
