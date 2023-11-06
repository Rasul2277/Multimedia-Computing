using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMatrix2 : MonoBehaviour
{
    public Vector3 translation = Vector3.zero;
    public Vector3 rotation = Vector3.zero;
    public Vector3 scale = Vector3.one;

    // Start is called before the first frame update

    private void OnDrawGizmos()
    {
        Vector3[] cube = {
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
        };

        int[] cubeEdges = {
            0, 1, 1, 3, 3, 2, 2, 0,
            4, 5, 5, 7, 7, 6, 6, 4,
            0, 4, 1, 5, 2, 6, 3, 7
        };


        Vector4 translationVector = new Vector4(translation.x, translation.y, translation.z, 1.0f);
        Matrix4x4 transformMatrix = Matrix4x4.TRS(translationVector, Quaternion.Euler(rotation), scale);

        for (int i = 0; i < cube.Length; i++)
        {
            cube[i] = transformMatrix.MultiplyPoint(cube[i]);
        }

        Gizmos.color = Color.blue;

        for (int i = 0; i < cubeEdges.Length; i += 2)
        {
            Vector3 p1 = cube[cubeEdges[i]];
            Vector3 p2 = cube[cubeEdges[i + 1]];
            Gizmos.DrawLine(p1, p2);
        }
    }
}