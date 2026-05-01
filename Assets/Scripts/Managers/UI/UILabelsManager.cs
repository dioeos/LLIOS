using UnityEngine;
using UnityEngine.UIElements;
using Dioeos.UnityAppleReplayKit;

public class UILabelsManager : MonoBehaviour
{
  private Label _recordingStateLabel;
  private Label _headerLabel;
  private bool _isRecording;

  private IUIComponentsService _uIComponentsService;
  private IARSessionStatusService _sessionService;
  private ILocationService _locationService;
  private bool _isInitialized = false;

  public void Initialize(IUIComponentsService uIComponentsService, IARSessionStatusService sessionService, ILocationService locationService)
  {
    _uIComponentsService = uIComponentsService;
    _sessionService = sessionService;
    _isInitialized = true;
    _locationService = locationService;
  }

  void Start()
  {
    _headerLabel = _uIComponentsService.GetLabel("header-label");
    if (_headerLabel != null)
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

    // bool isReplayAvailable = UnityAppleReplayKitApi.IsReplayKitAvailable();
    // string version = _sessionService.GetARSessionVersion().ToString();
    // string ptrString = _sessionService.GetARSessionPtr().ToString();
    //
    // _recordingStateLabel.text = $"{version} : {ptrString}";
    //
    while (true)
    {
      string location = _locationService.GetLocationCoordinates();
      _headerLabel.text = $"{location}";
      yield return new WaitForSeconds(1f);
    }
  }
}
