using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Dioeos.UnityAppleReplayKit;

[RequireComponent(typeof(ARCameraManager))]
public class ARFrameLogger : MonoBehaviour {
  private ARCameraManager cameraManager;

  [SerializeField]
  private SessionManager sessionManager;

  [SerializeField]
  private UIDocument uiDocument;

  private Label statusLabel;

  private bool isRecording = false;
  private int frameCount = 0;

  [SerializeField]
  private int updateLabelEveryNFrames = 15;
  [SerializeField]
  private int sampleCpuImageEveryNFrames = 30;

  private void Awake() { cameraManager = GetComponent<ARCameraManager>(); }

  private void Start() {
    if (uiDocument != null) {
      var root = uiDocument.rootVisualElement;
      statusLabel = root.Q<Label>("Label");
      bool isReplayAvailable = UnityAppleReplayKitApi.IsReplayKitAvailable();
      string version = sessionManager.GetARSessionVersion().ToString();
      string ptrString = sessionManager.GetARSessionPtr().ToString();
      statusLabel.text = $"{version} : {ptrString}";
    }
  }

  private void OnEnable() {
    if (cameraManager != null)
      cameraManager.frameReceived += OnFrameReceived;
  }

  private void OnDisable() {
    if (cameraManager != null)
      cameraManager.frameReceived -= OnFrameReceived;
  }

  public void SetRecordingState(bool state) {
    isRecording = state;

    if (uiDocument != null) {
      if (statusLabel == null) {
        var root = uiDocument.rootVisualElement;
        statusLabel = root.Q<Label>("Label");
      }

      if (statusLabel != null) {
        statusLabel.text = isRecording ? "Recording started..." : "Recording stopped";
      }
    }
  }

  private void OnFrameReceived(ARCameraFrameEventArgs args) {
    if (!isRecording)
      return;

    frameCount++;
    // double time = sessionManager.GetTimestamp();

    if (statusLabel != null && frameCount % 15 == 0) {
      statusLabel.text = $"Frames: {frameCount}\n" + $"Time:\n" +
                         $"Pos: {transform.position}";
    }
  }
}
