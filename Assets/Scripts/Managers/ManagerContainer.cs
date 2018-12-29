using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("ManagerContainer")]
public class ManagerContainer : MonoBehaviour {
  [SerializeField] private BaseManager[] managers;

  void Start() {
    foreach (var manager in managers) {
      manager.Start();
    }
  }

  void Destroy() {
    foreach (var manager in managers) {
      manager.Stop();
    }
  }
}
