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


    //AR managers
    _sessionManager.Initialize(
      poseService,
      _uiRecordButtonManager
    );
    _framesManager.Initialize(
      camera,
      _uiRecordButtonManager
    );

    //UI managers
    _uiRecordButtonManager.Initialize(
      uiComponentsService,
      _sessionManager
    );
    _uiLabelsManager.Initialize(
      uiComponentsService,
      _sessionManager
    );

      
  }
}
