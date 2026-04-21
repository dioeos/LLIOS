using UnityEngine;

public class AppCoordinator : MonoBehaviour
{
  [Header("Required Game Objects")]
  [SerializeField]
  private Camera arCamera;

  [SerializeField]
  private SessionManager sessionManager;

  void Awake() 
  {
    IARCameraPoseService poseService = new ARCameraPoseService(arCamera);

    //initialize managers with needed services
    sessionManager.Initialize(poseService);
      
  }
}
