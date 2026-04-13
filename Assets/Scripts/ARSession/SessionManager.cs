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


public class SessionManager : MonoBehaviour
{
  [SerializeField]
  private ARSession arSession;

  private XRSessionSubsystem _sessionSubsystem;
  private NativeSessionWrapper _nativeWrapper;

  private bool _attached;
  private double _currentArTimestamp = 0.0;

  void Start()
  {
    MarshalNativePointer();
    
    if (_sessionSubsystem != null && _sessionSubsystem.nativePtr != IntPtr.Zero)
    {
        _attached = AttachSession(_sessionSubsystem.nativePtr);
    }
  }

  void Update()
  {
    if (_attached)
    {
      _currentArTimestamp = SessionManagerApi.GetSessionTimestamp();
    }
  }

  void OnDestroy()
  {
    DetachSession();
  }

  private void DetachSession()
  {
    SessionManagerApi.DetachSession();
  }

  private bool AttachSession(IntPtr sessionPointer)
  {
    if (sessionPointer == IntPtr.Zero)
      return false;

    bool isConnected = SessionManagerApi.AttachSession(sessionPointer);
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
}

