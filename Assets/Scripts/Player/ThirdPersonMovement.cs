using UnityEngine;

[AddComponentMenu("Player/ThirdPersonMovement")]
[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour {
  private const float AccelerationY = -18f;

  public float speed = 10.0f;
  public float gravity = -9.8f;
  public float jumpSpeed = 12.0f;

  [SerializeField] private Transform targetCamera;
  [SerializeField] private Animator animator;

  private CharacterController characterController;
  private float velocityY = 0.0f;
  private float idleTime = 0.0f;

  void Start() {
    this.characterController = this.GetComponent<CharacterController>();
  }

  void Update() {
    bool isIdling = false;

    var horizontal = Input.GetAxis("Horizontal") * this.speed;
    var vertical = Input.GetAxis("Vertical") * this.speed;
    bool moving = horizontal != 0.0f || vertical != 0.0f;
    if (moving) {
      var movement = new Vector3(horizontal, 0.0f, vertical);
      movement = Vector3.ClampMagnitude(movement, this.speed);

      movement = this.ConvertToRelative(movement);

      this.characterController.Move(movement * Time.deltaTime);
      this.transform.rotation = Quaternion.LookRotation(movement);
    } else {
      isIdling = true;
    }
    this.animator.SetBool("moving", moving);

    float groundedDistance
      = (this.characterController.height + this.characterController.radius)
      / 1.9f;
    bool isGrounded = Physics.Raycast(
      this.transform.position,
      -this.transform.up,
      groundedDistance);

    bool jumping = Input.GetButtonDown("Jump") && isGrounded;
    if (jumping) {
      this.velocityY = this.jumpSpeed;
    } else {
      if (this.characterController.isGrounded) {
        this.velocityY = 0.0f;
        isIdling = true;
      } else {
        this.velocityY += AccelerationY * Time.deltaTime;
      }
    }
    this.animator.SetBool("jumping", jumping);

    this.characterController.Move(
      new Vector3(0f, this.velocityY * Time.deltaTime, 0f));

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
