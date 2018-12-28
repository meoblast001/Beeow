using UnityEngine;

[AddComponentMenu("Bee/BeeMovement")]
[RequireComponent(typeof(CharacterController))]
public class BeeMovement : MonoBehaviour {
  private const int DirectionChooseAttempts = 10;

  public float speed = 1.5f;

  private CharacterController characterController;
  private Bounds bounds;
  private Vector3 direction;

  void Start() {
    this.characterController = this.GetComponent<CharacterController>();

    this.bounds = new Bounds(Vector3.zero, Vector3.zero);
    var colliders = this.GetComponentsInChildren<Collider>();
    foreach (var collider in colliders) {
      bounds.Encapsulate(collider.bounds);
    }

    this.direction = this.RandomDirection();
  }

  void Update() {
    int attempts = DirectionChooseAttempts;
    if (this.IsApproachingCollision() && attempts-- > 0) {
      this.direction = this.RandomDirection();
    }

    this.transform.LookAt(this.transform.position + this.direction);
    var movement = this.speed * this.transform.forward;
    movement = Vector3.ClampMagnitude(movement, this.speed);
    this.characterController.Move(movement * Time.deltaTime);
  }

  private bool IsApproachingCollision() {
    RaycastHit hit;
    var hasHit = Physics.SphereCast(
      this.transform.position,
      (this.bounds.size.x + this.bounds.size.y) / 2f,
      this.transform.forward,
      out hit);
    return hasHit
      && Vector3.Distance(this.transform.position, hit.point)
      < this.bounds.size.z;
  }

  private Vector3 RandomDirection() {
    var vector = new Vector3(
      Random.Range(-1f, 1f),
      Random.Range(-0.75f, 0.75f),
      Random.Range(-1f, 1f));
    return Vector3.ClampMagnitude(vector, 1f);
  }
}
