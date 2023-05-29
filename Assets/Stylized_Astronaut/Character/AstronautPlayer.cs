using UnityEngine;
using System.Collections;

namespace AstronautPlayer
{

	public class AstronautPlayer : MonoBehaviour {

		private Animator anim;
		private CharacterController controller;

		public AudioSource winSound;
		public AudioSource backgroundMusic;
		private AudioSource jump_sound;

		// Moving fields
		public float speed = 7.0f;
		public float turnSpeed = 120.0f;
		private Vector3 moveDirection = Vector3.zero;
		public float gravity = 32.0f;
		public float knockback = 3;

		public GameObject winText;

		// Jumping fields
	    [SerializeField]
	    float jumpSpeed = 8;
	    [SerializeField]
	    int nrOfAlowedDJumps = 2; // New vairable
	    int dJumpCounter = 0;     // New variable

		private int numJumps = 0;

		private Vector3 impact = Vector3.zero;

		void Start () {
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
			jump_sound = GetComponent<AudioSource>();
			winText.SetActive(false);
		}

		private void OnTriggerEnter(Collider other)
		{
			//makes enemy dissapear when player collides with them
			if(other.gameObject.CompareTag("Enemy")){
				if (transform.position.y - other.transform.position.y > 1 ){
					// jumped on top
					other.gameObject.SetActive(false); // kill the enemey
				} else {
					// ememy hit player --> knockpack
					impact = moveDirection * -1 * knockback;

					
				}
				
			}
			//makes the win text show up when the player interacts with the flag
			if (other.gameObject.CompareTag("Finish")){
				if(!winSound.isPlaying)
            	{
					winSound.Play();
            	}

				backgroundMusic.Pause();
				winText.SetActive(true);
			}

		}


		void Update ()
		{
			if (Input.GetKey ("w") || Input.GetKey(KeyCode.UpArrow)) {
				anim.SetInteger ("AnimationPar", 1);
				anim?.SetInteger ("Idle", 0);
			}  else {
				anim?.SetInteger ("AnimationPar", 0);
				anim.SetInteger ("Idle", 1);
			}

			if(controller.isGrounded){
				moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
				numJumps = 0;
			}

			// double jump
			if (Input.GetKeyDown(KeyCode.Space) && (numJumps < nrOfAlowedDJumps)) 
			{
				moveDirection.y = jumpSpeed;
				numJumps++;
				jump_sound.Play();
	        }
		
			
			float turn = Input.GetAxis("Horizontal");
			transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
			if (impact != Vector3.zero){
				moveDirection = impact;
				impact = Vector3.zero;
			}

			controller.Move(moveDirection * Time.deltaTime);
			moveDirection.y -= gravity * Time.deltaTime;
		}

	}
}

// using UnityEngine;
// using System.Collections;


// namespace AstronautPlayer
// {
// 	public class AstronautPlayer : MonoBehaviour
// 	{
// 		private Animator anim;

// 	    enum MovementMode
// 	    {
// 	        Platformer,
// 	        Strafe
// 	    }

// 	    [SerializeField]
// 	    private MovementMode _movementMode = MovementMode.Strafe;
// 	    [SerializeField]
// 	    private float _walkSpeed = 3f;
// 	    [SerializeField]
// 	    private float _runningSpeed = 6f;
// 	    [SerializeField]
// 	    private float _gravity = 9.81f;
// 	    [SerializeField]
// 	    private float _gravityPlatformer = -12f;
// 	    [SerializeField]
// 	    private float _jumpSpeed = 3.5f;
// 	    [SerializeField]
// 	    private float _doubleJumpMultiplier = 0.5f;
// 	    [SerializeField]
// 	    private GameObject _cameraRig;

// 	    public float jumpHeight = 1;

// 	    private CharacterController _controller;

// 		private AudioSource jump_sound;

// 	    private float _directionY;
// 	    private float _currentSpeed;

// 	    private bool _canDoubleJump = false;

// 	    public float turnSmoothTime = 0.2f;
// 	    float turnSmoothVelocity;

// 	    public float speedSmoothTime = 0.1f;
// 	    float speedSmoothVelocity;

// 	    private float velocityY;

// 		public float turnSpeed = 400.0f;

// 		public GameObject winText;


// 	    void Start()
// 	    {
// 	        _controller = GetComponent<CharacterController>();
// 	        anim = gameObject.GetComponentInChildren<Animator>();
// 			jump_sound = GetComponent<AudioSource>();

