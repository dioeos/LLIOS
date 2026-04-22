using UnityEngine;
using UnityEngine.UIElements;
using Dioeos.UnityAppleReplayKit;

public class UILabelsManager : MonoBehaviour
{
  private Label _recordingStateLabel;
  private bool _isRecording;

  private IUIComponentsService _uIComponentsService;
  private IARSessionStatusService _sessionService;
  private bool _isInitialized = false;

  public void Initialize(IUIComponentsService uIComponentsService, IARSessionStatusService sessionService)
  {
    _uIComponentsService = uIComponentsService;
    _sessionService = sessionService;
    _isInitialized = true;
  }

  void Start()
  {
    _recordingStateLabel = _uIComponentsService.GetLabel("Label");
    if (_recordingStateLabel != null)
    {
      StartCoroutine(InitializeLabelWhenReady());
    }
  }

  private System.Collections.IEnumerator InitializeLabelWhenReady()
  {
    // Wait until the AR session is fully initialized and attached
    while (!_sessionService.IsPluginAttachedToSession())
    {
      yield return null;
    }

    bool isReplayAvailable = UnityAppleReplayKitApi.IsReplayKitAvailable();
    string version = _sessionService.GetARSessionVersion().ToString();
    string ptrString = _sessionService.GetARSessionPtr().ToString();

    _recordingStateLabel.text = $"{version} : {ptrString}";
  }
}
