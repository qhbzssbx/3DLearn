using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUtility
{
    private static RaycastHit[] _reusableHits = new RaycastHit[10]; // 复用数组减少GC
    // 检测垂直向下的射线
    public static float DetectSlope(Vector3 point)
    {
        Vector3 origin = new Vector3(point.x, point.y + 1, point.z);
        const float maxDistance = 5f;
        RaycastHit hit;
        bool hasHit = Physics.Raycast(
            origin, 
            Vector3.down, 
            out hit, 
            maxDistance, 
            1 << LayerMask.NameToLayer("Terrain"),
            QueryTriggerInteraction.Ignore
        );
        return hasHit ? hit.point.y : point.y;

    }
}
