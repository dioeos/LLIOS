using UnityEngine;
using UnityEngine.UIElements;
using Dioeos.UnityAppleReplayKit;

public class UILabelsManager : MonoBehaviour
{
  private Label _recordingStateLabel;
  private bool _isRecording;

  private UIComponentsService _uIComponentsService;
  private bool _isInitialized = false;

  public void Initialize(UIComponentsService uIComponentsService)
  {
    _uIComponentsService = uIComponentsService;
    _isInitialized = true;
  }

  void Start()
  {
    _recordingStateLabel = _uIComponentsService.GetLabel("Label");
    if (_recordingStateLabel == null) { return; }

  }
}
