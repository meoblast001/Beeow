using UnityEngine;

public class PlayerManager : BaseManager {
  public const string IsPausedEvent = "PlayerManager.IsPaused";
  public const string BeeCountEvent = "PlayerManager.BeeCountEvent";
  public const string FishCountEvent = "PlayerManager.FishCountEvent";
  public const string HealthChangeEvent = "PlayerManager.HealthChangeEvent";

  public static PlayerManager Current { get; private set; }

  public GameObject PlayerObject { get; private set; }
  public bool IsPaused { get; private set; }
  public int Health { get; private set; }
  public int MaxHealth { get; set; }
  public int BeeCount { get; private set; }
  public int TotalFishCount { get; private set; }
  public int FishCount { get; private set; }

  public override void Start() {
    this.BeeCount = 0;
    this.TotalFishCount = FindObjectsOfType<FishCollectible>().Length;
    Current = this;
    this.Ready = true;
  }

  public override void Stop() {
    Current = default(PlayerManager);
  }

  public void SetPlayerObject(GameObject obj) {
    this.PlayerObject = obj;
  }

  public void SetPaused(bool paused) {
    this.IsPaused = paused;
    EventSystem.Publish(this, IsPausedEvent);
  }

  public void SetHealth(int health) {
    this.Health = health;
    EventSystem.Publish(this, HealthChangeEvent);
  }

  public void ChangeHealth(int delta) {
    this.Health = Mathf.Clamp(this.Health + delta, 0, this.MaxHealth);
    EventSystem.Publish(this, HealthChangeEvent);
  }

  public void CountBee() {
    this.BeeCount += 1;
    EventSystem.Publish(this, BeeCountEvent);
  }

  public void CountFish() {
    this.FishCount += 1;
    EventSystem.Publish(this, FishCountEvent);
  }
}
