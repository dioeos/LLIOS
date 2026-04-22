using UnityEngine;
using UnityEngine.UIElements;
using Dioeos.UnityAppleReplayKit;

public class UIRecordButtonManager : MonoBehaviour
{
  [SerializeField]
  private UIDocument ui;

  //RecordButtonManager state variables
  private Button _recordButton;
  private bool _isRecording;

  private IUIComponentsService _uiComponentsService;
  private IARSessionStatusService _sessionService;
  private bool _isInitialized = false;

  public void Initialize(IUIComponentsService uiComponentsService, IARSessionStatusService sessionService)
  {
    _uiComponentsService = uiComponentsService;
    _sessionService = sessionService;
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
    if (!_sessionService.IsPluginAttachedToSession()) { return; }
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
