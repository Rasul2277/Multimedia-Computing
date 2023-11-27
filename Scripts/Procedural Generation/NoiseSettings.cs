using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public float scale = 50;

    public int octaves = 6;
    [Range(0, 1)]
    public float persistance = .6f;
    public float lacunarity = 2;

    public int seed;
    public Vector2 offset;



}
