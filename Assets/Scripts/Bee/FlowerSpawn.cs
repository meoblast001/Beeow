using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Bee/FlowerSpawn")]
public class FlowerSpawn : MonoBehaviour {
  public int activeBeeLimit = 3;
  public float minNextBeeSeconds = 10f;
  public float maxNextBeeSeconds = 30f;

  [SerializeField] private GameObject beePrefab;
  [SerializeField] private Transform spawnPoint;

  private float untilNextBee;
  private List<GameObject> activeBees = new List<GameObject>();

  private float RandNextBeeDelay {
    get { return Random.Range(this.minNextBeeSeconds, this.maxNextBeeSeconds); }
  }

  void Start() {
    this.untilNextBee = this.RandNextBeeDelay;
  }

  void Update() {
    this.activeBees.RemoveAll(bee => bee == null);
    this.untilNextBee -= Time.deltaTime;

    if (untilNextBee < 0f) {
      if (this.activeBees.Count < this.activeBeeLimit) {
        var bee = GameObject.Instantiate(
          this.beePrefab,
          this.spawnPoint.transform.position,
          Quaternion.Euler(0f, Random.Range(0f, 360), 0f));
        this.activeBees.Add(bee);
      }

      this.untilNextBee = this.RandNextBeeDelay;
    }
  }
}
