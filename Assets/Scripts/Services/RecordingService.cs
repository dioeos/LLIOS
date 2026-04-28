using UnityEngine;
using Dioeos.UnityAppleReplayKit;
using System;

public interface IRecordingService
{
  event Action<bool> OnRecordingStateChanged;
  void RequestStartRecording();
  void RequestStopRecording();
  bool TryConsumeStartRequest();
  bool TryConsumeStopRequest();
  void StartRecording(Transform pose);
  void StopRecording();
  void UpdateRecording(Transform pose);
  bool IsRecording();
}

public class RecordingService : IRecordingService
{
  private bool _isRecording;
  private bool _startRequested;
  private bool _stopRequested;

  public event System.Action<bool> OnRecordingStateChanged;

  public RecordingService() {}

  public void RequestStartRecording()
  {
    if (_isRecording) { return; }

    _startRequested = true;
    _stopRequested = false;
  }

  public void RequestStopRecording()
  {
    if (!_isRecording) { return; }

    _stopRequested = true;
    _startRequested = false;
  }

  public bool TryConsumeStartRequest()
  {
    if (!_startRequested || _isRecording) { return false; }

    _startRequested = false;
    return true;
  }

  public bool TryConsumeStopRequest()
  {
    if (!_stopRequested || !_isRecording) { return false; }

    _stopRequested = false;
    return true;
  }

  public void StartRecording(Transform pose)
  {
    CoordinatorApi.StartRecording(pose);
    _isRecording = true;
    _startRequested = false;

    OnRecordingStateChanged?.Invoke(true);
  }

  public void StopRecording()
  {
    CoordinatorApi.StopRecording();
    _isRecording = false;
    _stopRequested = false;
    OnRecordingStateChanged?.Invoke(false);
  }

  public void UpdateRecording(Transform pose)
  {
    if (!_isRecording) { return; }

    CoordinatorApi.UpdateRecording(pose);
  }

  public bool IsRecording()
  {
    return _isRecording;
  }
}
