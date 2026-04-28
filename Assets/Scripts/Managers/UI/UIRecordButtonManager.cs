using UnityEngine;
using UnityEngine.UIElements;

public class UIRecordButtonManager : MonoBehaviour
{
    [SerializeField]
    private UIDocument ui;

    private Button _recordButton;
    private VisualElement _recordVisual;
    private VisualElement _recordContainer;

    private IUIComponentsService _uiComponentsService;
    private IARSessionStatusService _sessionService;
    private IRecordingService _recordingService;

    private bool _isInitialized = false;
    private float _currentVisualSize = 0f;

    public void Initialize(
        IUIComponentsService uiComponentsService,
        IARSessionStatusService sessionService,
        IRecordingService recordingService)
    {
        _uiComponentsService = uiComponentsService;
        _sessionService = sessionService;
        _recordingService = recordingService;
        _isInitialized = true;
    }

    void Start()
    {
        if (!_isInitialized || _uiComponentsService == null || _recordingService == null)
        {
            Debug.LogError("UIRecordButtonManager was not initialized before Start().");
            return;
        }

        _recordButton = _uiComponentsService.GetButton("record-button");
        _recordVisual = _uiComponentsService.GetVisualElement("record-visual");
        _recordContainer = _uiComponentsService.GetVisualElement("record-container");

        if (_recordButton == null || _recordVisual == null)
        {
            Debug.LogError("Record button or record visual was not found.");
            return;
        }

        _recordContainer?.RegisterCallback<GeometryChangedEvent>(OnRecordContainerGeometryChanged);
        _recordButton.clicked += OnRecordButtonClicked;
        _recordingService.OnRecordingStateChanged += OnRecordingStateChanged;

        ApplyRecordVisualShape();
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

        if (_recordingService != null)
        {
            _recordingService.OnRecordingStateChanged -= OnRecordingStateChanged;
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
        _currentVisualSize = buttonSize * 0.7f;

        _recordButton.style.width = buttonSize;
        _recordButton.style.height = buttonSize;
        _recordButton.style.maxWidth = buttonSize;
        _recordButton.style.maxHeight = buttonSize;

        float buttonRadius = buttonSize * 0.5f;
        _recordButton.style.borderTopLeftRadius = buttonRadius;
        _recordButton.style.borderTopRightRadius = buttonRadius;
        _recordButton.style.borderBottomLeftRadius = buttonRadius;
        _recordButton.style.borderBottomRightRadius = buttonRadius;

        _recordVisual.style.width = _currentVisualSize;
        _recordVisual.style.height = _currentVisualSize;

        ApplyRecordVisualShape();
    }

    private void OnRecordButtonClicked()
    {
        if (!_sessionService.IsPluginAttachedToSession()) { return; }

        if (_recordingService.IsRecording())
        {
            _recordingService.RequestStopRecording();
        }
        else
        {
            _recordingService.RequestStartRecording();
        }
    }

    private void OnRecordingStateChanged(bool isRecording)
    {
        ApplyRecordVisualShape();
    }

    private void ApplyRecordVisualShape()
    {
        if (_recordVisual == null) { return; }

        if (_recordingService.IsRecording())
        {
            float radius = _currentVisualSize * 0.15f;

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
}
