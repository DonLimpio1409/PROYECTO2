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
        public float waitTime;
        public WayPoint(Vector3 _wayPointPosition, float _waitTime)
        {
            wayPointPosition = _wayPointPosition;
            waitTime = _waitTime;
        }
    }

    public List<WayPoint> wayPointList = new List<WayPoint>();

    public void AddWayPoint(Vector3 _wayPointPosition, float _waitTime)
    {
        WayPoint newWayPoint = new WayPoint(_wayPointPosition, _waitTime);
        wayPointList.Add(newWayPoint);
    }
}
