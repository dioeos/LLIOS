using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class RecordButtonController : MonoBehaviour {
  [SerializeField]
  private ARFrameLogger logger;

  private Button recordButton;

  private void OnEnable() {
    var uiDocument = GetComponent<UIDocument>();

    if (uiDocument == null) {
      Debug.LogError("UIDocument missing!");
      return;
    }

    var root = uiDocument.rootVisualElement;

    if (root == null) {
      Debug.LogError("rootVisualElement is null");
      return;
    }

    recordButton = root.Q<Button>("RecordButton");

    if (recordButton == null) {
      Debug.LogError("Button with name 'record-button' not found in UXML");
      return;
    }

    if (logger == null) {
      Debug.LogError("Logger reference not assigned!");
      return;
    }

    recordButton.clicked += OnRecordClicked;

    Debug.Log("Record button successfully connected");
  }

  private void OnDisable() {
    if (recordButton != null)
      recordButton.clicked -= OnRecordClicked;
  }

  private void OnRecordClicked() { logger.ToggleRecording(); }
}
