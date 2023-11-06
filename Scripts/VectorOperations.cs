using UnityEngine;

public class VectorOperations : MonoBehaviour
{
    public Vector3 a;
    public Vector3 b;

    private Color colorA = Color.red;
    private Color colorB = Color.blue;
    private Color colorSum = Color.green;
    private Color colorDiff = Color.yellow;
    private Color colorNormalizedA = Color.magenta;
    private Color colorNormalizedB = Color.cyan;
    private Color colorCross = Color.white;

    private Vector3 sum;
    private Vector3 diff;
    private Vector3 normalizedA;
    private Vector3 normalizedB;
    private Vector3 crossProduct;

    private void OnDrawGizmos()
    {
        Gizmos.color = colorA;
        Gizmos.DrawRay(Vector3.zero, a);

        Gizmos.color = colorB;
        Gizmos.DrawRay(Vector3.zero, b);

        sum = a + b;
        diff = a - b;
        normalizedA = a.normalized;
        normalizedB = b.normalized;
        crossProduct = Vector3.Cross(a, b);

        Gizmos.color = colorSum;
        Gizmos.DrawRay(Vector3.zero, sum);

        Gizmos.color = colorDiff;
        Gizmos.DrawRay(Vector3.zero, diff);

        Gizmos.color = colorNormalizedA;
        Gizmos.DrawRay(Vector3.zero, normalizedA);

        Gizmos.color = colorNormalizedB;
        Gizmos.DrawRay(Vector3.zero, normalizedB);

        Gizmos.color = colorCross;
        Gizmos.DrawRay(Vector3.zero, crossProduct);
    }
}