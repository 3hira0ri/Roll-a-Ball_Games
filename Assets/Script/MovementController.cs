using UnityEngine;
public class MovementController : MonoBehaviour
{
    [SerializeField] ParticleSystem SprintParticles;
    [SerializeField] AudioSource AudioSource;
    GameObject _playerCamera;
    Rigidbody _body;
    [SerializeField] float MoveUP;
    [SerializeField] float MoveF;
    float _timerJump;
    Vector3 _moveDirection;
    [SerializeField] float SprintMultiplier = 2.5f;
   // [SerializeField] AudioClip Died;

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
        _body.AddForce(_moveDirection);
    }
    void Update()
    {
        movement();
    }
    void movement()
    {
        float currentMoveF = MoveF;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 forward = _playerCamera.transform.forward;
        forward.y = 0; 
        forward = forward.normalized; 

        Vector3 right = _playerCamera.transform.right;
        right.y = 0; 
        right = right.normalized;
        float czas = Time.unscaledTime;
        Vector3 up = new Vector3(0, 1, 0);
        if (Input.GetKeyDown(KeyCode.Space) && czas - _timerJump > 1f)
        {
            up = up * MoveUP;
            _timerJump = Time.unscaledTime;
        }
        else
        {
            up = new Vector3();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentMoveF *= SprintMultiplier;
            HandleSprintParticles(true);
        }
        else
        {
            HandleSprintParticles(false);
        }
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            if (slopeAngle < 60f) 
            {
                _moveDirection = (forward * vertical + right * horizontal + up) * currentMoveF;
            }
            else
            {
                _moveDirection = Vector3.down*MoveF;
            }
        }
        
    }
    
    void HandleSprintParticles(bool isSpringing)
    {
        if (isSpringing)
        {
            if (!SprintParticles.isPlaying)
            {
                SprintParticles.Play();
            }
        }
        else
        {
            if (SprintParticles.isPlaying)
            {
                SprintParticles.Stop();
            }
        }
    }
}
