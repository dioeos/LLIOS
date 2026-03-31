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

  private void Awake() { cameraManager = GetComponent<ARCameraManager>(); }

  private void Start() {
    if (uiDocument != null) {
      var root = uiDocument.rootVisualElement;

      statusLabel = root.Q<Label>("Label");

      if (statusLabel != null)
        statusLabel.text = "Ready";
    }
  }

  private void OnEnable() { cameraManager.frameReceived += OnFrameReceived; }

  private void OnDisable() { cameraManager.frameReceived -= OnFrameReceived; }

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

    Vector3 pos = transform.position;

    if (statusLabel != null) {
      statusLabel.text =
          $"Frames: {frameCount}\n" + $"Time: {Time.time:F2}\n" + $"Pos: {pos}";
    }

    if (cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image)) {
      statusLabel.text += $"\nImage: {image.width}x{image.height}";
      image.Dispose();
    }
  }
}
