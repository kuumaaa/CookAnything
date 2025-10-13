using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //components
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject hand;

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private GameObject cameraObject;

    //jumping
    public float jumpForce = 5f;
    private HeadRotation _cameraRotation;
    private float _gravityConst = 9.81f;
    private float _groundRayLength = 0.6f;
    private Vector3 _moveDirection = Vector3.zero;

    private bool _once = true;

    //movement vectors
    private Vector3 _rotatedForward = Vector3.forward;
    private Vector3 _rotatedRight = Vector3.right;

    private float _rotationDuration = 0.15f;

    //constants
    private float _speed = 5.0f;
    private InputSystem_Actions input = null;
    private Vector2 inputVector = Vector2.zero;
    
    
    private float pickupRange = 3f;      

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void Start()
    {
        _cameraRotation = head.GetComponent<HeadRotation>();
        ApplyGravity();
    }

    void FixedUpdate()
    {
        Movement();
        DetectObject();
        
    }
    

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += OnMovementPerformed;
        input.Player.Move.canceled += OnMovementCancelled;

        input.Player.Jump.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= OnMovementPerformed;
        input.Player.Move.canceled -= OnMovementCancelled;

        input.Player.Jump.performed -= OnJumpPerformed;
    }

    private void Movement()
    {
        _rotatedForward = _cameraRotation.GetRotatedForwardVector();
        _rotatedRight = _cameraRotation.GetRotatedRightVector();
        _moveDirection = Vector3.zero;

        _moveDirection += inputVector.x * _rotatedRight;
        _moveDirection += inputVector.y * _rotatedForward;

        rigidbody.MovePosition(rigidbody.position + _moveDirection.normalized * (_speed * Time.deltaTime));
    }

    public void ApplyGravity()
    {
        Physics.gravity = -transform.up * _gravityConst;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        inputVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        inputVector = Vector2.zero;
    }

    private void OnJumpPerformed(InputAction.CallbackContext value)
    {
        Ray groundRay = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(groundRay, _groundRayLength))
        {
            rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void DetectObject()
    {
        Ray ray = cameraObject.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            if (hit.rigidbody != null && hit.collider.CompareTag("Object"))
            {
                Debug.Log("hit an object!");
                /*
                heldObject = hit.rigidbody;
                heldObject.useGravity = false;
                heldObject.drag = 10;
                heldObject.transform.parent = holdPosition;
                */
            }
        }
    }
    
}
