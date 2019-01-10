using UnityEngine;

[AddComponentMenu("Player/SingletonPlayer")]
public class SingletonPlayer : MonoBehaviour {
  void Start() {
    PlayerManager.Current.SetPlayerObject(this.gameObject);
  }
}
