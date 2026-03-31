using UnityEngine;
using UnityEngine.UIElements;

public class RecordButtonController : MonoBehaviour {
  [SerializeField]
  private UIDocument uiDocument;

  private Button recordButton;
  private VisualElement recordToggle;
  private bool isRecording;

  private void OnEnable() {
    var root = uiDocument.rootVisualElement;
    recordButton = root.Q<Button>("RecordButton");
    recordToggle = root.Q<VisualElement>("RecordToggle");
    recordButton.clicked += OnRecordClicked;
  }

  private void OnRecordClicked() {}
}
