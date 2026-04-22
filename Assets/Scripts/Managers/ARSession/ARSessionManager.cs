using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using System.Runtime.InteropServices;
using Dioeos.UnityAppleReplayKit;

[StructLayout(LayoutKind.Sequential)]
public struct NativeSessionWrapper
{
  public int version;
  public IntPtr session;
}


public class ARSessionManager : MonoBehaviour
{
  private UIRecordButtonManager rbc;

  [Header("General ARSession Components")]
  [SerializeField]
  private ARSession arSession;
  private XRSessionSubsystem _sessionSubsystem;
  private NativeSessionWrapper _nativeWrapper;

  private IARCameraPoseService _cameraPoseService;

  [Header("SessionManager State Variables")]
  private bool _attached;
  private double _currentArTimestamp = 0.0;
  private bool _isInitialized = false;

  public void Initialize(IARCameraPoseService poseService, UIRecordButtonManager buttonManager)
  {
    _cameraPoseService = poseService;
    rbc = buttonManager;
    _isInitialized = true;
  }

  void Start()
  {
    if (!_isInitialized) { return; }
    TryAttach();
  }

  void Update()
  {
    if (!_attached)
    {
      TryAttach();
    }

    if (_attached)
    {
      CameraPose pose = _cameraPoseService.GetUnityARSessionCameraPose();
      Vector3 camPosition = pose.Position;
      Quaternion camRotation = pose.Rotation;

      if (rbc.IsRecording())
// TODO: pass in camPosition and camRotation as params into Update
        CoordinatorApi.UpdateRecording();
    }
  }

  private void TryAttach()
  {
    if (_sessionSubsystem == null || _sessionSubsystem.nativePtr == IntPtr.Zero)
    {
      MarshalNativePointer();
    }

    if (_sessionSubsystem != null && _sessionSubsystem.nativePtr != IntPtr.Zero)
    {
      _attached = AttachSession(_sessionSubsystem.nativePtr);
    }
  }

  void OnDestroy()
  {
    DetachSession();
  }

  private void DetachSession()
  {
    CoordinatorApi.DetachSession();
  }

  private bool AttachSession(IntPtr sessionPointer)
  {
    if (sessionPointer == IntPtr.Zero)
      return false;

    bool isConnected = CoordinatorApi.AttachSession(sessionPointer);
    return isConnected;
  }

  private void MarshalNativePointer()
  {
    if (arSession == null)
      return;

    _sessionSubsystem = arSession.subsystem;

    if (_sessionSubsystem == null)
      return;

    IntPtr wrapperPtr = _sessionSubsystem.nativePtr;

    if (wrapperPtr == IntPtr.Zero)
      return;

    _nativeWrapper = Marshal.PtrToStructure<NativeSessionWrapper>(wrapperPtr);
  }

  public int GetARSessionVersion()
  {
    return _nativeWrapper.version;
  }

  public IntPtr GetARSessionPtr()
  {
    return _nativeWrapper.session;
  }

  public double GetTimestamp()
  {
    return _currentArTimestamp;
  }

  // public bool GetIsAttached()
  // {
  //   return _attached;
  // }

  public bool IsPluginAttachedToSession()
  {
    return _attached;
  }
}

