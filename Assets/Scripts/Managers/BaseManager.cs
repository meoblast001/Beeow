using UnityEngine;

public abstract class BaseManager : MonoBehaviour {
  public bool Ready { get; protected set; }

  public abstract void Start();
  public abstract void Stop();
}
