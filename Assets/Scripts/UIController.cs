using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UIController")]
public class UIController : MonoBehaviour {
  [SerializeField] Text beeCount;
  [SerializeField] Text fishCount;

  void Start () {
    this.OnBeeCount(this, EventArgs.Empty);
    this.OnFishCount(this, EventArgs.Empty);
    EventSystem.Subscribe(PlayerManager.BeeCountEvent, OnBeeCount);
    EventSystem.Subscribe(PlayerManager.FishCountEvent, OnFishCount);
  }

  void OnDestroy() {
    EventSystem.Unsubscribe(PlayerManager.BeeCountEvent, OnBeeCount);
    EventSystem.Unsubscribe(PlayerManager.FishCountEvent, OnFishCount);
  }

  private void OnBeeCount(object sender, EventArgs args) {
    this.beeCount.text = PlayerManager.Current.BeeCount.ToString();
  }

  private void OnFishCount(object sender, EventArgs args) {
    this.fishCount.text = PlayerManager.Current.FishCount.ToString()
      + " / " + PlayerManager.Current.TotalFishCount.ToString();
  }
}
