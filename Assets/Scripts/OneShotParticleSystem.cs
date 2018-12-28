using System.Collections;
using UnityEngine;

[AddComponentMenu("OneShotParticleSystem")]
[RequireComponent(typeof(ParticleSystem))]
public class OneShotParticleSystem : MonoBehaviour {
  void Start () {
    this.StartCoroutine(this.OneShot());
  }

  private IEnumerator OneShot() {
    var particleSystem = this.GetComponent<ParticleSystem>();
    yield return new WaitForSeconds(particleSystem.main.duration);
    Destroy(this.gameObject);
  }
}
