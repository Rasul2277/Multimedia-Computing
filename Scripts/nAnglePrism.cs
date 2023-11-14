using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class nAnglePrism : MonoBehaviour
{

    public Texture2D cubeTexture;
   
    
    


    public enum Task {Cone, Cylinder, Cube, Frustrum};
    public Task type;

    public int points = 16;

    public float coneRadius = 1;
    public float coneHeight = 1;
    

    public float CylRadius = 1;
    public float CylHeight = 1;

    public float CubeSize = 1;


    public float FustrumTopSize = 1;
    public float FustrumBottomSize = 1.5f;
    public float FustrumHeight = 1;




    int[] tria;
    Mesh mesh;
    Vector2[] uvs;

    Vector3[] vert;
    void OnEnable()
    {
        mesh = new Mesh
        {
            name = "Procedural Mesh2"
        };

        
        

        
    }

    private void Update()
    {
        

        if (type == Task.Cone) GenerateCone();

        else if (type == Task.Cylinder) GenerateCylinder();

        else if (type == Task.Cube) GenerateCube();

        else if (type == Task.Frustrum) GenerateFustrum();



        GenerateTriangles();
        GenerateUV();


        mesh.vertices = vert;
        mesh.triangles = tria;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    void GenerateCone()
    {
        vert = new Vector3[points * 2 + 2];
        tria = new int[points * 4 * 3];

        for (int i = 0; i < points; i++)
        {

            vert[i] = new Vector3(
                0 * Mathf.Cos(360f / points * i * Mathf.Deg2Rad),
                coneHeight / 2,
                0 * Mathf.Sin(360f / points * i * Mathf.Deg2Rad)
            );

            vert[i + points] = new Vector3(
                coneRadius * Mathf.Cos(360f / points * i * Mathf.Deg2Rad),
                -coneHeight / 2,
                coneRadius * Mathf.Sin(360f / points * i * Mathf.Deg2Rad)
            );

        }

        vert[vert.Length - 2] = new Vector3(0, coneHeight / 2, 0);
        vert[vert.Length - 1] = new Vector3(0, -coneHeight / 2, 0);
    }

    void GenerateCylinder()
    {
        vert = new Vector3[points * 2 + 2];
        tria = new int[points * 4 * 3];

        for (int i = 0; i < points; i++)
        {

            vert[i] = new Vector3(
                CylRadius * Mathf.Cos(360f / points * i * Mathf.Deg2Rad),
                CylHeight / 2,
                CylRadius * Mathf.Sin(360f / points * i * Mathf.Deg2Rad)
            );

            vert[i + points] = new Vector3(
                CylRadius * Mathf.Cos(360f / points * i * Mathf.Deg2Rad),
               -CylHeight / 2,
                CylRadius * Mathf.Sin(360f / points * i * Mathf.Deg2Rad)
            );

        }

        vert[vert.Length - 2] = new Vector3(0, CylHeight / 2, 0);
        vert[vert.Length - 1] = new Vector3(0, -CylHeight / 2, 0);
    }


    void GenerateCube()
    {
        points = 4;
        vert = new Vector3[points * 2 + 2];
        tria = new int[points * 4 * 3];

        for (int i = 0; i < points; i++)
        {

            
            vert[i] = new Vector3(
                CubeSize * Mathf.Cos((360f / points * i + 45) * Mathf.Deg2Rad) / Mathf.Sqrt(2),
                CubeSize / 2,
                CubeSize * Mathf.Sin((360f / points * i + 45) * Mathf.Deg2Rad) / Mathf.Sqrt(2)
            );

            vert[i + points] = new Vector3(
                CubeSize * Mathf.Cos((360f / points * i + 45) * Mathf.Deg2Rad) / Mathf.Sqrt(2),
               -CubeSize / 2,
                CubeSize * Mathf.Sin((360f / points * i + 45) * Mathf.Deg2Rad) / Mathf.Sqrt(2)
            );

        }

        vert[vert.Length - 2] = new Vector3(0, CubeSize / 2, 0);
        vert[vert.Length - 1] = new Vector3(0, -CubeSize / 2, 0);
    }



    void GenerateFustrum()
    {
        points = 4;
        vert = new Vector3[points * 2 + 2];
        tria = new int[points * 4 * 3];

        for (int i = 0; i < points; i++)
        {
           
            vert[i] = new Vector3(
                FustrumTopSize * Mathf.Cos((360f / points * i + 45) * Mathf.Deg2Rad),
                FustrumHeight / 2,
                FustrumTopSize * Mathf.Sin((360f / points * i + 45) * Mathf.Deg2Rad)
            );

            vert[i + points] = new Vector3(
                FustrumBottomSize * Mathf.Cos((360f / points * i + 45) * Mathf.Deg2Rad),
               -FustrumHeight / 2,
                FustrumBottomSize * Mathf.Sin((360f / points * i + 45) * Mathf.Deg2Rad)
            );

        }

        vert[vert.Length - 2] = new Vector3(0, FustrumHeight / 2, 0);
        vert[vert.Length - 1] = new Vector3(0, -FustrumHeight / 2, 0);
    }



    void GenerateTriangles()
    {
        int count = 0;
        for (int i = 0; i < points; i++)
        {
            if (i + 1 != points)
            {
                tria[count++] = i + 1;
                tria[count++] = i + points;
                tria[count++] = i;

                tria[count++] = i + 1;
                tria[count++] = i + points + 1;
                tria[count++] = i + points;

            }
            else
            {
                tria[count++] = 0;
                tria[count++] = i + points;
                tria[count++] = i;

                tria[count++] = i + 1;
                tria[count++] = i + points;
                tria[count++] = 0;
            }
        }








        for (int i = 0; i < points; i++)
        {
            if (i + 1 != points)
            {
                tria[count++] = vert.Length - 2;
                tria[count++] = i + 1;
                tria[count++] = i;
                tria[count++] = i + points;
                tria[count++] = i + points + 1;
                tria[count++] = vert.Length - 1;
            }
            else
            {
                tria[count++] = vert.Length - 2;
                tria[count++] = 0;
                tria[count++] = i;
                tria[count++] = i + points;
                tria[count++] = points;
                tria[count++] = vert.Length - 1;
            }

        }
    }
    void GenerateUV()
    {
        uvs = new Vector2[mesh.vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].z);
        }
    }
}
