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
      double time = sessionManager.GetTimestamp();
      statusLabel.text = $"{version} : {ptrString} : {time}";
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

    if (!UnityAppleReplayKitApi.IsReplayKitAvailable())
    {
      var root = uiDocument.rootVisualElement;
      statusLabel = root.Q<Label>("Label");
      statusLabel.text = "Failed to toggle recording";
    }

    if (isRecording)
    {
      UnityAppleReplayKitApi.StartRecording();
    }
    else
    {
      UnityAppleReplayKitApi.StopRecording();
    }


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
