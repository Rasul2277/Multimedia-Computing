using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class nAnglePrism : MonoBehaviour
{

    public Texture2D cubeTexture;
    public int Points = 16;
    public float Height = 1;
    public float TopRadius = 1;
    public float BottomRadius = 1;
    Vector3[] vert;
    void OnEnable()
    {
        var mesh = new Mesh
        {
            name = "Procedural Mesh2"
        };

        vert = new Vector3[Points * 2 + 2];
        int[]     tria = new int    [Points * 4 * 3];


        for (int i = 0; i < Points; i++)
        {

            vert[i] = new Vector3( 
                TopRadius * Mathf.Cos(360f / Points * i * Mathf.Deg2Rad),
                Height/2,
                TopRadius * Mathf.Sin(360f / Points * i * Mathf.Deg2Rad)
            );

            vert[i+Points] = new Vector3(
                BottomRadius * Mathf.Cos(360f / Points * i * Mathf.Deg2Rad),
                -Height/2,
                BottomRadius * Mathf.Sin(360f / Points * i * Mathf.Deg2Rad)
            );
    
        }

        vert[vert.Length - 2] = new Vector3(0, Height/2, 0);
        vert[vert.Length - 1] = new Vector3(0, -Height/2, 0);


        int count = 0;
        for (int i = 0; i < Points; i++)
        {
            if (i + 1 != Points)
            {
                tria[count++] = i + 1;
                tria[count++] = i + Points;
                tria[count++] = i;

                tria[count++] = i + 1;
                tria[count++] = i + Points + 1;
                tria[count++] = i + Points;
                
            }
            else
            {
                tria[count++] = 0;
                tria[count++] = i + Points;
                tria[count++] = i;

                tria[count++] = i + 1;
                tria[count++] = i + Points;
                tria[count++] = 0;
            }
        }

        for (int i = 0; i < Points; i++)
        {
            if (i + 1 != Points)
            {
                tria[count++] = vert.Length - 2;
                tria[count++] = i + 1;
                tria[count++] = i;
                tria[count++] = i + Points;
                tria[count++] = i + Points + 1;
                tria[count++] = vert.Length - 1;
            }
            else
            {
                tria[count++] = vert.Length - 2;
                tria[count++] = 0;
                tria[count++] = i;
                tria[count++] = i + Points;
                tria[count++] = Points;
                tria[count++] = vert.Length - 1;
            }
            
        }



        


        
        Vector2[] uvs = new Vector2[mesh.vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].z);
        }

        mesh.vertices = vert;
        mesh.triangles = tria;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
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
