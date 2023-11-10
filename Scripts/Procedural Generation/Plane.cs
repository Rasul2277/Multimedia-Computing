using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Plane : MonoBehaviour
{
    public Vector2Int sizeOfMap;
    public float amplitude;
    private Vector3[] verts;
    private int[] triang;
    private float xOrg;
    private float yOrg;
    private float scale;
    private Mesh mesh;
    private Vector2[] uvs;

    private void OnEnable()
    {
        Init();
        mesh = new Mesh
        {
            name = "Procedural Generation"
        };

        mesh.vertices = CalcVertices();
        mesh.triangles = CalcTriangles();
        mesh.uv = CalcUV();

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    private void Update()
    {
        Init();
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }


    private void Init()
    {
        xOrg = GetComponent<PerlinNoise>().xOrg;
        yOrg = GetComponent<PerlinNoise>().yOrg;
        scale = GetComponent<PerlinNoise>().scale;
    }
    

    private int[] CalcTriangles()
    {
        triang = new int[sizeOfMap.x * sizeOfMap.y * 6];
        int tri = 0;
        int vert = 0;

        for (int z = 0; z < sizeOfMap.x; z++)
        {
            for (int x = 0; x < sizeOfMap.x; x++)
            {
                triang[tri++] = vert;
                triang[tri++] = vert + sizeOfMap.x + 1;
                triang[tri++] = vert + 1;
                triang[tri++] = vert + 1;
                triang[tri++] = vert + sizeOfMap.x + 1;
                triang[tri++] = vert + sizeOfMap.x + 2;

                vert++;
            }

            vert++;
        }

        return triang;
    }

    private Vector3[] CalcVertices()
    {
        verts = new Vector3[(sizeOfMap.x + 1) * (sizeOfMap.y + 1)];
        int z = 0;
        for (int i = 0; z <= sizeOfMap.y; z++)
        {
            for (int x = 0; x <= sizeOfMap.x; x++)
            {
                verts[i++] = new Vector3(x, 0, z);
            }
        }

        return verts;
    }

    private Vector2[] CalcUV()
    {
        uvs = new Vector2[verts.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(verts[i].x, verts[i].z) / sizeOfMap;
        }
        return uvs;
    }

}
