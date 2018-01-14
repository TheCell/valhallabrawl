using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	private float moveSpeed = 6f; // move speed
	private float moveSpeedInJump = 10f; // move speed
	private float turnSpeed = 180f; // turning speed (degrees/second)
	private float lerpSpeed = 10f; // smoothing speed
	public const float _gravity = 10f; // gravity acceleration
	private bool isGrounded;
	private float deltaGround = 0.2f; // character is grounded up to this distance
	private float jumpSpeed = 5f; // vertical jump initial speed
	private float jumpRange = 10f; // range to detect target wall
	private Vector3 surfaceNormal; // current surface normal
	private Vector3 myNormal; // character normal
	private float distGround; // distance from character position to ground
	private bool jumping = false; // flag "I'm jumping to wall";
	private float vertSpeed = 0; // vertical jump current speed

	// Camera Variables
	private Vector3 cameraOffset;
	private Vector3 lookAtPosition;
	private float cameraLastUsed;
    private float cameraRotationSpeed = 2f;
	public GameObject lookAtPositionObject;
	private int cameraAccelerationSpeed = 7;
	private float minRotationDegree = -45;
	private float maxRotationDegree = 45;
	private float minRotation = -0.6f;
	private float maxRotation = 0.6f;

	// Game Objects
	public GameObject playerObject;
	public GameObject cameraObject;
    private Transform cameraObjectOriginTransform;
	private Rigidbody rigidb;
	private Transform myTransform;
	public BoxCollider coll;
	public int player = 0;
    private Vector3 playerInput = new Vector3(0, 0, 0);
	// Use this for initialization
	void Start ()
	{
		rigidb = GetComponent<Rigidbody>();
		coll = GetComponent<BoxCollider>();

		myNormal = transform.up; // normal starts as character up direction 
		myTransform = transform;
		lookAtPosition = playerObject.transform.position + new Vector3(0, 0, 10);
		cameraLastUsed = Time.realtimeSinceStartup;

		if (rigidb)
		{
			rigidb.freezeRotation = true;
			distGround = coll.bounds.extents.y - coll.center.y;
		}
	}

	void FixedUpdate()
	{
		rigidb.AddForce(-_gravity * rigidb.mass * myNormal);
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

      //  Debug.DrawRay(myTransform.position, myTransform.forward, Color.red, 5.0f);

		bool doJump = false;
		if(player == 0 && isGrounded)
		{
			doJump = Input.GetButton("Jumpp1");
		}
		else if (player == 1 && isGrounded)
		{
			doJump = Input.GetButton("Jumpp2");
		}

		if (doJump)
		{
			// ray = new Ray(myTransform.position, myTransform.forward);

			/*
			if (Physics.Raycast(ray, out hit, jumpRange))
			{
				jumpToWall(hit.point, hit.normal);
				print("jump to wall");
			}
			else if (isGrounded) // no: if grounded, jump up
			{
				rigidb.velocity += jumpSpeed * myNormal;
			}
			*/
			rigidb.velocity += jumpSpeed * myNormal;
		}

		if(player == 0)
		{
			// move character
			//myTransform.Rotate(0, Input.GetAxis("Horizontalp1") * turnSpeed * Time.deltaTime, 0);
			
			if (isGrounded)
			{
				// forward / backwards
				myTransform.Translate(0, 0, Input.GetAxis("Verticalp1") * moveSpeed * Time.deltaTime);
				// strive 
				myTransform.Translate(Input.GetAxis("Horizontalp1") * moveSpeed * Time.deltaTime, 0, 0);
			}
			else
			{
				// forward / backwards
				myTransform.Translate(0, 0, Input.GetAxis("Verticalp1") * moveSpeedInJump * Time.deltaTime);
				// strive 
				myTransform.Translate(Input.GetAxis("Horizontalp1") * moveSpeedInJump * Time.deltaTime, 0, 0);
			}
			
			// limit looking up and down
			if (cameraObject.transform.localRotation.x < maxRotation && Input.GetAxis("VerticalViewp1") > 0)
			{
				cameraObject.transform.Rotate(new Vector3(Input.GetAxis("VerticalViewp1") * cameraRotationSpeed, 0, 0));
			}
			else if (cameraObject.transform.localRotation.x > minRotation && Input.GetAxis("VerticalViewp1") < 0)
			{
				cameraObject.transform.Rotate(new Vector3(Input.GetAxis("VerticalViewp1") * cameraRotationSpeed, 0, 0));
			}

			//turn Camera/Player Horizontal
			/*
			if (Input.GetAxis("Horizontalp1") == 0)
			{
				myTransform.Rotate(0, -Input.GetAxis("HorizontalViewp1") * turnSpeed * Time.deltaTime, 0);	
			}
			*/
			myTransform.Rotate(0, -Input.GetAxis("HorizontalViewp1") * turnSpeed * Time.deltaTime, 0);
		}
		else if (player == 1)
		{
			// move character
			//myTransform.Rotate(0, Input.GetAxis("Horizontalp2") * turnSpeed * Time.deltaTime, 0);

			if (isGrounded)
			{
				// forward / backwards
				myTransform.Translate(0, 0, Input.GetAxis("Verticalp2") * moveSpeed * Time.deltaTime);
				// strive 
				myTransform.Translate(Input.GetAxis("Horizontalp2") * moveSpeed * Time.deltaTime, 0, 0);
			}
			else
			{
				// forward / backwards
				myTransform.Translate(0, 0, Input.GetAxis("Verticalp2") * moveSpeedInJump * Time.deltaTime);
				// strive 
				myTransform.Translate(Input.GetAxis("Horizontalp2") * moveSpeedInJump * Time.deltaTime, 0, 0);
			}

			//turn Camera Vertical
			// limit looking up and down
			if (cameraObject.transform.localRotation.x < maxRotation && Input.GetAxis("VerticalViewp2") > 0)
			{
				cameraObject.transform.Rotate(new Vector3(Input.GetAxis("VerticalViewp2") * cameraRotationSpeed, 0, 0));
			}
			else if (cameraObject.transform.localRotation.x > minRotation && Input.GetAxis("VerticalViewp2") < 0)
			{
				cameraObject.transform.Rotate(new Vector3(Input.GetAxis("VerticalViewp2") * cameraRotationSpeed, 0, 0));
			}
			
			//turn Camera/Player Horizontal
			/*
			if (Input.GetAxis("Horizontalp2") == 0)
			{
				myTransform.Rotate(0, -Input.GetAxis("HorizontalViewp2") * turnSpeed * Time.deltaTime, 0);
			}
			*/
			myTransform.Rotate(0, -Input.GetAxis("HorizontalViewp2") * turnSpeed * Time.deltaTime, 0);
		}

		ray = new Ray(myTransform.position, -myNormal);
		Debug.DrawRay(ray.origin, ray.direction * 5, Color.red);

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
