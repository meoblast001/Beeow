using UnityEngine;

[AddComponentMenu("Player/SingletonPlayer")]
public class SingletonPlayer : MonoBehaviour {
  void Start() {
    PlayerManager.Current.SetPlayerObject(this.gameObject);
  }

  public void Lose() {
    this.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
  }
}
