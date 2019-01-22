using UnityEngine;

[AddComponentMenu("Player/PlayerTarget")]
[RequireComponent(typeof(SingletonPlayer))]
public class PlayerTarget : MonoBehaviour {
  private SingletonPlayer player;

  void Start() {
    this.player = this.GetComponent<SingletonPlayer>();
  }

  void OnTriggerEnter(Collider other) {
    if (other.GetComponent<BeeMovement>()) {
      PlayerManager.Current.ChangeHealth(-1);
    }
  }
}
