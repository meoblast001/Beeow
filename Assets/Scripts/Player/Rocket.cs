using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Player/Rocket")]
public class Rocket : MonoBehaviour {
  public float speed = 18.0f;

  void Update() {
    this.transform.Translate(0.0f, 0.0f, this.speed * Time.deltaTime);
  }
}
