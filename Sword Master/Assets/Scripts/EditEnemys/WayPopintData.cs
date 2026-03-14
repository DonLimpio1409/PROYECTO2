using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WayPointData", menuName = "WayPointData/DataSet", order = 1)]
public class WayPopintData : ScriptableObject
{
    [System.Serializable]
    public struct WayPoint 
    {
        public Vector3 wayPointPosition;
        public WayPoint(Vector3 _wayPointPosition)
        {
            wayPointPosition = _wayPointPosition;
        }
    }

    public List<WayPoint> wayPointList = new List<WayPoint>();

    public void AddWayPoint(Vector3 _wayPointPosition)
    {
        WayPoint newWayPoint = new WayPoint(_wayPointPosition);
        wayPointList.Add(newWayPoint);
    }
}
