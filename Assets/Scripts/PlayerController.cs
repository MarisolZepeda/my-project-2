 using System.Collections.Specialized;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 10)]
    private float acceleration = 6f;
    [SerializeField]
    [Range(0.1f, 1)]
    private float maxSpeed = 40;
    [SerializeField]
    [Range(0, 1)]
    private float friccion = 0.9f;
    [SerializeField]
    [Range(0, 0.1f)]
    private float tolerance = 0.01f;
    
    [SerializeField]  

    public Vector2 _inputVector;
    private CharacterController controller;
    private Vector3 _velocity;

    public InputAction wasd;
    public float speed = 3.14f;


    void FixedUpdate()
    {
        Vector3 direction = new Vector3(_inputVector.x, 0, _inputVector.y);
        _velocity += direction * acceleration * Time.deltaTime;
        _velocity = Vector3.ClampMagnitude(_velocity, maxSpeed);
        if(_velocity.magnitude<tolerance)
        {
            _velocity = Vector3.zero;
        } else
        {
            controller.Move(_velocity);
        }
        _velocity *= friccion;
    }

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
