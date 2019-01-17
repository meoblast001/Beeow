using System.Collections;
using UnityEngine;

[AddComponentMenu("Bee/BeeEnemyTarget")]
[RequireComponent(typeof(BeeMovement))]
public class BeeEnemyTarget : MonoBehaviour {
  private BeeMovement movement;
  private bool destroyable = true;

  void Start() {
    this.movement = this.GetComponent<BeeMovement>();
  }

  public void OnTriggerEnter(Collider other) {
    var isRocket = other.GetComponent<Rocket>() != null;
    if (isRocket && this.destroyable) {
      this.destroyable = false;
      this.StartCoroutine(this.StartDestroySequence());
    }
  }

  public void OnOutOfBounds() {
    Destroy(this.gameObject);
  }

  private IEnumerator StartDestroySequence() {
    PlayerManager.Current.CountBee();
    yield return this.movement.StartDestroySequence();
    Destroy(this.gameObject);
  }
}
