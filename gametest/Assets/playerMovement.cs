using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;

    float speed = 2.5f;
    float speedRotate = 75.0f;

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.wKey.isPressed)
            {
                Vector3 movement = new Vector3(0.0f, 0.0f, 1.0f * Time.deltaTime * speed);
                movement = transform.TransformDirection(movement);
                controller.Move(movement);
            }
            if (keyboard.aKey.isPressed)
            {
                Vector3 rotation = new Vector3(0.0f, -1.0f * Time.deltaTime * speedRotate, 0.0f);
                transform.Rotate(rotation);
            }
            if (keyboard.sKey.isPressed)
            {
                Vector3 movement = new Vector3(0.0f, 0.0f, -1.0f * Time.deltaTime * speed);
                movement = transform.TransformDirection(movement);
                controller.Move(movement);
            }
            if (keyboard.dKey.isPressed)
            {
                Vector3 rotation = new Vector3(0.0f, 1.0f * Time.deltaTime * speedRotate, 0.0f);
                transform.Rotate(rotation);
            }
        }
    }
}