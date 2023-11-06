using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConfigurations : MonoBehaviour
{
    public Vector2Int Offset = Vector2Int.zero;
    public Vector2 Speed = Vector2.zero;
    private Vector2 OffsetF = Vector2.zero;

    public int Scale = 1;
    public int ChunkValue = 1;

    public GameObject Chunk;

    private void Start()
    {
        for (int y = 0; y < ChunkValue; y++)
        {
            for (int x = 0; x < ChunkValue; x++)
            {
                GameObject GO = Instantiate(Chunk, new Vector3(16 * x, 0, 16 * y), transform.rotation, gameObject.transform);
                GO.GetComponent<Chunk>().Offset = new Vector2Int(16 * x, 16 * y);
                GO.GetComponent<Chunk>().GlobalVariables = GetComponent<MapConfigurations>();
            } 
        }
    }

    private void Update()
    {
        OffsetF += Speed * Time.deltaTime;

        Offset = new Vector2Int((int) OffsetF.x, (int) OffsetF.y);

    }
}
