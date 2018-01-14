using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControlls : MonoBehaviour {

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
    private float minRotation = -0.5f;
    private float maxRotation = 0.18f;

    // Game Objects
    public GameObject playerObject;
    public GameObject cameraObject;
    private Vector3 cameraLocalOriginVector;
    private Rigidbody rigidb;
    private Transform myTransform;
    public BoxCollider coll;
    public int player = 0;
    private Vector3 playerInput = new Vector3(0, 0, 0);

    // sounds
    public AudioClip jump;
    private AudioSource musicSource;

    //controller
    public static int usedController = 99;
    /**
    * Fire
    * */
    public GameObject pinselPrefab;
    public Transform pinselSpawn;
   // public GameObject playerCamera;
    private List<GameObject> pinselPool = new List<GameObject>();
    private List<GameObject>.Enumerator pinselEnumerator;
    public float shootCooldownSeconds = 100.0f;
    private float lastShot = 0.0f;
    private bool canFire = true;
    private int controllerNumber = 99;
    //Animations
    int attackingHash = Animator.StringToHash("Attacking");
    int walkingHash = Animator.StringToHash("Walking");
    Animator playerAnimator;
    // Use this for initialization
    void Start()
    {
        cameraLocalOriginVector = cameraObject.transform.localPosition;
        playerAnimator = gameObject.GetComponent<Animator>();
        /* alternativ controller Input
         * Checks which controllers are connected
         * sets the two connected controllres to 1 and 2
         * if you add to every input + controllerNumber you don't need every input twice
         * example: "Jumpp"+controllerNumber
         * 
        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();

        //Check whether array contains anything
        if (temp.Length > 0)
        {
            //Iterate over every element
            for (int i = 0; i < temp.Length; ++i)
            {
                //Check if the string is empty or not
                if (!string.IsNullOrEmpty(temp[i]))
                {
                    if(usedController != i && controllerNumber == 99)
                    {
                        usedController = i;
                        controllerNumber = i;
                    }
                    //Not empty, controller temp[i] is connected
                    Debug.Log("Controller " + i + " is connected using: " + temp[i]);
                }
                else
                {
                    //If it is empty, controller i is disconnected
                    //where i indicates the controller number
                    Debug.Log("Controller: " + i + " is disconnected.");

                }
            }
            //Debug.Log("controllerNumber:"+controllerNumber);
        }
        if(controllerNumber == 0 || controllerNumber == 2)
        {
            controllerNumber = 2;
        }
        else if (controllerNumber == 1 || controllerNumber == 3)
        {
            controllerNumber = 1;
        }
        */
        rigidb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        musicSource = GetComponent<AudioSource>();

        myNormal = transform.up; // normal starts as character up direction 
        myTransform = transform;
        lookAtPosition = playerObject.transform.position + new Vector3(0, 0, 10);
        cameraLastUsed = Time.realtimeSinceStartup;

        if (rigidb)
        {
            rigidb.freezeRotation = true;
            distGround = coll.bounds.extents.y - coll.center.y;
        }

        /**
         * 
         * Fire
         * 
         * */
       /* foreach (string bla in Input.GetJoystickNames())
        {
            Debug.Log(bla);
        }
        lastShot = Time.realtimeSinceStartup;
        */
        // prefill Pool
        for (int i = 0; i < 20; i++)
        {
            GameObject pinsel = (GameObject)Instantiate(pinselPrefab);
            pinsel.SetActive(false);
            pinselPool.Add(pinsel);
        }

        pinselEnumerator = pinselPool.GetEnumerator();
        pinselSpawn.position = new Vector3(pinselSpawn.position.x, pinselSpawn.position.y - 1, pinselSpawn.position.z - 1);
    }

    void FixedUpdate()
    {
        rigidb.AddForce(-_gravity * rigidb.mass * myNormal);
    }

    // Update is called once per frame
    void Update()
    {
        if (jumping) // abort Update while jumping to a wall
        {
            return;
        }

        Ray ray;
        RaycastHit hit;

        //  Debug.DrawRay(myTransform.position, myTransform.forward, Color.red, 5.0f);

        bool doJump = false;
        if (player == 0 && isGrounded)
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
            musicSource.clip = jump;
            musicSource.Play();
            rigidb.velocity += jumpSpeed * myNormal;
        }

        if (player == 0)
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
            //looking down
            if (cameraObject.transform.localRotation.x < maxRotation && Input.GetAxis("VerticalViewp1") > 0)
            {
                cameraObject.transform.Rotate(new Vector3(Input.GetAxis("VerticalViewp1") * cameraRotationSpeed, 0, 0));
            }
            //looking up            
            else if (cameraObject.transform.localRotation.x > minRotation && Input.GetAxis("VerticalViewp1") < 0)
            {
                cameraObject.transform.Rotate(new Vector3(Input.GetAxis("VerticalViewp1") * cameraRotationSpeed, 0, 0));
            }
            if (cameraObject.transform.localRotation.x <= -0.2 && Input.GetAxis("VerticalViewp1") < 0)
            {
                if (cameraObject.transform.localPosition.x > 2.215545 && cameraObject.transform.localPosition.y > -1.52335)
                {
                    cameraObject.transform.localPosition = new Vector3(cameraObject.transform.localPosition.x - 0.1f * Mathf.Abs(Input.GetAxis("VerticalViewp1")), cameraObject.transform.localPosition.y - 0.05f * Mathf.Abs(Input.GetAxis("VerticalViewp1")), cameraObject.transform.localPosition.z);
                }
            }
            else /*if (cameraObject.transform.localRotation.x <= -0.3 && cameraObject.transform.localRotation.x >= -0.5 && Input.GetAxis("VerticalViewp1") > 0)*/
            {
                if (cameraObject.transform.localPosition.x < cameraLocalOriginVector.x) {
                    cameraObject.transform.localPosition = new Vector3(cameraObject.transform.localPosition.x + 0.09f * Mathf.Abs(Input.GetAxis("VerticalViewp1")), cameraObject.transform.localPosition.y, cameraObject.transform.localPosition.z);
                }
                if(cameraObject.transform.localPosition.y < cameraLocalOriginVector.y)
                {
                    cameraObject.transform.localPosition = new Vector3(cameraObject.transform.localPosition.x, cameraObject.transform.localPosition.y + 0.05f * Mathf.Abs(Input.GetAxis("VerticalViewp1")), cameraObject.transform.localPosition.z);
                }

            }
           /* if(cameraObject.transform.localRotation.x > -0.1 && cameraObject.transform.localRotation.x < 0.1 && Input.GetAxis("VerticalViewp1") > 0.1 && Input.GetAxis("VerticalViewp1") > -0.1)
            {
                cameraObject.transform.localPosition = cameraLocalOriginVector;
            }*/

            //turn Camera/Player Horizontal
            /*
			if (Input.GetAxis("Horizontalp1") == 0)
			{
				myTransform.Rotate(0, -Input.GetAxis("HorizontalViewp1") * turnSpeed * Time.deltaTime, 0);	
			}
			*/
            myTransform.Rotate(0, -Input.GetAxis("HorizontalViewp1") * turnSpeed * Time.deltaTime, 0);
            /**
            * Fire
            * */
            float timeDelta = (Time.realtimeSinceStartup - lastShot - shootCooldownSeconds);
            if (timeDelta > 0.0f)
            {
                canFire = true;
            }

            if ((Input.GetButton("Fire1") || Input.GetButton("Fire1p1") || Mathf.Abs(Input.GetAxis("Fire1p1")) > 0.5f) && canFire)
            {
                fire();
            }else if(Mathf.Abs(Input.GetAxis("Fire1p1")) < 0.1f)
            {
                playerAnimator.SetTrigger(walkingHash);
                playerAnimator.ResetTrigger(attackingHash);
            }
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
            //looking down
            if (cameraObject.transform.localRotation.x < maxRotation && Input.GetAxis("VerticalViewp2") > 0)
            {
                cameraObject.transform.Rotate(new Vector3(Input.GetAxis("VerticalViewp2") * cameraRotationSpeed, 0, 0));
            }
            //looking up            
            else if (cameraObject.transform.localRotation.x > minRotation && Input.GetAxis("VerticalViewp2") < 0)
            {
                cameraObject.transform.Rotate(new Vector3(Input.GetAxis("VerticalViewp2") * cameraRotationSpeed, 0, 0));
            }
            if (cameraObject.transform.localRotation.x <= -0.2 && Input.GetAxis("VerticalViewp2") < 0)
            {
                if (cameraObject.transform.localPosition.x > 2.215545 && cameraObject.transform.localPosition.y > -1.52335)
                {
                    cameraObject.transform.localPosition = new Vector3(cameraObject.transform.localPosition.x - 0.1f * Mathf.Abs(Input.GetAxis("VerticalViewp2")), cameraObject.transform.localPosition.y - 0.05f * Mathf.Abs(Input.GetAxis("VerticalViewp2")), cameraObject.transform.localPosition.z);
                }
            }
            else /*if (cameraObject.transform.localRotation.x <= -0.3 && cameraObject.transform.localRotation.x >= -0.5 && Input.GetAxis("VerticalViewp1") > 0)*/
            {
                if (cameraObject.transform.localPosition.x < cameraLocalOriginVector.x)
                {
                    cameraObject.transform.localPosition = new Vector3(cameraObject.transform.localPosition.x + 0.09f * Mathf.Abs(Input.GetAxis("VerticalViewp2")), cameraObject.transform.localPosition.y, cameraObject.transform.localPosition.z);
                }
                if (cameraObject.transform.localPosition.y < cameraLocalOriginVector.y)
                {
                    cameraObject.transform.localPosition = new Vector3(cameraObject.transform.localPosition.x, cameraObject.transform.localPosition.y + 0.05f * Mathf.Abs(Input.GetAxis("VerticalViewp2")), cameraObject.transform.localPosition.z);
                }

            }
            //turn Camera/Player Horizontal
            /*
			if (Input.GetAxis("Horizontalp2") == 0)
			{
				myTransform.Rotate(0, -Input.GetAxis("HorizontalViewp2") * turnSpeed * Time.deltaTime, 0);
			}
			*/
            myTransform.Rotate(0, -Input.GetAxis("HorizontalViewp2") * turnSpeed * Time.deltaTime, 0);
            /**
             * Fire
             * */
            float timeDelta = (Time.realtimeSinceStartup - lastShot - shootCooldownSeconds);
            if (timeDelta > 0.0f)
            {
                canFire = true;
            }

            if ((Input.GetButton("Fire1p2") || Mathf.Abs(Input.GetAxis("Fire1p2")) > 0.5f) && canFire)
            {
                fire();
            }
            else if (Mathf.Abs(Input.GetAxis("Fire1p2")) < 0.1f)
            {
                playerAnimator.SetTrigger(walkingHash);
                playerAnimator.ResetTrigger(attackingHash);
            }
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

        /**
         * 
         * Fire
         * 
         * */
      /*  if (player == 1 && controllerNumber == null && (Input.GetButton("Fire1p1")))
        {
            controllerNumber = "p1";
            Debug.Log(controllerNumber);
        }
        else if (player == 0 && controllerNumber == null && (Input.GetButton("Fire1p2")))
        {
            controllerNumber = "p2";
            Debug.Log(controllerNumber);
        }
        //Debug.Log(controllerNumber+"  "+player);
        float timeDelta = (Time.realtimeSinceStartup - lastShot - shootCooldownSeconds);
        if (timeDelta > 0.0f)
        {
            canFire = true;
        }

        if ((Input.GetButton("Fire1") || Mathf.Abs(Input.GetAxis("Fire1")) > 0.5f) && canFire)
        {
            fire();
        }*/
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
        for (float t = 0.0f; t < 1.0;)
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

    private void fire()
    {
        playerAnimator.SetTrigger(attackingHash);
        playerAnimator.ResetTrigger(walkingHash);
        canFire = false;
        lastShot = Time.realtimeSinceStartup;

        /*
		GameObject pinsel = (GameObject)Instantiate(
			pinselPrefab,
			pinselSpawn.position,
			pinselSpawn.rotation);
		*/

        /*
		GameObject pinsel = (GameObject)Instantiate(
			pinselPrefab,
			pinselSpawn.position,
			playerCamera.transform.rotation);
		*/

        if (!pinselEnumerator.MoveNext())
        {
            pinselEnumerator = pinselPool.GetEnumerator();
            pinselEnumerator.MoveNext();
            print("was last");
        }
        if (pinselEnumerator.Current != null)
        {
            GameObject currentPinsel = pinselEnumerator.Current;
            currentPinsel.transform.position = pinselSpawn.position;            
            currentPinsel.transform.rotation = cameraObject.transform.rotation;
            currentPinsel.SetActive(true);

            //Add velocity to the pinsel
            //pinsel.GetComponent<Rigidbody>().velocity = pinsel.transform.forward * 60;
            currentPinsel.GetComponent<Rigidbody>().velocity = currentPinsel.transform.forward * 60;

            //Destroy the pinsel
            //Destroy(pinsel, 5.0f);
        }
    }
}
