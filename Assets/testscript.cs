using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    public Vector3 relativePos;

    public Transform target;
    public Vector3 thisForward;
    public Vector3 targetForward;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        var from = transform.forward;
        var to = target.forward;

        from.Normalize();
        to.Normalize();

        thisForward = from;

        targetForward = to;

        Quaternion rotation = Quaternion.LookRotation(targetForward);

        // Step 2: Inverse rotation transforms from F space to E space
        Vector3 newD = Quaternion.Inverse(rotation) * thisForward;

        //var rotation = Quaternion.FromToRotation(Vector3.forward, to);

        var newDir = newD;

        //newDir.Normalize();

        relativePos = newDir;
    }
}
