using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction wasd;
    public Vector2 _inputVector;
    public CharacterController controller;
    public float speed = 3.14f;

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
    void Update()
    {
        _inputVector = wasd.ReadValue<Vector2>();
       
        Vector3 finalVector = new Vector3();
        finalVector.x = _inputVector.x;
        finalVector.z = _inputVector.y;
       
        controller.Move(_inputVector * Time.deltaTime * speed); 

    }
}
