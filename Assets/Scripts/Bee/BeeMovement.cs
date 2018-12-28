﻿using System.Collections;
using UnityEngine;

[AddComponentMenu("Bee/BeeMovement")]
[RequireComponent(typeof(CharacterController))]
public class BeeMovement : MonoBehaviour {
  private const int DirectionChooseAttempts = 10;
  private const float DestroyAfterCrashDelay = 1f;

  public float speed = 1.5f;
  public float destroyGravity = -3.0f;

  [SerializeField] private ParticleSystem crashing;
  [SerializeField] private GameObject crashAnimation;

  private CharacterController characterController;
  private Bounds bounds;
  private Vector3 direction;
  private MovementMode mode = MovementMode.Normal;

  private enum MovementMode {
    Normal,
    Destroying
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
    switch (this.mode) {
      case MovementMode.Normal: {
        int attempts = DirectionChooseAttempts;
        if (this.IsApproachingCollision() && attempts-- > 0) {
          this.direction = this.RandomDirection();
        }

        this.transform.LookAt(this.transform.position + this.direction);
        var movement = this.speed * this.transform.forward;
        movement = Vector3.ClampMagnitude(movement, this.speed);
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
    }
  }

  public IEnumerator StartDestroySequence() {
    this.mode = MovementMode.Destroying;
    this.crashing.Play();
    while (!this.characterController.isGrounded) {
      yield return null;
    }

    var animation = Instantiate(this.crashAnimation);
    animation.transform.position = this.transform.position;
    yield return new WaitForSeconds(DestroyAfterCrashDelay);
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
