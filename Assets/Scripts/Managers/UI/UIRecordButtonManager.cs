using UnityEngine;
using UnityEngine.UIElements;

public class UIRecordButtonManager : MonoBehaviour
{
  [SerializeField]
  private UIDocument ui;

  //RecordButtonManager state variables
  private Button _recordButton;
  private VisualElement _recordVisual;
  private VisualElement _recordContainer;

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
    _recordButton = _uiComponentsService.GetButton("record-button");
    _recordVisual = _uiComponentsService.GetVisualElement("record-visual");
    _recordContainer = _uiComponentsService.GetVisualElement("record-container");


    if (_recordButton == null) { return; }

    _recordContainer?.RegisterCallback<GeometryChangedEvent>(OnRecordContainerGeometryChanged);
    _recordButton.clicked += OnRecordButtonClicked;
  }

  void OnDestroy()
  {
    if (_recordContainer != null)
    {
      _recordContainer.UnregisterCallback<GeometryChangedEvent>(OnRecordContainerGeometryChanged);
    }

    if (_recordButton != null)
    {
      _recordButton.clicked -= OnRecordButtonClicked;
    }
  }

  private void OnRecordContainerGeometryChanged(GeometryChangedEvent evt)
  {
    ResizeRecordButton(evt.newRect.height);
  }

  private void ResizeRecordButton(float containerHeight)
  {
    if (_recordButton == null || containerHeight <= 0) { return; }

    float buttonSize = containerHeight * 0.8f;
    float visualSize = buttonSize * 0.7f;

    _recordButton.style.width = buttonSize;
    _recordButton.style.height = buttonSize;
    _recordButton.style.maxWidth = buttonSize;
    _recordButton.style.maxHeight = buttonSize;

    float buttonRadius = buttonSize * 0.5f;
    _recordButton.style.borderTopLeftRadius = buttonRadius;
    _recordButton.style.borderTopRightRadius = buttonRadius;
    _recordButton.style.borderBottomLeftRadius = buttonRadius;
    _recordButton.style.borderBottomRightRadius = buttonRadius;

    if (_recordVisual == null) { return; }

    _recordVisual.style.width = visualSize;
    _recordVisual.style.height = visualSize;

    ApplyRecordVisualShape(visualSize);
  }

  private void ApplyRecordVisualShape(float visualSize)
  {
      bool isRecording = _recordingService.IsRecording();

      if (isRecording)
      {
          float radius = visualSize * 0.15f;

          _recordVisual.style.borderTopLeftRadius = radius;
          _recordVisual.style.borderTopRightRadius = radius;
          _recordVisual.style.borderBottomLeftRadius = radius;
          _recordVisual.style.borderBottomRightRadius = radius;

          _recordVisual.style.backgroundColor = Color.red;
      }
      else
      {
          _recordVisual.style.borderTopLeftRadius = StyleKeyword.Null;
          _recordVisual.style.borderTopRightRadius = StyleKeyword.Null;
          _recordVisual.style.borderBottomLeftRadius = StyleKeyword.Null;
          _recordVisual.style.borderBottomRightRadius = StyleKeyword.Null;

          _recordVisual.style.backgroundColor = StyleKeyword.Null;
      }
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

      ApplyRecordVisualShape(_recordVisual.resolvedStyle.width);
  }


}
