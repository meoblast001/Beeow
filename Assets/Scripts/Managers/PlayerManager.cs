public class PlayerManager : BaseManager {
  public const string BeeCountEvent = "PlayerManager.BeeCountEvent";

  public static PlayerManager Current { get; private set; }

  public int BeeCount { get; private set; }

  public override void Start() {
    this.BeeCount = 0;
    Current = this;
    this.Ready = true;
  }

  public override void Stop() {
    Current = default(PlayerManager);
  }

  public void CountBee() {
    this.BeeCount += 1;
    EventSystem.Publish(this, BeeCountEvent);
  }
}
