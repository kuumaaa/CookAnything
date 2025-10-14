using Unity.VisualScripting;
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
    public float jumpForce = 2.5f;
    private HeadRotation _cameraRotation;
    private float _gravityConst = 9.81f;
    private float _groundRayLength = 0.6f;
    private Vector3 _moveDirection = Vector3.zero;
    
    //movement vectors
    private Vector3 _rotatedForward = Vector3.forward;
    private Vector3 _rotatedRight = Vector3.right;

    private float _rotationDuration = 0.15f;

    //constants
    private float _speed = 2.5f;
    private InputSystem_Actions input = null;
    private Vector2 inputVector = Vector2.zero;
    
    
    private float pickupRange = 1f;
    private bool isObjectInHand = false;
    private GameObject objectInHand = null;

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
        
        input.Player.Throw.performed += OnThrowPerformed;
        input.Player.Interact.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= OnMovementPerformed;
        input.Player.Move.canceled -= OnMovementCancelled;

        input.Player.Jump.performed -= OnJumpPerformed;

        input.Player.Throw.performed -= OnThrowPerformed;
        input.Player.Interact.performed -= OnInteractPerformed;
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
            if (hit.collider.CompareTag("Object"))
            {
                GameManager.Instance.GetUIManager().UpdateObjectInfo(hit.collider.gameObject.GetComponent<Object>().GetObjectData());
            }
        }
        else
        {
            GameManager.Instance.GetUIManager().DisableObjectInfo();
        }
    }

    private void OnThrowPerformed(InputAction.CallbackContext value)
    {
        isObjectInHand = false;
        objectInHand.transform.SetParent(null);
        
        objectInHand.AddComponent<Rigidbody>();
        objectInHand.GetComponent<Rigidbody>().useGravity = true;
        objectInHand.GetComponent<Rigidbody>().linearDamping = 0.1f;
        objectInHand.GetComponent<Rigidbody>().AddForce(cameraObject.transform.forward * 10f, ForceMode.Impulse);

        Collider col = objectInHand.GetComponent<Collider>();
        if (col == null) col = objectInHand.AddComponent<BoxCollider>();
        else col.enabled = true;

        if (objectInHand.GetComponent<Thrown>() == null)
            objectInHand.AddComponent<Thrown>();

        objectInHand = null;
    }

    private void OnInteractPerformed(InputAction.CallbackContext value)
    {
        Ray ray = cameraObject.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
       
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            if (hit.collider.CompareTag("Object") && !isObjectInHand)
            {
                objectInHand = hit.collider.gameObject;
                objectInHand.transform.SetParent(hand.transform);
                objectInHand.transform.localPosition = Vector3.zero;
                Destroy(objectInHand.GetComponent<Rigidbody>());
                objectInHand.GetComponent<Collider>().enabled = false;
                isObjectInHand = true;
            } else if (hit.collider.CompareTag("Tablet") && isObjectInHand)
            {
                hit.collider.gameObject.GetComponent<Tablet>().AddObject(objectInHand.GetComponent<Object>().GetObjectData());
                Destroy(objectInHand);
                isObjectInHand = false;
            }
        } 
    }
    
}
