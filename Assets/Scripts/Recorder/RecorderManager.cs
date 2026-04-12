using System;
using UnityEngine;

public class RecorderManager : MonoBehaviour {
  public bool IsRecording { get; private set; }
  public event Action<bool> RecordingStateChanged;

  public void ToggleRecording() {
    IsRecording = !IsRecording;
    RecordingStateChanged?.Invoke(IsRecording);
  }

  // public void StartRecording() {
  //   if (IsRecording)
  //     return;
  //
  //   IsRecording = true;
  //   RecordingStateChanged?.Invoke(true);
  // }
  //
  // public void StopRecording() {
  //   if (!IsRecording)
  //     return;
  //
  //   IsRecording = false;
  //   RecordingStateChanged?.Invoke(false);
  // }
}
