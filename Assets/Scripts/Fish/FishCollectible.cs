using UnityEngine;

[AddComponentMenu("Fish/FishCollectible")]
public class FishCollectible : MonoBehaviour {
  void OnTriggerEnter(Collider other) {
    if (other.GetComponent<SingletonPlayer>() != null) {
      PlayerManager.Current.CountFish();
      Destroy(this.gameObject);
    }
  }
}
