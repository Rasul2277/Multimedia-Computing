using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hash
{
    protected float x = 0f;
    protected float y = 0f;
    protected float z = 0f;

    public Hash(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public int UniqueValue()
    {
        float alpha = Mathf.Abs(Mathf.Sin(x) / Sigmoid(x) / Mathf.Cos(z));
        float beta = Mathf.Abs(Mathf.Sin(y) / Sigmoid(y) / Mathf.Cos(x));
        float gamma = Mathf.Abs(Mathf.Sin(z) / Sigmoid(z) / Mathf.Cos(y));
        return (int)Mathf.Ceil(alpha * beta + beta * gamma + alpha * gamma);
    }

    private static float Sigmoid(float x)
    {
        return 1 / (1 + Mathf.Pow(2.7182818f, x));
    }
}
