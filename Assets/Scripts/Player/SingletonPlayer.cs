using System;
using UnityEngine;

[AddComponentMenu("Player/SingletonPlayer")]
public class SingletonPlayer : MonoBehaviour {
  void Start() {
    PlayerManager.Current.SetPlayerObject(this.gameObject);
    EventSystem.Subscribe(PlayerManager.HealthChangeEvent, OnHealthChange);
  }

  void OnDestroy() {
    EventSystem.Unsubscribe(PlayerManager.HealthChangeEvent, OnHealthChange);
  }

  public void Lose() {
    this.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

    var playerManager = PlayerManager.Current;
    playerManager.SetHealth(playerManager.MaxHealth);
  }

  private void OnHealthChange(object sender, EventArgs args) {
    if (PlayerManager.Current.Health == 0) {
      this.Lose();
    }
  }
}
