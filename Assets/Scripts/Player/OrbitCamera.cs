using UnityEngine;

[AddComponentMenu("Player/OrbitCamera")]
public class OrbitCamera : MonoBehaviour {
  public float rotSpeed = 9.0f;

  [SerializeField] private Transform target;

  private float yRotation;
  private Vector3 offset;

  void Start() {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    this.yRotation = this.transform.eulerAngles.y;
    this.offset = this.target.position - this.transform.position;
  }

  void Update() {
    this.yRotation += Input.GetAxis("Mouse X") * this.rotSpeed;

    var rotation = Quaternion.Euler(0.0f, this.yRotation, 0.0f);
    this.transform.position = this.target.position	- (rotation * this.offset);
    this.transform.LookAt(this.target);
  }
}
