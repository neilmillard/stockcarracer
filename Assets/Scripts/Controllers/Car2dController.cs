using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car2dController : MonoBehaviour {

	float speedForce = 10f;
	float turnforce = -200f;
	float driftFactorGrip = 0.9f;
	float driftFactorSkid = 1f;
	float maxGripVelocity = 4.5f;
	float minGripVelocity = 1.0f; // when the skid stops

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();

		Debug.Log (RightVelocity ().magnitude);

		float driftFactor = driftFactorGrip;
		if (RightVelocity ().magnitude > maxGripVelocity) {
			driftFactor = driftFactorSkid;
		}

		if (Input.GetButton ("Accelerate")) {
			rb.AddForce ( transform.up * speedForce );
		}
			
		if (Input.GetButton ("Brake")) {
			float speedChange = -speedForce;
			if (ForwardVelocity ().magnitude < 0f) {
				speedChange = speedChange / 2;
			}
			rb.AddForce (transform.up * speedChange);
		}

		// angular velocity only effective above ForwardVelocity
		float tf = Mathf.Lerp(0, turnforce, rb.velocity.magnitude /2 );
		rb.angularVelocity = Input.GetAxis ("Horizontal" ) * tf;

		rb.velocity = ForwardVelocity () + RightVelocity()*driftFactor ;
	}

	Vector2 ForwardVelocity() {
	// how much rigidbody velocity is in the model up direction
		return transform.up * Vector2.Dot ( GetComponent<Rigidbody2D>().velocity, transform.up );
	}

	Vector2 RightVelocity() {
		// how much rigidbody velocity is in the model right direction
		return transform.right * Vector2.Dot ( GetComponent<Rigidbody2D>().velocity, transform.right );
	}
}
