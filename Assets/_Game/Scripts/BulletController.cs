using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody body;
    public float speed;
    public ParticleSystem effect;
    public MeshRenderer meshRenderer;
    public int dame = 10;

    private bool isExplosive;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}

    private void OnEnable()
    {
        meshRenderer.enabled = true;
    }

    void FixedUpdate()
    {
        if (isExplosive == false)
            MoveToDirection(transform.forward);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Enemy"))
    //    {
    //        SetHit();
    //    }
    //}

    public void MoveToDirection(Vector3 direction)
    {
        body.MovePosition(body.position + direction * speed * Time.deltaTime);
    }

    public void SetHit()
    {
        effect.transform.gameObject.SetActive(true);
        effect.Play();
        isExplosive = true;
        meshRenderer.enabled = false;
        Invoke("ReturnPool", 1f);
    }

    void ReturnPool()
    {
        effect.transform.gameObject.SetActive(false);
        isExplosive = false;
        gameObject.SetActive(false);
    }
}
