using UnityEngine;
using System.Collections;

public class GravityAttractor : MonoBehaviour {

	public float gravity = 0;
	public float rotationSmoothness = 5f;
	public float density = 5.52f;
	private float volume;
	
	public void Start(){
		if(gravity==0){
			if(GetComponent<Collider>().GetType() == typeof(SphereCollider)){
				volume = (GetComponent<Collider>().bounds.size.x * Mathf.PI)/6;
			}else if(GetComponent<Collider>().GetType() == typeof(CapsuleCollider)){
				volume = ((Mathf.PI * GetComponent<Collider>().bounds.size.x * GetComponent<Collider>().bounds.size.y) * GetComponent<Collider>().bounds.size.z)/4;
			}else{
				volume = GetComponent<Collider>().bounds.size.x * GetComponent<Collider>().bounds.size.y * GetComponent<Collider>().bounds.size.z;
			}
			gravity = -((density/10) * volume)/2;
		}
	}

	public void Attract(Transform body, float mutiple = 1) {
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 localUp = body.up;

		body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity * mutiple);

		Quaternion targetRotation = Quaternion.FromToRotation(localUp,gravityUp) * body.rotation;
		body.rotation = Quaternion.Slerp(body.rotation,targetRotation,rotationSmoothness * Time.deltaTime );
	}  

}
