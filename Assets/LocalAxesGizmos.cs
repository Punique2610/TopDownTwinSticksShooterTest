using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LocalAxesGizmos : MonoBehaviour
{
    public float axisLength = 2;
    public Vector3 OffSet;
    private void OnDrawGizmosSelected()
    {
        if (Selection.activeGameObject != gameObject)
            return;
        
        // Draw X axis in red
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector3(transform.position.x + OffSet.x, transform.position.y + OffSet.y, transform.position.z + OffSet.z), transform.right * axisLength);

        // Draw Y axis in green
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(transform.position.x + OffSet.x, transform.position.y + OffSet.y, transform.position.z + OffSet.z), transform.up * axisLength);

        // Draw Z axis in blue
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Vector3(transform.position.x + OffSet.x, transform.position.y + OffSet.y, transform.position.z + OffSet.z), transform.forward * axisLength);

    }

    
}
