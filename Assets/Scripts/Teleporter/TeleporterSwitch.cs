using System;
using UnityEngine;

[AddComponentMenu("Teleporter/TeleporterSwitch")]
public class TeleporterSwitch : MonoBehaviour {
  [SerializeField] GameObject beam;

  void Start() {
    this.beam.SetActive(false);
    EventSystem.Subscribe(PlayerManager.FishCountEvent, this.TryEnable);
  }

  void OnDestroy() {
    EventSystem.Unsubscribe(PlayerManager.FishCountEvent, this.TryEnable);
  }

  private void TryEnable(object sender, EventArgs args) {
    var playerManager = PlayerManager.Current;
    if (playerManager.FishCount == playerManager.TotalFishCount) {
      this.beam.SetActive(true);
    }
  }
}
