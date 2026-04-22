using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;

public class AppCoordinator : MonoBehaviour
{
  [Header("Required Game Objects")]
  [SerializeField]
  private Camera _arCamera;
  [SerializeField]
  private UIDocument _ui;
  [SerializeField]
  private ARSession _arSession;

  [Header("AR Managers")]
  [SerializeField]
  private ARSessionManager _sessionManager;
  [SerializeField]
  private ARFramesManager _framesManager;

  [Header("UI Managers")]
  [SerializeField]
  private UIRecordButtonManager _uiRecordButtonManager;
  [SerializeField]
  private UILabelsManager _uiLabelsManager;

  void Awake() 
  {
    var camera = GetComponent<ARCameraManager>();

    IARCameraPoseService poseService = new ARCameraPoseService(_arCamera);
    IUIComponentsService uiComponentsService = new UIComponentsService(_ui);
    IARSessionStatusService sessionService = new ARSessionStatusService(_arSession);
    IRecordingService recordingService = new RecordingService();


    //AR managers
    _sessionManager.Initialize(
      poseService,
      sessionService,
      recordingService
    );
    _framesManager.Initialize(
      camera,
      recordingService
    );

    //UI managers
    _uiRecordButtonManager.Initialize(
      uiComponentsService,
      sessionService,
      recordingService
    );
    _uiLabelsManager.Initialize(
      uiComponentsService,
      sessionService
    );

      
  }
}
