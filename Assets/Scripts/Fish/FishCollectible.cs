using UnityEngine;

[AddComponentMenu("Fish/FishCollectible")]
public class FishCollectible : MonoBehaviour {
  private float rotY = 0f;

  void Update() {
      this.rotY += Time.deltaTime;
      this.transform.Rotate(new Vector3(0f, this.rotY, 0f));
  }

  void OnTriggerEnter(Collider other) {
    if (other.GetComponent<SingletonPlayer>() != null) {
      PlayerManager.Current.CountFish();
      Debug.Log("Fish collect: "
        + PlayerManager.Current.FishCount + " / "
        + PlayerManager.Current.TotalFishCount);
      Destroy(this.gameObject);
    }
  }
}
