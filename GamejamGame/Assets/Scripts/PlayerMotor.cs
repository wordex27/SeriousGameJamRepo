using Unity.VisualScripting;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    private CharacterController controller;

    private bool isGrounded;

    public float gravity  = -9.8f;

    private Vector3 playerVelocity;

    private float jumpHeight = 1f;

    private float speed = 5f;

    private LockerController lockerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        lockerController = GetComponent<LockerController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void processMove(Vector2 input)
    {
      Vector3 moveDirection = Vector3.zero;
      moveDirection.x = input.x;
      moveDirection.z = input.y;
      controller.Move(transform.TransformDirection(moveDirection * speed * Time.deltaTime)); 
      if (isGrounded && playerVelocity.y < 0)
        playerVelocity.y = -2;
      playerVelocity.y += gravity * Time.deltaTime;
      controller.Move(playerVelocity * Time.deltaTime);
    }

    public void jump()
    {
        if (isGrounded)
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Locker"))
        {
            lockerController.toggleLocker();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Locker"))
        {
            lockerController.toggleLocker();
        }
    }
}
