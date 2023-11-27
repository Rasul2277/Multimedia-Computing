using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    public float speed;
    public NoiseSettings noiseSetting;
    // Start is called before the first frame update
    private void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        noiseSetting.offset += new Vector2(hor * Time.deltaTime * speed, -vert * Time.deltaTime * speed);
        

    }
}
