using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using System.Runtime.InteropServices;

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

  void Start()
  {
    GetARNativePointer();
  }

  private void GetARNativePointer()
  {
    if (arSession == null)
      return;

    _sessionSubsystem = arSession.subsystem;

    if (_sessionSubsystem == null)
      return;

    IntPtr wrapperPtr = arSession.subsystem.nativePtr;

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
}

