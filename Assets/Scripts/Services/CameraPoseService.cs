using UnityEngine;

public readonly struct CameraPose
{
  public Vector3 Position { get; }
  public Quaternion Rotation { get; }

  public CameraPose(Vector3 position, Quaternion rotation)
  {
    Position = position;
    Rotation = rotation;
  }
}

public interface IARCameraPoseService
{
  CameraPose GetUnityARSessionCameraPose();
}

public class ARCameraPoseService : IARCameraPoseService
{
  private readonly Camera _arCamera;

  public ARCameraPoseService(Camera arCamera)
  {
    _arCamera = arCamera;
  }
  public CameraPose GetUnityARSessionCameraPose()
  {
    Transform ct = _arCamera.transform;
    return new CameraPose(ct.position, ct.rotation);
  }
}
