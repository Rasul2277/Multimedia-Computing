using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rotationSpeed = 5;
    public float Speed = 5;
    public float mouseX;
    public float mouseY;    



    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            /*
            Change.RotateC.y += Input.GetAxis("Mouse X") * rotationSpeed;
            if (-90 < Change.RotateC.x && Change.RotateC.x < 90)
                Change.RotateC.x -= Input.GetAxis("Mouse Y") * rotationSpeed;
            else
                Change.RotateC.x += Input.GetAxis("Mouse Y") * rotationSpeed;

            /* transform.Rotate(Vector3.up, mouseX, Space.World);

             transform.Rotate(Vector3.left, mouseY, Space.Self);




             float moveHorizontal = Input.GetAxis("Horizontal");

             float moveVertical = Input.GetAxis("Vertical");

             Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);



             transform.Translate(movement * Speed * Time.fixedDeltaTime);
            */
        }
        else if (Input.GetKeyUp(KeyCode.Escape)) Cursor.lockState = CursorLockMode.None;


        
    }
}



