using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARCameraManager))]
public class ARFrameLogger : MonoBehaviour {
  private ARCameraManager arCameraManager;

  [SerializeField]
  private bool isRecording = false;
  [SerializeField]
  private int logEveryNFrames = 30;

  private int frameCounter = 0;

  private void Awake() { arCameraManager = GetComponent<ARCameraManager>(); }

  private void OnEnable() { arCameraManager.frameReceived += OnFrameReceived; }

  private void OnDisable() { arCameraManager.frameReceived -= OnFrameReceived; }

  public void ToggleRecording() {
    isRecording = !isRecording;

    Debug.Log(isRecording ? "=== RECORDING STARTED ==="
                          : "=== RECORDING STOPPED ===");
  }

  public void StartRecording() {
    isRecording = true;
    Debug.Log("=== RECORDING STARTED ===");
  }

  public void StopRecording() {
    isRecording = false;
    Debug.Log("=== RECORDING STOPPED ===");
  }

  private void OnFrameReceived(ARCameraFrameEventArgs args) {
    if (!isRecording)
      return;

    frameCounter++;

    if (logEveryNFrames > 1 && frameCounter % logEveryNFrames != 0)
      return;

    Vector3 camPos = transform.position;
    Quaternion camRot = transform.rotation;

    Debug.Log($"[AR FRAME] #" + frameCounter + $" time={Time.time:F2}" +
              $" pos={camPos}" + $" rot={camRot.eulerAngles}");

    if (arCameraManager.TryAcquireLatestCpuImage(out XRCpuImage image)) {
      Debug.Log(
          $"[AR IMAGE] {image.width}x{image.height} format={image.format}");

      image.Dispose();
    }
  }
}
