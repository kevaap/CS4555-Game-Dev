using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public InputAction wasd;
    public CharacterController controller;
    void Start()
    {
      controller = GetComponent<CharacterController>();
    }
    void OnEnable()
    {
      wasd.Enable();
    }

    void OnDisable()
    {
      wasd.Disable();
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = wasd.ReadValue<Vector2>();

        Vector3 finalVector = new Vector3();
        finalVector.x = inputVector.x;
        finalVector.z = inputVector.y;

        controller.Move(finalVector * Time.deltaTime * 3.14f);
    }
}
