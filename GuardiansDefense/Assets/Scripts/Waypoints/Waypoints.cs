using UnityEngine;

public class Waypoints : SingletonInSceneNoInstance<Waypoints>
{
  [SerializeField] private WaypointData[] _listWaypointsData;

  //======================================

  protected override void Awake()
  {
    base.Awake();

    GetPaths();
  }

#if UNITY_EDITOR
  private void OnValidate()
  {
    GetPaths();
  }
#endif

  //======================================

  public Vector3[] GetWaypoints(int parPathIndex)
  {
    Vector3[] points = new Vector3[_listWaypointsData[parPathIndex].Waypoints.Length];

    for (int i = 0; i < points.Length; i++)
    {
      Vector3 waypointPosition = _listWaypointsData[parPathIndex].Waypoints[i].position;

      points[i] = waypointPosition;
    }

    return points;
  }

  public Vector3 GetPointPosition(int parPathIndex, int parPointIndex)
  {
    return _listWaypointsData[parPathIndex].Waypoints[parPointIndex].position;
  }

  private void GetPaths()
  {
    _listWaypointsData = new WaypointData[transform.childCount];

    for (int i = 0; i < transform.childCount; i++)
    {
      Transform path = transform.GetChild(i);

      Transform[] waypoints = new Transform[path.childCount];
      for (int j = 0; j < path.childCount; j++)
      {
        waypoints[j] = path.GetChild(j);
      }

      WaypointData waypointData = new WaypointData(path, waypoints);
      _listWaypointsData[i] = waypointData;
    }
  }

  //======================================

  private void OnDrawGizmos()
  {
    if (_listWaypointsData == null)
      return;

    foreach (var waypointData in _listWaypointsData)
    {
      for (int i = 0; i < waypointData.Waypoints.Length; i++)
      {
        if (i < waypointData.Waypoints.Length - 1)
        {
          Gizmos.color = Color.white;
          Gizmos.DrawLine(waypointData.Waypoints[i].position + transform.position, waypointData.Waypoints[i + 1].position + transform.position);
        }
      }
    }
  }

  //======================================

  [System.Serializable]
  public sealed class WaypointData
  {
    [SerializeField] private Transform _path;

    [SerializeField] private Transform[] _waypoints;

    //======================================

    public WaypointData(Transform parPath, Transform[] parWaypoints)
    {
      _path = parPath;
      _waypoints = parWaypoints;
    }

    //======================================

    public Transform Path => _path;

    public Transform[] Waypoints { get => _waypoints; set => _waypoints = value; }
  }

  //======================================
}