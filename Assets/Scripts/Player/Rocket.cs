using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Player/Rocket")]
public class Rocket : MonoBehaviour {
  public float maxTravelDistance = 50.0f;
  public float speed = 30.0f;

  private Vector3 startPosition;

  void Start() {
    this.startPosition = this.transform.position;
  }

  void Update() {
    this.transform.Translate(0.0f, 0.0f, this.speed * Time.deltaTime);

    var dist = Vector3.Distance(this.transform.position, this.startPosition);
    if (dist > this.maxTravelDistance)
      Destroy(this.gameObject);
  }

  private void OnTriggerEnter(Collider other) {
    Destroy(this.gameObject);
  }
}
