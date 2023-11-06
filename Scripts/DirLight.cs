using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirLight : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion ane = transform.rotation;

        if (Input.GetKey(KeyCode.I)) ane.x += Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.K)) ane.x -= Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.J)) ane.z -= Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.L)) ane.z += Speed * Time.deltaTime;

        transform.rotation = ane;
    }
}
