using UnityEngine;

[AddComponentMenu("Player/RocketLauncher")]
public class RocketLauncher : MonoBehaviour {
  [SerializeField] private GameObject orientationCamera;
  [SerializeField] private Transform launchPoint;
  [SerializeField] private GameObject rocketPrefab;

  void Update() {
    // Use same Y rotation as orientation camera.
    var selfOrient = this.transform.eulerAngles;
    var targetOrient = this.orientationCamera.transform.eulerAngles;
    this.transform.eulerAngles
      = new Vector3(selfOrient.x, targetOrient.y, selfOrient.z);

    // Fire.
    var launch = Input.GetButtonDown("Fire1");
    if (launch) {
      var rocketInst = GameObject.Instantiate(this.rocketPrefab);
      rocketInst.transform.position = new Vector3(
        this.launchPoint.position.x,
        this.launchPoint.position.y,
        this.launchPoint.position.z);
      rocketInst.transform.rotation = new Quaternion(
        this.launchPoint.rotation.x,
        this.launchPoint.rotation.y,
        this.launchPoint.rotation.z,
        this.launchPoint.rotation.w);
    }
  }
}
