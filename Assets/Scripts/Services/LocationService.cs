using UnityEngine;
using Dioeos.UnityAppleReplayKit;

public interface ILocationService
{
  string GetLocationCoordinates();
}

public class LocationService: ILocationService
{
  public string GetLocationCoordinates()
  {
    return CoordinatorApi.GetLatestLocationString() ?? "Location unavailable";
  }

}


