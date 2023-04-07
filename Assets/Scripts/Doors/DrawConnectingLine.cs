using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawConnectingLine : MonoBehaviour
{
    [ExecuteInEditMode]
    public Transform A;
    public Transform B;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(A.position, B.position);
    }

}
