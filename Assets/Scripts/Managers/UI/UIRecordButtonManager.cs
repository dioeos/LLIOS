using UnityEngine;
using UnityEngine.UIElements;
using Dioeos.UnityAppleReplayKit;

public class UIRecordButtonManager : MonoBehaviour
{
  [SerializeField]
  private UIDocument ui;

  private ARSessionManager _sessionManager;

  //RecordButtonManager state variables
  private Button _recordButton;
  private bool _isRecording;

  private UIComponentsService _uiComponentsService;
  private bool _isInitialized = false;

  public void Initialize(UIComponentsService uiComponentsService, ARSessionManager sessionManager)
  {
    _uiComponentsService = uiComponentsService;
    _sessionManager = sessionManager;
    _isInitialized = true;
  }


  void Start() 
  {
    _recordButton = _uiComponentsService.GetButton("RecordButton");

    if (_recordButton == null) { return; }

    _recordButton.clicked += OnRecordButtonClicked;
  }

  private void OnRecordButtonClicked() 
  {
    if (!_sessionManager.IsPluginAttachedToSession()) { return; }
    if (_isRecording)
    {
      CoordinatorApi.StopRecording();
      _isRecording = false;

    }
    else 
    {
      CoordinatorApi.StartRecording();
      _isRecording = true;
    }
  }

  public bool IsRecording()
  {
    return _isRecording;
  }
}
