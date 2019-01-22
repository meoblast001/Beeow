using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {
  [SerializeField] private Sprite activeSprite;
  [SerializeField] private Sprite inactiveSprite;

  [SerializeField] private Image[] images;

  void Start() {
    EventSystem.Subscribe(PlayerManager.HealthChangeEvent, this.OnHealthChange);

    var playerManager = PlayerManager.Current;
    playerManager.MaxHealth = this.images.Length;
    playerManager.SetHealth(playerManager.MaxHealth);
  }

  void OnDestroy() {
    EventSystem.Unsubscribe(
      PlayerManager.HealthChangeEvent,
      this.OnHealthChange);
  }

  private void OnHealthChange(object sender, EventArgs args) {
    var playerManager = PlayerManager.Current;

    for (int i = 0; i < this.images.Length; ++i) {
      this.images[i].sprite = i < playerManager.Health
        ? this.activeSprite : this.inactiveSprite;
    }
  }
}
