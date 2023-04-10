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

        Gizmos.DrawWireSphere(A.position, 0.5f);
        Gizmos.DrawWireSphere(B.position, 0.5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(A.position, transform.forward);
        Gizmos.DrawRay(B.position, transform.forward);

    }

}
