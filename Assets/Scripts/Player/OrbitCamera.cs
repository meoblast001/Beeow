using UnityEngine;

[AddComponentMenu("Player/OrbitCamera")]
public class OrbitCamera : MonoBehaviour {
  public RotationMode rotationMode;
  public float rotSpeed = 9.0f;
  public float minVertical = -75.0f;
  public float maxVertical = 5.0f;

  [SerializeField] private Transform horizontalTarget;

  private float rotation;
  private Vector3 offset;

  public enum RotationMode {
    RotateWorldHorizontal,
    RotateLocalVertical
  }

  void Start() {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    switch (this.rotationMode) {
      case RotationMode.RotateWorldHorizontal:
        this.rotation = this.transform.eulerAngles.y;
        this.offset = this.horizontalTarget.position - this.transform.position;
        break;

      case RotationMode.RotateLocalVertical:
        this.rotation = this.transform.localEulerAngles.x;
        this.offset = -this.transform.localPosition;
        break;
    }
  }

  void Update() {
    switch (this.rotationMode) {
      case RotationMode.RotateWorldHorizontal: {
        this.rotation += Input.GetAxis("Mouse X") * this.rotSpeed;

        var rotation = Quaternion.Euler(0.0f, this.rotation, 0.0f);
        this.transform.position
          = this.horizontalTarget.position - (rotation * this.offset);
        this.transform.LookAt(this.horizontalTarget);
        break;
      }

      case RotationMode.RotateLocalVertical: {
        this.rotation += -Input.GetAxis("Mouse Y") * this.rotSpeed;
        this.rotation = Mathf.Clamp(
          this.rotation,
          this.minVertical,
          this.maxVertical);

        var rotation = Quaternion.Euler(this.rotation, 0.0f, 0.0f);
        this.transform.localPosition = -(rotation * this.offset);

        var lookAt = Quaternion.LookRotation(
          -this.transform.localPosition,
          Vector3.up);
        this.transform.localRotation
          = Quaternion.Euler(lookAt.eulerAngles.x, 0.0f, 0.0f);
        break;
      }
    }
  }
}
