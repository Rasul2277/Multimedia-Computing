using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Vector3 a = new Vector3(4,0,0);
        Vector3 b = new Vector3(2,4,0);
        Vector3 c = a - b;



        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.zero, a);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero, b);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero, c);


    }

}
