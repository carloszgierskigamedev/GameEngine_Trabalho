using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    //private Animator animator;
    [SerializeField] private float forwardMoveSpeed = 7.5f;
    [SerializeField] private float backwardMoveSpeed = 3.5f;
    [SerializeField] private float turnSpeed = 70f;
    [SerializeField] private AudioClip[] footstepsAudio;
    private AudioSource audioSource;
    private Vector3 lastPosition;
    private float totalMoved;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;

        //animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalCamera = Input.GetAxis("Mouse X");
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        var verticalMovement = Input.GetAxisRaw("Vertical");
        var movement = new Vector3(horizontalMovement, 0, verticalMovement);
        var movementNormalized = Vector3.Normalize(movement);
        //characterController.SimpleMove(movementNormalized * Time.deltaTime * moveSpeed);

        //animator.SetFloat("Speed", vertical);

        transform.Rotate(Vector3.up, horizontalCamera * turnSpeed * Time.deltaTime);

        if(verticalMovement != 0 || horizontalMovement != 0)
        {
            float moveSpeedToUse = (verticalMovement > 0) ? forwardMoveSpeed : backwardMoveSpeed;
            characterController.SimpleMove((transform.forward * moveSpeedToUse * movementNormalized.z) + (transform.right * backwardMoveSpeed * movementNormalized.x));
        }

        if(characterController.isGrounded && characterController.velocity.magnitude > 2f)
        {
            float moveFromLastPosition = (lastPosition - transform.position).magnitude;
            lastPosition = transform.position;
            totalMoved += moveFromLastPosition;

            if (totalMoved >= 6f)
            {
                AudioClip clip = GetRandomAudioClip();
                audioSource.PlayOneShot(clip, 0.3f);
                totalMoved = 0f;
            }
        }

    }

    private AudioClip GetRandomAudioClip()
    {
        return footstepsAudio[UnityEngine.Random.Range(0,footstepsAudio.Length)];
    }
}
