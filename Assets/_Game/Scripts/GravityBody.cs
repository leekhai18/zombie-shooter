using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
 
	public GravityAttractor attractor;
    public float gravityMutiple;
	private Transform myTransform;

	void Start () {
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
		myTransform = transform;
	}

	void FixedUpdate () {
		if (attractor){
			attractor.Attract(myTransform, gravityMutiple);
		}
	}
	
	void OnTriggerEnter(Collider col){
        GravityAttractor obj = col.GetComponent<GravityAttractor>();
		if(obj){
			attractor = obj;
		}
	}

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Planet"))
            attractor = null;
    }
	
}
