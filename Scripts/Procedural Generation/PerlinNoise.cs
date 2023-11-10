using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    private int pixWidth;
    private int pixHeight;

    public float xOrg;
    public float yOrg;

    public float scale = 1.0F;

    private Texture2D noiseTex;
    private Color[] pix;
    private Renderer rend;

    void Start()
    {

        pixWidth = GetComponent<Plane>().sizeOfMap.x;
        pixHeight = GetComponent<Plane>().sizeOfMap.y;

        rend = GetComponent<Renderer>();

        noiseTex = new Texture2D(pixWidth, pixHeight);
        pix = new Color[noiseTex.width * noiseTex.height];
        rend.material.mainTexture = noiseTex;

    }

    void CalcNoise()
    {



        for (float y = 0; y < noiseTex.height; y++)
        {
            for (float x = 0; x < noiseTex.width; x++)
            {
                float xCoord = xOrg + x / noiseTex.width * scale;
                float yCoord = yOrg + y / noiseTex.height * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);

                pix[(int)y * noiseTex.width + (int)x] = new Color(sample, sample, sample);
            }
        }

        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }

    void Update()
    {
        CalcNoise();
    }
}
