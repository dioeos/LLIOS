using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARFramesManager : MonoBehaviour
{
  private ARCameraManager _arCameraManager;
  private IRecordingService _recordingService;

  public void Initialize(ARCameraManager camera, IRecordingService recordingService)
  {
    _arCameraManager = camera;
    _recordingService = recordingService;
  }

  void OnEnable() 
  {
    if (_arCameraManager == null) { return; }
    _arCameraManager.frameReceived += OnFrameReceived;
  }

  void OnDisable()
  {
    if (_arCameraManager == null) { return; }
    _arCameraManager.frameReceived -= OnFrameReceived;
  }

  private void OnFrameReceived(ARCameraFrameEventArgs args)
  {
    if (!_recordingService.IsRecording()) { return; }
  }
}
