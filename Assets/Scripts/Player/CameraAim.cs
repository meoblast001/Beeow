using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("Player/CameraAim")]
[RequireComponent(typeof(Camera))]
public class CameraAim : MonoBehaviour {
  const float EPSILON = 0.25f;

  [SerializeField] private Transform shootOrigin;
  private Camera cam;

  void Start() {
    this.cam = this.GetComponent<Camera>();
  }

  public Vector3 SearchTarget() {
    var originDist = (this.shootOrigin.position - this.cam.transform.position)
      .magnitude;

    Ray ray = this.cam.ScreenPointToRay(new Vector3(
      this.cam.pixelWidth / 2,
      this.cam.pixelHeight / 2));
    RaycastHit[] hits = Physics.RaycastAll(
      this.transform.position,
      ray.direction,
      this.cam.farClipPlane);
    IEnumerable<RaycastHit?> targetableHits = hits
      .Where(hit => {
        var hitDist = (hit.transform.position - this.cam.transform.position)
          .magnitude;
        var isChild = hit.transform.IsChildOf(this.shootOrigin.transform);
        return hitDist - EPSILON > originDist && !isChild;
      })
      .OrderBy(hit => hit.distance)
      .Cast<RaycastHit?>();

    var nearestHit = targetableHits.FirstOrDefault();
    if (nearestHit.HasValue) {
      return nearestHit.Value.point;
    } else {
      // No hit, aim very far directly forward from camera.
      return this.cam.transform.position
        + (this.cam.transform.forward * this.cam.farClipPlane);
    }
  }
}
