using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Dioeos.UnityAppleReplayKit;

public class ARSessionManager : MonoBehaviour
{
  [Header("General ARSession Components")]
  [SerializeField]
  private ARSession arSession;
  private XRSessionSubsystem _sessionSubsystem;

  private IARCameraPoseService _cameraPoseService;
  private IARSessionStatusService _sessionService;
  private IRecordingService _recordingService;

  [Header("SessionManager State Variables")]
  private bool _attached;
  private double _currentArTimestamp = 0.0;
  private bool _isInitialized = false;

  public void Initialize(IARCameraPoseService poseService, IARSessionStatusService sessionService, IRecordingService recordingService)
  {
    _cameraPoseService = poseService;
    _sessionService = sessionService;
    _recordingService = recordingService;
    _isInitialized = true;
  }

  void Start()
  {
    if (!_isInitialized) { return; }
    _attached = _sessionService.AttachPluginToSession();
  }

  void Update()
  {
    if (!_attached)
    {
      _attached = _sessionService.AttachPluginToSession();
    }

    if (_attached)
    {
      CameraPose pose = _cameraPoseService.GetUnityARSessionCameraPose();
      Vector3 camPosition = pose.Position;
      Quaternion camRotation = pose.Rotation;

      if (_recordingService.IsRecording())
// TODO: pass in camPosition and camRotation as params into Update
        _recordingService.UpdateRecording();
    }
  }

  void OnDestroy()
  {
    _sessionService.DetachPluginFromSession();
  }
}

