using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UIController")]
public class UIController : MonoBehaviour {
  [SerializeField] Text beeCount;

  void Start () {
    this.beeCount.text = (0).ToString();
    EventSystem.Subscribe(PlayerManager.BeeCountEvent, OnBeeCount);
  }

  private void OnBeeCount(object sender, EventArgs args) {
    this.beeCount.text = PlayerManager.Current.BeeCount.ToString();
  }
}
