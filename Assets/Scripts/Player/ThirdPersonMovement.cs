using UnityEngine;

[AddComponentMenu("Player/ThirdPersonMovement")]
[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour {
  public float speed = 10.0f;
  public float gravity = -9.8f;
  public float terminalVelocity = -54f;
  public float jumpSpeed = 8.0f;

  [SerializeField] private Transform targetCamera;
  [SerializeField] private Animator animator;

  private CharacterController characterController;
  private float velocityY = 0.0f;
  private float idleTime = 0.0f;

  void Start() {
    this.characterController = this.GetComponent<CharacterController>();
  }

  void Update() {
    var movement = new Vector3();
    bool isGrounded = this.characterController.isGrounded;

    bool isIdling = true;

    var horizontal = Input.GetAxis("Horizontal") * this.speed;
    var vertical = Input.GetAxis("Vertical") * this.speed;
    bool moving = horizontal != 0.0f || vertical != 0.0f;
    if (moving) {
      movement.x = horizontal;
      movement.z = vertical;
      movement = this.ConvertToRelative(
        Vector3.ClampMagnitude(movement, this.speed));

      this.transform.rotation = Quaternion.LookRotation(movement);

      isIdling = false;
    }
    this.animator.SetBool("moving", moving);

    bool jumping = Input.GetButtonDown("Jump") && isGrounded;
    if (jumping) {
      this.velocityY = this.jumpSpeed;
      isIdling = false;
    } else {
      if (isGrounded) {
        this.velocityY = -1.0f;
      } else {
        if (this.velocityY > this.terminalVelocity)
          this.velocityY += this.gravity * Time.deltaTime;

        isIdling = false;
      }
    }
    this.animator.SetBool("jumping", jumping);

    movement.y = this.velocityY;
    this.characterController.Move(movement * Time.deltaTime);

    this.idleTime = isIdling ? this.idleTime + Time.deltaTime : 0.0f;
    this.animator.SetFloat("idleTime", this.idleTime);
  }

  private Vector3 ConvertToRelative(Vector3 vector) {
    var orig = this.targetCamera.eulerAngles;
    // Only interested in rotation on Y axis of camera. This keeps movements
    // on the X/Z plane.
    this.targetCamera.eulerAngles = new Vector3(0.0f, orig.y, 0.0f);
    var result = this.targetCamera.TransformDirection(vector);
    this.targetCamera.eulerAngles = orig;
    return result;
  }
}
