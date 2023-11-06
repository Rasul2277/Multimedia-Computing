using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SimpleProceduralMesh: MonoBehaviour
{

    public Texture2D cubeTexture;

    void OnEnable()
    {
        var mesh = new Mesh
        {
            name = "Procedural Mesh"
        };

        mesh.vertices = new Vector3[] {

            new Vector3 (0, 0, 0), //A
            new Vector3 (1, 0, 0), //B
            new Vector3 (1, 1, 0), //C
            new Vector3 (0, 1, 0), //D


            new Vector3 (0, 0, 1), //A1
            new Vector3 (1, 0, 1), //B1
            new Vector3 (1, 1, 1), //C1
            new Vector3 (0, 1, 1), //D1

            new Vector3 (1, 1, 0), //C
            new Vector3 (0, 1, 0), //D
            new Vector3 (1, 1, 1), //C1
            new Vector3 (0, 1, 1), //D1

            new Vector3 (0, 0, 0), //A
            new Vector3 (1, 0, 0), //B
            new Vector3 (0, 0, 1), //A1
            new Vector3 (1, 0, 1), //B1
            

        };


        mesh.uv = new Vector2[]{
            new Vector2(0, 0),
            new Vector2(1, 0), 
            new Vector2(1, 1), 
            new Vector2(0, 1),  

            new Vector2(-1, 0), 
            new Vector2(0, 0), 
            new Vector2(0, 1), 
            new Vector2(-1, 1),  

            new Vector2(0, 0), 
            new Vector2(1, 0), 
            new Vector2(0, 1), 
            new Vector2(1, 1),  

            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),


        };


        mesh.triangles = new int[] {
            0, 3, 1,
            1, 3, 2,

            0, 7, 3,
            4, 7, 0,

            2, 5, 1,
            6, 5, 2,

            5, 7, 4,
            6, 7, 5,

            9, 11, 8,
            8, 11, 10,

            13, 14, 12,
            13, 15, 14,



        };


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
