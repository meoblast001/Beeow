using UnityEngine;

[AddComponentMenu("Player/MoveWithPlatforms")]
public class MoveWithPlatforms : MonoBehaviour {
  private const float DetatchAfterSeconds = 0.1f;

  private Transform originalParent;
  private bool activated = false;
  private float secondsUntilDetatch = 0;

  void Start() {
    this.originalParent = this.transform.parent;
  }

  void Update() {
    if (this.activated && this.secondsUntilDetatch <= 0) {
      this.transform.parent = this.originalParent;
      this.activated = false;
    } else {
      this.secondsUntilDetatch -= Time.deltaTime;
    }
  }

  void OnControllerColliderHit(ControllerColliderHit other) {
    if (other.gameObject.tag == "Platform") {
      if (!this.activated) {
        this.originalParent = this.transform.parent;
        this.transform.parent = other.transform;
      }

      this.activated = true;
      this.secondsUntilDetatch = DetatchAfterSeconds;
    }
  }
}
