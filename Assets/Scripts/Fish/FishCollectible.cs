using UnityEngine;

[AddComponentMenu("Fish/FishCollectible")]
public class FishCollectible : MonoBehaviour {
  private const float Speed = 120f;

  void Update() {
      this.transform.Rotate(new Vector3(0f, Speed * Time.deltaTime, 0f));
  }

  void OnTriggerEnter(Collider other) {
    if (other.GetComponent<SingletonPlayer>() != null) {
      PlayerManager.Current.CountFish();
      Destroy(this.gameObject);
    }
  }
}
