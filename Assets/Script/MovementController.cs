using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] AudioSource AudioSource;
    GameObject _playerCamera;
    Rigidbody _body;
    [SerializeField] float MoveUP;
    [SerializeField] float MoveF;
    float _timerJump;
    Vector3 _moveDirection;
    [SerializeField] float SprintMultiplier = 2.5f;
    [SerializeField] private float _interactionDistance = 2.0f; // Maksymalna odległość interakcji
    [SerializeField] private LayerMask _interactableLayer;      // Warstwa dla obiektów interaktywnych
    [Header("Ladder Settings")]
    [SerializeField] private float ladderClimbSpeed = 5.0f;    // Prędkość wspinaczki po drabinie
    public bool isOnLadder = false;                          // Czy gracz jest na drabinie
    [SerializeField] bool isInventory = false;
    void Awake()
    {
        _playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        _timerJump = Time.unscaledTime;
    }

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (!isOnLadder)
        {
            // Dodawaj tylko siłę odpowiadającą za ruch poziomy
            Vector3 horizontalMovement = new Vector3(_moveDirection.x, 0, _moveDirection.z);
            _body.AddForce(horizontalMovement, ForceMode.Acceleration);
        }
    }

    void Update()
    {
        if (isOnLadder)
        {
            ClimbLadder();
        }
        else
        {
            movement();
        }

        if (Input.GetKeyDown(KeyCode.E)) // Klawisz interakcji (E)
        {
            TryInteract();
        }
        if (Input.GetKeyDown(KeyCode.I) && isInventory) // Klawisz Ekwipunku (I)
        {
            if (!Cursor.visible)
            {
                
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
               
                Cursor.visible = false; 
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }

    void Rotation()
    {
        Vector3 forward = _playerCamera.transform.forward;
        forward.y = 0;
        forward = forward.normalized;
        transform.forward = forward;
    }

    void movement()
    {
        Rotation();
        float currentMoveF = MoveF;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 forward = _playerCamera.transform.forward;
        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = _playerCamera.transform.right;
        right.y = 0;
        right = right.normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentMoveF *= SprintMultiplier;
        }

        // Ustaw ruch poziomy
        _moveDirection = (forward * vertical + right * horizontal) * currentMoveF;

        // Obsługa skoku
        if (Input.GetKeyDown(KeyCode.Space) && Time.unscaledTime - _timerJump > 0.3f && IsGrounded())
        {
            _body.AddForce(Vector3.up * MoveUP, ForceMode.Impulse);
            _timerJump = Time.unscaledTime;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f, LayerMask.GetMask("Ground"));
    }

    private void TryInteract()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward); // Tworzymy raycast w kierunku przedmiotu
        Debug.DrawRay(_playerCamera.transform.position, _playerCamera.transform.forward * _interactionDistance, Color.green); // Dla debugowania
        
        // Sprawdzamy, czy przedmiot w zasięgu jest interaktywny
        if (Physics.Raycast(ray, out RaycastHit hit, _interactionDistance, _interactableLayer))
        {
            // Sprawdzamy, czy trafiliśmy w obiekt implementujący IInteractable
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(); // Wykonaj interakcję
            }
        }
    }

    // Obsługa wspinaczki po drabinie
    private void ClimbLadder()
    {
        Rotation();
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 climbDirection = new Vector3(0, verticalInput * ladderClimbSpeed, 0);

        _body.linearVelocity = climbDirection; // Przemieszczenie gracza w pionie
        _body.useGravity = false; // Wyłączenie grawitacji na drabinie

        // Zatrzymanie wspinaczki, gdy gracz puści klawisz
        if (verticalInput == 0)
        {
            _body.linearVelocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isOnLadder = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isOnLadder = true;
            _body.useGravity = false; // Wyłącz grawitację
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isOnLadder = false;
            _body.useGravity = true; // Włącz grawitację
        }
    }
}
