using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Parabolla : MonoBehaviour
{

    public int Points = 4;
    public float z = 1;

    Vector3[] vert;
    void Update()
    {
        var mesh = new Mesh
        {
            name = "Procedural Mesh2"
        };

        vert = new Vector3[Points * 2 + 2];
        int[] tria = new int[Points * 4 * 3];

        int i = 0;
        for (int x = -Points / 2; x <= Points / 2; x++)
        {



            vert[i] = new Vector3(
                x,
                x*x,
                z / 2

            );

            vert[i + Points + 1] = new Vector3(
                x,
                x * x,
               -z / 2
            );

        }







        int count = 0;
        for (i = 0; i < Points; i++)
        {
            tria[count++] = i + 1;
            tria[count++] = i + Points + 1;
            tria[count++] = i;

            tria[count++] = i + 1;
            tria[count++] = i + Points + 2;
            tria[count++] = i + Points + 1;
        }

        for (i = 0; i < Points; i++)
        {

            tria[count++] = i;
            tria[count++] = i + Points + 1;
            tria[count++] = i + 1;

            tria[count++] = i + Points + 1;
            tria[count++] = i + Points + 2;
            tria[count++] = i + 1;
        }






        mesh.normals = CalculateNormals(mesh.vertices);

        Vector2[] uvs = new Vector2[mesh.vertices.Length];

        for (i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].z);
        }

        mesh.vertices = vert;
        mesh.triangles = tria;
        mesh.uv = uvs;

        GetComponent<MeshFilter>().mesh = mesh;

    }

    private Vector3[] CalculateNormals(Vector3[] vertices)
    {
        Vector3[] normals = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            normals[i] = Vector3.up;
        }
        return normals;
    }



}