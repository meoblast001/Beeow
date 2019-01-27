using System.Collections;
using UnityEngine;

[AddComponentMenu("Bee/BeeMovement")]
[RequireComponent(typeof(CharacterController))]
public class BeeMovement : MonoBehaviour {
  private const int DirectionChooseAttempts = 10;
  private const float DestroyAfterCrashDelay = 1f;
  private const float StartFollowingDistance = 10f;
  private const float StopFollowingDistance = 12f;

  public float speed = 1.5f;
  public float destroyGravity = -5.0f;

  [SerializeField] private ParticleSystem crashing;
  [SerializeField] private GameObject crashAnimation;

  private CharacterController characterController;
  private Bounds bounds;
  private Vector3 direction;
  private MovementMode mode = MovementMode.Normal;
  private Transform target = null;

  private enum MovementMode {
    Normal,
    Targetting,
    Destroying,
    Destroyed
  }

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
    if (this.mode == MovementMode.Normal
        || this.mode == MovementMode.Targetting) {
      float? targetDistance = PlayerManager.Current.PlayerObject != null
        ? Vector3.Distance(
          this.transform.position,
          PlayerManager.Current.PlayerObject.transform.position)
        : default(float?);

      if (targetDistance.HasValue && targetDistance < StartFollowingDistance) {
        this.mode = MovementMode.Targetting;
        this.target = PlayerManager.Current.PlayerObject.transform;
      } else if (!targetDistance.HasValue
                || targetDistance > StopFollowingDistance) {
        this.mode = MovementMode.Normal;
        this.target = null;
      }
    }

    switch (this.mode) {
      case MovementMode.Normal: {
        int attempts = DirectionChooseAttempts;
        if (this.IsApproachingCollision() && attempts-- > 0) {
          this.direction = this.RandomDirection();
        }

        this.transform.LookAt(this.transform.position + this.direction);
        var movement = this.speed * this.transform.forward;
        this.characterController.Move(movement * Time.deltaTime);

        break;
      }

      case MovementMode.Targetting: {
        this.transform.LookAt(this.target.position);
        var movement = this.speed * this.transform.forward;
        this.characterController.Move(movement * Time.deltaTime);
        break;
      }

      case MovementMode.Destroying: {
        var movement = new Vector3(
          this.direction.x,
          this.destroyGravity,
          this.direction.z);
        this.characterController.Move(movement * Time.deltaTime);

        break;
      }

      case MovementMode.Destroyed:
        break;
    }
  }

  public IEnumerator StartDestroySequence() {
    this.mode = MovementMode.Destroying;
    this.crashing.Play();
    while (!this.characterController.isGrounded) {
      yield return null;
    }
    yield return this.StartDestroyedSequence();
  }

  public IEnumerator StartDestroyedSequence() {
    this.mode = MovementMode.Destroyed;
    var animation = Instantiate(this.crashAnimation);
    animation.transform.position = this.transform.position;
    yield return new WaitForSeconds(DestroyAfterCrashDelay);
  }

  private bool IsApproachingCollision() {
    RaycastHit hit;
    return Physics.SphereCast(
      this.transform.position,
      (this.bounds.size.x + this.bounds.size.y) / 6f,
      this.transform.forward,
      out hit,
      this.bounds.size.z * 2f,
      ~0,
      QueryTriggerInteraction.Ignore);
  }

  private Vector3 RandomDirection() {
    var vector = new Vector3(
      Random.Range(-1f, 1f),
      Random.Range(-0.75f, 0.75f),
      Random.Range(-1f, 1f));
    return Vector3.ClampMagnitude(vector, 1f);
  }
}
