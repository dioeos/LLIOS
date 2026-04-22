using UnityEngine;
using UnityEngine.UIElements;
using Dioeos.UnityAppleReplayKit;

public class UILabelsManager : MonoBehaviour
{
  private ARSessionManager _sm;

  private Label _recordingStateLabel;
  private bool _isRecording;

  private UIComponentsService _uIComponentsService;
  private bool _isInitialized = false;

  public void Initialize(UIComponentsService uIComponentsService, ARSessionManager sm)
  {
    _uIComponentsService = uIComponentsService;
    _sm = sm;
    _isInitialized = true;
  }

  void Start()
  {
    _recordingStateLabel = _uIComponentsService.GetLabel("Label");
    if (_recordingStateLabel == null) { return; }

    bool isReplayAvailable = UnityAppleReplayKitApi.IsReplayKitAvailable();
    string version = _sm.GetARSessionVersion().ToString();
    string ptrString = _sm.GetARSessionPtr().ToString();

    _recordingStateLabel.text = $"{version} : {ptrString}";
  }
}
