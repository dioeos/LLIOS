using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARFramesManager : MonoBehaviour
{
  private ARCameraManager _arCameraManager;
  private UIRecordButtonManager _rbm;

  public void Initialize(ARCameraManager camera, UIRecordButtonManager rbm)
  {
    _arCameraManager = camera;
    _rbm = rbm;
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
    if (!_rbm.IsRecording()) { return; }
  }
}
