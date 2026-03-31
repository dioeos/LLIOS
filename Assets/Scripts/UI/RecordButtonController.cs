using UnityEngine;
using UnityEngine.UIElements;

public class RecordButtonController : MonoBehaviour {
  public ARFrameLogger logger;

  private void Start() {
    var root = GetComponent<UIDocument>().rootVisualElement;

    Button recordButton = root.Q<Button>("record-button");

    recordButton.clicked += () => { logger.ToggleRecording(); };
  }
}
