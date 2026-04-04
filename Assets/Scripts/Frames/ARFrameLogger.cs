using Dioeos.UnityAppleReplayKit;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARCameraManager))]
public class ARFrameLogger : MonoBehaviour {
  private ARCameraManager cameraManager;

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
      string result = UnityAppleReplayKitApi.SayHello();
      if (statusLabel != null)
        statusLabel.text = result;
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

  public void ToggleRecording() {
    isRecording = !isRecording;

    if (statusLabel != null)
      statusLabel.text =
          isRecording ? "Recording started..." : "Recording stopped";
  }

  private void OnFrameReceived(ARCameraFrameEventArgs args) {
    if (!isRecording)
      return;

    frameCount++;

    if (statusLabel != null && frameCount % 15 == 0) {
      statusLabel.text = $"Frames: {frameCount}\n" + $"Time: {Time.time:F2}\n" +
                         $"Pos: {transform.position}";
    }
  }
}
