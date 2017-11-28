using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quat : MonoBehaviour {

    /// <summary>
    /// Get the rotation in quaternion from a object to a target in a 2D space  
    /// </summary>
    /// <param name="from">The viewer</param>
    /// <param name="target">The object to point</param>
    /// <returns>Returns the rotation (Already transformed) in quaternions</returns>
    public static Quaternion LookAt(Transform from, Transform target)
    {
        Vector3 dirV = target.position - from.position;
        float dir = Mathf.Atan2(dirV.y, dirV.x);
        dir *= Mathf.Rad2Deg;
        Quaternion Rotation = Quaternion.Euler(0f, 0f, dir);
        return Rotation;
    }
    public static Quaternion LookAt(Transform from, Transform target,float extraRot)
    {
        Vector3 dirV = target.position - from.position;
        float dir = Mathf.Atan2(dirV.y, dirV.x);
        dir *= Mathf.Rad2Deg;
        Quaternion Rotation = Quaternion.Euler(0f, 0f, dir+extraRot);
        return Rotation;
    }
}
