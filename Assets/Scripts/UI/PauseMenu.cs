using UnityEngine;

[AddComponentMenu("UI/PauseMenu")]
public class PauseMenu : MonoBehaviour {
  [SerializeField] private GameObject window;

  private bool isEnabled = false;
  private bool origCursorVisible;
  private CursorLockMode origCursorLockState;

  void Start() {
    this.window.SetActive(false);
  }

  void Update() {
    if (Input.GetButtonDown("Cancel")) {
      this.SetEnabled(true);
    }
  }

  public void EndGame() {
    Application.Quit();
  }

  public void Close() {
    this.SetEnabled(false);
  }

  private void SetEnabled(bool enabled) {
    this.window.SetActive(enabled);
    this.isEnabled = enabled;

    if (enabled) {
      this.origCursorVisible = Cursor.visible;
      this.origCursorLockState = Cursor.lockState;

      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
    } else {
      Cursor.visible = this.origCursorVisible;
      Cursor.lockState = this.origCursorLockState;
    }

    Time.timeScale = enabled ? 0f : 1f;
    PlayerManager.Current.SetPaused(enabled);
  }
}
