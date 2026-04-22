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

public interface IARSessionStatusService
{
  bool AttachPluginToSession();
  void DetachPluginFromSession();
  bool IsPluginAttachedToSession();
  int GetARSessionVersion();
  IntPtr GetARSessionPtr();
}

public class ARSessionStatusService : IARSessionStatusService
{
  private XRSessionSubsystem _sessionSubsystem;
  private NativeSessionWrapper _nativeWrapper;
  private ARSession _arSession;

  private bool _isPluginAttached;

  public ARSessionStatusService(ARSession arSession)
  {
    _arSession = arSession;
  }

  public bool AttachPluginToSession()
  {
    _sessionSubsystem = _arSession.subsystem;
    if (_sessionSubsystem == null)
      return false;

    IntPtr wrapperPtr = _sessionSubsystem.nativePtr;
    if (wrapperPtr == IntPtr.Zero)
      return false;

    _nativeWrapper = Marshal.PtrToStructure<NativeSessionWrapper>(wrapperPtr);

    _isPluginAttached = CoordinatorApi.AttachSession(wrapperPtr);
    return _isPluginAttached;
  }

  public void DetachPluginFromSession()
  {
    CoordinatorApi.DetachSession();
  }

  public bool IsPluginAttachedToSession() 
  {
    return _isPluginAttached;
  }

  public int GetARSessionVersion()
  {
    return _nativeWrapper.version;
  }

  public IntPtr GetARSessionPtr()
  {
    return _nativeWrapper.session;
  }
}
