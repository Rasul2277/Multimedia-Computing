using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Tore : MonoBehaviour
{
    int[] tria;
    Mesh mesh;
    Vector2[] uvs;

    public int points;
    public int pointsOfCircle = 16;

    Vector3[] vert;
    void OnEnable()
    {
        mesh = new Mesh
        {
            name = "Procedural Mesh2"
        };  
        vert = new Vector3[points + pointsOfCircle * points];
        tria = new int[points * pointsOfCircle * 6];

        for (int i = 0; i < points; i++)
        {
            vert[i] = new Vector3(
                Mathf.Cos(360f / points * i * Mathf.Deg2Rad),
                0,
                Mathf.Sin(360f / points * i * Mathf.Deg2Rad)
                );
        }


        for (int j = 0; j < points; j++)
        {
            for (int i = 0; i < pointsOfCircle; i++)
            {
                vert[points + i + pointsOfCircle * j] = new Vector3(
                    0.25f * -Mathf.Cos((360f / pointsOfCircle * i) * Mathf.Deg2Rad)  ,
                    0.25f * Mathf.Sin((360f / pointsOfCircle  * i ) * Mathf.Deg2Rad) ,
                    0.25f * -Mathf.Sin((0) * Mathf.Deg2Rad)  
                );

                vert[points + i + pointsOfCircle * j] = Matrix4x4.Rotate(Quaternion.Euler(0, -360f/points * j, 0)).MultiplyVector(vert[points + i + pointsOfCircle * j]);

                vert[points + i + pointsOfCircle * j] += new Vector3(vert[j].x, vert[j].y, vert[j].z);
                 
                // 1, 0, 0
                // 1,  1, 0
                // 1,  0, 0  
                // 1, -1, 0  

                // 1, 0, 0
                // 0, 1, 0
                // -1, 0, 0
                // 0, -1, 0
                // 

            }
        }








        int index = 0;
        for (int i = 0; i < points; i++)
        {

            for (int j = 0; j < pointsOfCircle; j++)
            {
                tria[index++] = points + j + i * pointsOfCircle;
                tria[index++] = points + (pointsOfCircle + j + i * pointsOfCircle) % (points * pointsOfCircle);
                tria[index++] = points + (j + 1) % pointsOfCircle + i * pointsOfCircle;

                tria[index++] = points + (j + 1) % pointsOfCircle + i * pointsOfCircle;
                tria[index++] = points + (pointsOfCircle + j + i * pointsOfCircle) % (points * pointsOfCircle);


                tria[index++] = j == pointsOfCircle - 1 ? points + (pointsOfCircle + i * pointsOfCircle) % (points * pointsOfCircle) : points + ((pointsOfCircle + j) + i * pointsOfCircle + 1) % (points * pointsOfCircle); 
            }

        }


         




        mesh.vertices = vert;
        mesh.triangles = tria;
        GetComponent<MeshFilter>().mesh = mesh;

       


    }
    

    /*
    private void OnDrawGizmos()
    {
        foreach(Vector3 i in vert)
        Gizmos.DrawSphere(i, 0.05f); 
    }
    */
}
