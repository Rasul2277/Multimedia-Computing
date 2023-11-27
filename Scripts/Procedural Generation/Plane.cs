using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Plane : MonoBehaviour
{
    public Vector2Int sizeOfMap;
    public Gradient[] gradients;
    public NoiseSettings setting;
    public Material material;
    public Tree treeScript;
    public float amplitude;

    private Vector3[] verts;
    private Vector2[] uvs;
    private Mesh mesh;
    private float[,] noise;
    private int[] triang;

    private Vector2 OffsetTemp;
    
    private void OnEnable()
    {
        Init();
        mesh = new Mesh
        {
            name = "Procedural Generation"
        };

    }

    private void Update()
    {
       
        if (OffsetTemp != setting.offset)
        {
            Init();
            
            mesh.vertices = CalcVertices();
            mesh.triangles = CalcTriangles();
            mesh.uv = CalcUV();

            
            GetComponent<MeshFilter>().sharedMesh = mesh;

            OffsetTemp = setting.offset;
            
        }
        

    }

    private void Init()
    {
        noise = PerlinNoise.GenerateNoiseMap(sizeOfMap.x, sizeOfMap.y, setting, new Vector2(0, 0));
        
        Texture2D texture = new Texture2D(sizeOfMap.x, sizeOfMap.y);
        System.Random rand = new System.Random();

        
        int count = 0;
        treeScript.Delete();

        for (int y = 0; y < sizeOfMap.y; y++)
        {
            for (int x = 0; x < sizeOfMap.x; x++)
            {
                Color color = new Color(0, 0, 0, 1); ;

                for (int i = 0; i < gradients.Length; i++)
                {
                    
                    if (gradients[i].startHight <= noise[x, y] && noise[x, y] < gradients[i].endHight)
                    {
                        color = Gradient.CalcColorFromGrad(gradients[i], noise[x, y]);
                        break;
                    }

                    
                }
             
                if (noise[x,y] >= 0.5 && (x + OffsetTemp.x) % 24 == 0 && (y - OffsetTemp.y) % 28 == 0 && count < treeScript.plants.Length)
                {
                    treeScript.Generate(new Vector3(
                        x,
                        noise[x,y] * amplitude - 1,
                        y)
                    );
                    count++;
                } 

                texture.SetPixel(x, y, color);
                
;           }
        }
       
        texture.Apply();
        GetComponent<Renderer>().material.mainTexture = texture;
        


    }

    private Vector3[] CalcVertices()
    {
        verts = new Vector3[(sizeOfMap.x + 1) * (sizeOfMap.y + 1)];
        int z = 0; 
        
        for (int i = 0; z < sizeOfMap.y; z++)
        {
            for (int x = 0; x < sizeOfMap.x; x++)
            {
                
                verts[i++] = new Vector3(x, noise[x, z] * amplitude, z);
            }
        }


        return verts;
    }

    private int[] CalcTriangles()
    {
        triang = new int[sizeOfMap.x * sizeOfMap.y * 6];
        int tri = 0;
        int vert = 0;

        for (int z = 0; z < sizeOfMap.x - 1; z++)
        {
            for (int x = 0; x < sizeOfMap.x - 1; x++)
            {
                triang[tri++] = vert;
                triang[tri++] = vert + sizeOfMap.x;
                triang[tri++] = vert + 1;
                triang[tri++] = vert + 1;
                triang[tri++] = vert + sizeOfMap.x;
                triang[tri++] = vert + sizeOfMap.x + 1;

                vert++;
            }

            vert++;
        }

        return triang;
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

[System.Serializable]
public struct Gradient
{
    public string name;
    public Color startColor;
    public Color endColor;

    public float startHight;
    public float endHight;

    public Gradient(string name, Color startColor, Color endColor, float startHight, float endHight)
    {
        this.name = name;
        this.startColor = startColor;
        this.endColor = endColor;
        this.startHight = startHight;
        this.endHight = endHight;
    }

    public static Color CalcColorFromGrad(Gradient grad, float hight)
    {
        float kR = (grad.endColor.r - grad.startColor.r) / (Mathf.Abs(grad.endHight - grad.startHight));
        float kG = (grad.endColor.g - grad.startColor.g) / (Mathf.Abs(grad.endHight - grad.startHight));
        float kB = (grad.endColor.b - grad.startColor.b) / (Mathf.Abs(grad.endHight - grad.startHight));

        return new Color(
            kR * (hight - grad.startHight) + grad.startColor.r,
            kG * (hight - grad.startHight) + grad.startColor.g,
            kB * (hight - grad.startHight) + grad.startColor.b, 
            1
        );
    }
}
