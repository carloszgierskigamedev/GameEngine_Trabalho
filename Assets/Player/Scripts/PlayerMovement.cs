using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    //private Animator animator;
    [SerializeField] private float forwardMoveSpeed = 7.5f;
    [SerializeField] private float backwardMoveSpeed = 3.5f;
    [SerializeField] private float turnSpeed = 80f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        //animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalCamera = Input.GetAxis("Mouse X");
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        var verticalMovement = Input.GetAxisRaw("Vertical");
        var movement = new Vector3(horizontalCamera, 0, verticalMovement);
        var movementNormalized = Vector3.Normalize(movement);
        //characterController.SimpleMove(movementNormalized * Time.deltaTime * moveSpeed);

        //animator.SetFloat("Speed", vertical);

        transform.Rotate(Vector3.up, horizontalCamera * turnSpeed * Time.deltaTime);

        if(verticalMovement != 0 || horizontalMovement != 0)
        {
            float moveSpeedToUse = (verticalMovement > 0) ? forwardMoveSpeed : backwardMoveSpeed;
            characterController.SimpleMove((transform.forward * moveSpeedToUse * verticalMovement) + (transform.right * horizontalMovement * forwardMoveSpeed));
        }

    }
}
