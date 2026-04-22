using UnityEngine;
using Dioeos.UnityAppleReplayKit;

public interface IRecordingService
{
  void StartRecording();
  void StopRecording();
  void UpdateRecording();
  bool IsRecording();
  void SetIsRecording(bool newStatus);
}

public class RecordingService : IRecordingService
{
  private bool _isRecording;

  public RecordingService() {}

  public void StartRecording()
  {
    CoordinatorApi.StartRecording();
    _isRecording = true;
  }

  public void StopRecording()
  {
    CoordinatorApi.StopRecording();
    _isRecording = false;
  }

  public void UpdateRecording()
  {
    CoordinatorApi.UpdateRecording();
  }

  public bool IsRecording()
  {
    return _isRecording;
  }

  public void SetIsRecording(bool newStatus)
  {
    _isRecording = newStatus;
  }
}