// 			winText.SetActive(false);
	        
// 	    }
		

// 	    void Update()
// 	    {
// 			if(Input.GetKeyDown(KeyCode.Space)){
// 				jump_sound.Play();
// 			}
// 	        if (_movementMode == MovementMode.Strafe)
// 	        {
// 	            MovementStafe();
// 	        }

// 	        if (_movementMode == MovementMode.Platformer)
// 	        {
// 	            MovementPlatformer();
// 	        }


// 	    }


// 	    private void LateUpdate()
// 	    {
// 	        if (IsPlayerMoving() && _movementMode == MovementMode.Strafe)
// 	            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, _cameraRig.transform.localEulerAngles.y, transform.localEulerAngles.z);
// 	    }

// 		private void OnTriggerEnter(Collider other){
// 			//makes enemy dissapear when player collides with them
// 			if(other.gameObject.CompareTag("Enemy")){
// 				other.gameObject.SetActive(false);
// 			}
// 			//makes the win text show up when the player interacts with the flag
// 			if (other.gameObject.CompareTag("Finish")){
// 				winText.SetActive(true);
// 				print("FINISH");
// 			}

// 		}
	

// 	    private bool IsPlayerMoving()
// 	    {
// 	        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
// 	    }

// 	    private void MovementStafe()
// 	    {
// 	        float horizontalInput = Input.GetAxis("Horizontal");
// 	        float verticalInput = Input.GetAxis("Vertical");

// 	        anim.SetBool ("isJumping", false);
// 	        anim.SetBool ("isGrounded", true);
// 	        anim.SetBool ("isFalling", false);

// 	        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

// 	        if (_controller.isGrounded)
// 	        {
// 	            _canDoubleJump = true;

// 	            if (Input.GetButtonDown("Jump"))
// 	            {
// 	                _directionY = _jumpSpeed;
// 	                // anim.SetBool ("isJumping", true);
// 	            }
// 	        }
// 	        else
// 	        {
// 	            if (Input.GetButtonDown("Jump") && _canDoubleJump)
// 	            {
// 	                _directionY = _jumpSpeed * _doubleJumpMultiplier;
// 	                _canDoubleJump = false;
// 	            }
// 	        }

// 	        _directionY -= _gravity * Time.deltaTime;

// 	        moveDirection = transform.TransformDirection(moveDirection);

// 	        bool running = Input.GetKey(KeyCode.LeftShift);
// 	        float targetSpeed = (running) ? _runningSpeed : _walkSpeed;
// 	        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

// 	        moveDirection.y = _directionY;

// 	        _controller.Move(_currentSpeed * Time.deltaTime * moveDirection);

// 	        float turn = Input.GetAxis("Horizontal");
// 			transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);


// 			if (Input.GetKey ("w")) {
// 				anim.SetInteger ("AnimationPar", 1);
// 			}  else {
// 				anim.SetInteger ("AnimationPar", 0);
// 			}

// 	    }

// 	    private void MovementPlatformer()
// 	    {
// 	        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
// 	        Vector2 inputDir = input.normalized;
// 	        bool running = Input.GetKey(KeyCode.LeftShift);

// 	        if (inputDir != Vector2.zero)
// 	        {
// 	            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + _cameraRig.transform.eulerAngles.y;
// 	            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
// 	        }

// 	        float targetSpeed = ((running) ? _runningSpeed : _walkSpeed) * inputDir.magnitude;
// 	        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);



// 	        velocityY += Time.deltaTime * _gravityPlatformer;
// 	        Vector3 velocity = transform.forward * _currentSpeed + Vector3.up * velocityY;

// 	        _controller.Move(velocity * Time.deltaTime);
// 	        _currentSpeed = new Vector2(_controller.velocity.x, _controller.velocity.z).magnitude;

// 	        if (_controller.isGrounded)
// 	        {
// 	            velocityY = 0;
// 	        }

// 	        if (Input.GetKeyDown(KeyCode.Space))
// 	        {
// 	            if (_controller.isGrounded)
// 	            {
// 	                float jumpVelocity = Mathf.Sqrt(-2 * _gravityPlatformer * jumpHeight);
// 	                velocityY = jumpVelocity;
// 	            }
// 	        }
// 	    }
// 	}
// }
