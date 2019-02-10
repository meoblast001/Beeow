using UnityEngine;

[AddComponentMenu("RotatingObject")]
public class RotatingObject : MonoBehaviour {
  public float xSpeed = 0f;
  public float ySpeed = 0f;
  public float zSpeed = 0f;

  void Update() {
    this.transform.Rotate(new Vector3(
      this.xSpeed * Time.deltaTime,
      this.ySpeed * Time.deltaTime,
      this.zSpeed * Time.deltaTime));
  }
}
