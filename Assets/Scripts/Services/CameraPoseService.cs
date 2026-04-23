using UnityEngine;

public interface IARCameraPoseService
{
  Transform GetUnityARSessionCameraTransform();
}

public class ARCameraPoseService : IARCameraPoseService
{
  private readonly Camera _arCamera;

  public ARCameraPoseService(Camera arCamera)
  {
    _arCamera = arCamera;
  }
  public Transform GetUnityARSessionCameraTransform()
  {
    return _arCamera.transform;
  }
}
