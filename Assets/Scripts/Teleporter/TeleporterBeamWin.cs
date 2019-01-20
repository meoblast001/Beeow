using UnityEngine;

[AddComponentMenu("Teleporter/TeleporterBeamWin")]
public class TeleporterBeamWin : MonoBehaviour {
  void OnTriggerEnter(Collider other) {
    if (other.GetComponent<SingletonPlayer>() != null) {
      Debug.Log("You win!");
    }
  }
}
