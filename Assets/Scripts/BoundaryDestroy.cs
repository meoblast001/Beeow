using UnityEngine;
using UnityEngine.Events;

public class BoundaryDestroy : MonoBehaviour {
  [SerializeField] private UnityEvent onDestroy;

  public void Awake() {
    if (this.onDestroy == null)
      this.onDestroy = new UnityEvent();
  }

  public void Update() {
    var boundary = GameObject.FindWithTag("Boundary");
    var bounds
      = new Bounds(boundary.transform.position, boundary.transform.localScale);
    if (!bounds.Contains(this.transform.position)) {
      this.onDestroy.Invoke();
    }
  }
}
