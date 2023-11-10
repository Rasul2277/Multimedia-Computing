using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class nAnglePrism : MonoBehaviour
{

    public Texture2D cubeTexture;
    public int Qutality = 16;
    public float Height = 1;
    public float Radius = 1;
    public float BottomRadius = 1;
    Vector3[] vert;
    void OnEnable()
    {
        var mesh = new Mesh
        {
            name = "Procedural Mesh2"
        };

        vert = new Vector3[Qutality * 2 + 2];
        int[]     tria = new int    [Qutality * 4 * 3];


        for (int i = 0; i < Qutality; i++){

            vert[i] = new Vector3( 
                Radius * Mathf.Cos(360f / Qutality * i * Mathf.Deg2Rad),
                Height/2,
                Radius * Mathf.Sin(360f / Qutality * i * Mathf.Deg2Rad)
            );

            vert[i+Qutality] = new Vector3(
                BottomRadius * Mathf.Cos(360f / Qutality * i * Mathf.Deg2Rad),
                -Height/2,
                BottomRadius * Mathf.Sin(360f / Qutality * i * Mathf.Deg2Rad)
            );
    
        }

        vert[vert.Length - 2] = new Vector3(0, Height/2, 0);
        vert[vert.Length - 1] = new Vector3(0, -Height/2, 0);


        int count = 0;
        for (int i = 0; i < Qutality; i++)
        {
            if (i + 1 != Qutality)
            {
                tria[count++] = i + 1;
                tria[count++] = i + Qutality;
                tria[count++] = i;

                tria[count++] = i + 1;
                tria[count++] = i + Qutality + 1;
                tria[count++] = i + Qutality;
                
            }
            else
            {
                tria[count++] = 0;
                tria[count++] = i + Qutality;
                tria[count++] = i;

                tria[count++] = i + 1;
                tria[count++] = i + Qutality;
                tria[count++] = 0;
            }
        }

        for (int i = 0; i < Qutality; i++)
        {
            if (i + 1 != Qutality)
            {
                tria[count++] = vert.Length - 2;
                tria[count++] = i + 1;
                tria[count++] = i;
                tria[count++] = i + Qutality;
                tria[count++] = i + Qutality + 1;
                tria[count++] = vert.Length - 1;
            }
            else
            {
                tria[count++] = vert.Length - 2;
                tria[count++] = 0;
                tria[count++] = i;
                tria[count++] = i + Qutality;
                tria[count++] = Qutality;
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
