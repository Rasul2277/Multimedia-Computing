using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLevelPlane : MonoBehaviour
{
    Vector3[] vert;

    public int Squares = 1;
    void OnEnable()
    {
        GeneratePlane();


    }

    void GeneratePlane()
    {
        int Points = 4 + 4 * Squares;
        var mesh = new Mesh
        {
            name = "Procedural Mesh3"
        };

        vert = new Vector3[Points * Points];
        int[] tria = new int[(Points - 1) * (Points - 1) * 2 * 3];

        int cou = 0;
        for (int x = 0; x < Points; x++)
        {
            for (int z = 0; z < Points; z++)
            {

                vert[cou++] = new Vector3(x / 4, 0
                    , z / 4);

            }

        }

        int count = 0;
        for (int i = 0; i < Points * (Points - 1); i++)
        {
            if ((i + 1) % Points != 0)
            {
                tria[count++] = i;
                tria[count++] = i + 1;
                tria[count++] = Points + i + 1;

                tria[count++] = i;
                tria[count++] = Points + i + 1;
                tria[count++] = Points + i;
            }



        }

        mesh.vertices = vert;
        mesh.triangles = tria;
        Vector2[] uvs = new Vector2[mesh.vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].z) / 256;
        }

        mesh.uv = uvs;
        GetComponent<MeshFilter>().mesh = mesh;

    }

} 