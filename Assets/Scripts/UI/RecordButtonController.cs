using UnityEngine;
using UnityEngine.UIElements;
using Dioeos.UnityAppleReplayKit;

[RequireComponent(typeof(UIDocument))]
public class RecordButtonController : MonoBehaviour {

  [SerializeField]
  private SessionManager sessionManager;

  [SerializeField]
  private ARFrameLogger logger;

  private Button recordButton;
  private bool isRecording;

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

  private void OnRecordClicked() { 
    if (!sessionManager.GetIsAttached()) {
      Debug.LogWarning("Record button clicked, but session manager is not attached yet.");
      if (logger != null) logger.SetRecordingState(false);
      return;
    }

    if (isRecording) {
      SessionManagerApi.StopRecording();
      isRecording = false;
    } else {
      SessionManagerApi.StartRecording();
      isRecording = true;
    }

    if (logger != null) {
      logger.SetRecordingState(isRecording);
    }
  }

  public bool GetIsRecording() {
    return isRecording;
  }
}
