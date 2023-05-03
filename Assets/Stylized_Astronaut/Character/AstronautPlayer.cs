using UnityEngine;
using System.Collections;


namespace AstronautPlayer
{
	public class AstronautPlayer : MonoBehaviour
	{
		private Animator anim;

	    enum MovementMode
	    {
	        Platformer,
	        Strafe
	    }

	    [SerializeField]
	    private MovementMode _movementMode = MovementMode.Strafe;
	    [SerializeField]
	    private float _walkSpeed = 3f;
	    [SerializeField]
	    private float _runningSpeed = 6f;
	    [SerializeField]
	    private float _gravity = 9.81f;
	    [SerializeField]
	    private float _gravityPlatformer = -12f;
	    [SerializeField]
	    private float _jumpSpeed = 3.5f;
	    [SerializeField]
	    private float _doubleJumpMultiplier = 0.5f;
	    [SerializeField]
	    private GameObject _cameraRig;

	    public float jumpHeight = 1;

	    private CharacterController _controller;

		private AudioSource jump_sound;

	    private float _directionY;
	    private float _currentSpeed;

	    private bool _canDoubleJump = false;

	    public float turnSmoothTime = 0.2f;
	    float turnSmoothVelocity;

	    public float speedSmoothTime = 0.1f;
	    float speedSmoothVelocity;

	    private float velocityY;

		public float turnSpeed = 400.0f;

		public GameObject winText;


	    void Start()
	    {
	        _controller = GetComponent<CharacterController>();
	        anim = gameObject.GetComponentInChildren<Animator>();
			jump_sound = GetComponent<AudioSource>();

			winText.SetActive(false);
	        
	    }
		

	    void Update()
	    {
			if(Input.GetKeyDown(KeyCode.Space)){
				jump_sound.Play();
			}
	        if (_movementMode == MovementMode.Strafe)
	        {
	            MovementStafe();
	        }

	        if (_movementMode == MovementMode.Platformer)
	        {
	            MovementPlatformer();
	        }


	    }


	    private void LateUpdate()
	    {
	        if (IsPlayerMoving() && _movementMode == MovementMode.Strafe)
	            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, _cameraRig.transform.localEulerAngles.y, transform.localEulerAngles.z);
	    }

		private void OnTriggerEnter(Collider other){
			//makes enemy dissapear when player collides with them
			if(other.gameObject.CompareTag("Enemy")){
				other.gameObject.SetActive(false);
			}
			//makes the win text show up when the player interacts with the flag
			if (other.gameObject.CompareTag("Finish")){
				winText.SetActive(true);
				print("FINISH");
			}

		}
	

	    private bool IsPlayerMoving()
	    {
	        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
	    }

	    private void MovementStafe()
	    {
	        float horizontalInput = Input.GetAxis("Horizontal");
	        float verticalInput = Input.GetAxis("Vertical");

	        anim.SetBool ("isJumping", false);
	        anim.SetBool ("isGrounded", true);
	        anim.SetBool ("isFalling", false);

	        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

	        if (_controller.isGrounded)
	        {
	            _canDoubleJump = true;

	            if (Input.GetButtonDown("Jump"))
	            {
	                _directionY = _jumpSpeed;
	                // anim.SetBool ("isJumping", true);
	            }
	        }
	        else
	        {
	            if (Input.GetButtonDown("Jump") && _canDoubleJump)
	            {
	                _directionY = _jumpSpeed * _doubleJumpMultiplier;
	                _canDoubleJump = false;
	            }
	        }

	        _directionY -= _gravity * Time.deltaTime;

	        moveDirection = transform.TransformDirection(moveDirection);

	        bool running = Input.GetKey(KeyCode.LeftShift);
	        float targetSpeed = (running) ? _runningSpeed : _walkSpeed;
	        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

	        moveDirection.y = _directionY;

	        _controller.Move(_currentSpeed * Time.deltaTime * moveDirection);

	        float turn = Input.GetAxis("Horizontal");
			transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);


			if (Input.GetKey ("w")) {
				anim.SetInteger ("AnimationPar", 1);
			}  else {
				anim.SetInteger ("AnimationPar", 0);
			}

	    }

	    private void MovementPlatformer()
	    {
	        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	        Vector2 inputDir = input.normalized;
	        bool running = Input.GetKey(KeyCode.LeftShift);

	        if (inputDir != Vector2.zero)
	        {
	            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + _cameraRig.transform.eulerAngles.y;
	            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
	        }

	        float targetSpeed = ((running) ? _runningSpeed : _walkSpeed) * inputDir.magnitude;
	        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);



	        velocityY += Time.deltaTime * _gravityPlatformer;
	        Vector3 velocity = transform.forward * _currentSpeed + Vector3.up * velocityY;

	        _controller.Move(velocity * Time.deltaTime);
	        _currentSpeed = new Vector2(_controller.velocity.x, _controller.velocity.z).magnitude;

	        if (_controller.isGrounded)
	        {
	            velocityY = 0;
	        }

	        if (Input.GetKeyDown(KeyCode.Space))
	        {
	            if (_controller.isGrounded)
	            {
	                float jumpVelocity = Mathf.Sqrt(-2 * _gravityPlatformer * jumpHeight);
	                velocityY = jumpVelocity;
	            }
	        }
	    }
	}
}