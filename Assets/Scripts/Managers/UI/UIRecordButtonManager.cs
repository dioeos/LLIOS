using UnityEngine;
using UnityEngine.UIElements;

public class UIRecordButtonManager : MonoBehaviour
{
  [SerializeField]
  private UIDocument ui;

  //RecordButtonManager state variables
  private Button _recordButton;

  private IUIComponentsService _uiComponentsService;
  private IARSessionStatusService _sessionService;
  private IRecordingService _recordingService;
  private bool _isInitialized = false;

  public void Initialize(IUIComponentsService uiComponentsService, IARSessionStatusService sessionService, IRecordingService recordingService)
  {
    _uiComponentsService = uiComponentsService;
    _sessionService = sessionService;
    _recordingService = recordingService;
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

    if (!_recordingService.IsRecording())
    {
      _recordingService.RequestStartRecording();
    }
    else
    {
      _recordingService.RequestStopRecording();
    }
  }
}
