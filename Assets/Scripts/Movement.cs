using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	private float moveSpeed = 6f; // move speed
	private float turnSpeed = 180f; // turning speed (degrees/second)
	private float lerpSpeed = 10f; // smoothing speed
	private float gravity = 10f; // gravity acceleration
	private bool isGrounded;
	private float deltaGround = 0.2f; // character is grounded up to this distance
	private float jumpSpeed = 10f; // vertical jump initial speed
	private float jumpRange = 10f; // range to detect target wall
	private Vector3 surfaceNormal; // current surface normal
	private Vector3 myNormal; // character normal
	private float distGround; // distance from character position to ground
	private bool jumping = false; // flag "I'm jumping to wall";
	private float vertSpeed = 0; // vertical jump current speed

	private Rigidbody rigidb;
	private Transform myTransform;
	public BoxCollider coll;

	// Use this for initialization
	void Start ()
	{
		rigidb = GetComponent<Rigidbody>();
		coll = GetComponent<BoxCollider>();

		myNormal = transform.up; // normal starts as character up direction 
		myTransform = transform;

		if (rigidb)
		{
			rigidb.freezeRotation = true;
			distGround = coll.bounds.extents.y - coll.center.y;
		}
	}

	void FixedUpdate()
	{
		rigidb.AddForce(-gravity * rigidb.mass * myNormal);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (jumping) // abort Update while jumping to a wall
		{
			return;
		}

		Ray ray;
		RaycastHit hit;

		Debug.DrawRay(myTransform.position, myTransform.forward, new Color(1.0f, 0.0f, 0.0f));

		if (Input.GetButton("Jump"))
		{
			ray = new Ray(myTransform.position, myTransform.forward);
			
			
			if (Physics.Raycast(ray, out hit, jumpRange))
			{
				jumpToWall(hit.point, hit.normal);
				print("jump to wall");
			}
			else if (isGrounded) // no: if grounded, jump up
			{
				rigidb.velocity += jumpSpeed * myNormal;
			}
		}

		myTransform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
		ray = new Ray(myTransform.position, -myNormal);
		if (Physics.Raycast(ray, out hit))
		{
			isGrounded = hit.distance <= distGround + deltaGround;
			surfaceNormal = hit.normal;
		}
		else
		{
			isGrounded = false;
			// assume usual ground normal to avoid "falling forever"
			surfaceNormal = Vector3.up;
		}

		myNormal = Vector3.Lerp(myNormal, surfaceNormal, lerpSpeed * Time.deltaTime);
		Vector3 myForward = Vector3.Cross(myTransform.right, myNormal);
		Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);
		myTransform.rotation = Quaternion.Lerp(myTransform.rotation, targetRot, lerpSpeed * Time.deltaTime);
		myTransform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
	}

	private void jumpToWall(Vector3 point, Vector3 normal)
	{
		jumping = true;

		rigidb.isKinematic = true; // disable physics while jumping
		Vector3 originalPosition = myTransform.position;
		Quaternion originalRotation = myTransform.rotation;
		Vector3 distantPosition = point + normal * (distGround + 0.5f);
		Vector3 myForward = Vector3.Cross(myTransform.right, normal);
		Quaternion distantRotation = Quaternion.LookRotation(myForward, normal);

		StartCoroutine(jumpTime(originalPosition, originalRotation, distantPosition, distantRotation, normal));
	}

	private IEnumerator jumpTime(Vector3 originalPosition, Quaternion originalRotation, Vector3 distantPosition, Quaternion distantRotation, Vector3 normal)
	{
		for (float t = 0.0f; t < 1.0; )
		{
			t += Time.deltaTime;
			myTransform.position = Vector3.Lerp(originalPosition, distantPosition, t);
			myTransform.rotation = Quaternion.Slerp(originalRotation, distantRotation, t);
			yield return null; // return here next frame
		}

		myNormal = normal; // update myNormal
		rigidb.isKinematic = false; // enable physics
		jumping = false; // jumping to wall finished
	}
	
}
