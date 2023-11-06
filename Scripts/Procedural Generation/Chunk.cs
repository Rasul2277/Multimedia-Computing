using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chunk : MonoBehaviour
{

    public int width = 16;
    public int height = 16;
    public GameObject Cube;
    public Vector2Int Offset = Vector2Int.zero;
    

    private bool CreateOnce = false;
    private int count = 0;
    private GameObject[] Cubes;
    private GameObject[] CubesTemp;
    [SerializeField]
    private int Scale;
    private Vector2Int OsGl;
    public Color[] Colors;

    private float PreviousHash = -1;
    private float Hash = -1;

    public MapConfigurations GlobalVariables;

    // Update is called once per frame

    void Update()
    {
        Scale = GlobalVariables.Scale;
        OsGl = GlobalVariables.Offset;

        Hash = (Offset.x + Offset.y + OsGl.x) / (Offset.y - Offset.x + OsGl.y + 0.01f);


        if (CreateOnce)
        {
            CubesTemp = Cubes;
            count = 0;


            DeleteCubes();
            Cubes = new GameObject[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float P = Mathf.PerlinNoise((float)(x + Offset.x + OsGl.x) / width * Scale, (float)(y + Offset.y + OsGl.y) / height * Scale);
                    CreateCubes(x, y, P);

                }
            }

            CreateOnce = false;
        }

        else if (Hash != PreviousHash)
        {
            CreateOnce = true;
            PreviousHash = Hash;
        }

    }

    void CreateCubes(int x, int y, float Height)
    {
        Cubes[count] = Instantiate(
            Cube,
            transform.position + new Vector3(x - transform.localScale.x * 5, Height * 5f / Scale, y - transform.localScale.y * 5),
            transform.rotation,
            gameObject.transform);


        if (Height <= 0 ) Cubes[count].GetComponent<MeshRenderer>().material.color = Color.black;
        else
        for (int i = 0; i < Colors.Length; i++)
        {
            if (InRange(i * Colors.Length / 256f, Height, (i + 1) * Colors.Length / 256f))

                Cubes[count].GetComponent<MeshRenderer>().material.color = Colors[Colors.Length - i - 1];

                

        }


        count++;
        
    }

    void DeleteCubes()
    {
        if (CubesTemp != null)
            foreach (GameObject GObj in CubesTemp)
            {
                Destroy(GObj);
            }
    }


    public static bool InRange(float min, float x, float max)
    {
        return min < x && x <= max;
    }

}
