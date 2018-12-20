using UnityEngine;

[AddComponentMenu("Player/ThirdPersonMovement")]
[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour {
  public float speed = 6.0f;

  [SerializeField] private Transform targetCamera;

  private CharacterController characterController;

  void Start() {
    this.characterController = this.GetComponent<CharacterController>();
  }

  void Update() {
    var horizontal = Input.GetAxis("Horizontal") * this.speed;
    var vertical = Input.GetAxis("Vertical") * this.speed;
    if (horizontal != 0.0f || vertical != 0.0f) {
      var movement = new Vector3(horizontal, 0.0f, vertical);
      movement = Vector3.ClampMagnitude(movement, this.speed);

      movement = this.ConvertToRelative(movement);

      this.characterController.Move(movement * Time.deltaTime);
      this.transform.rotation = Quaternion.LookRotation(movement);
    }
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
