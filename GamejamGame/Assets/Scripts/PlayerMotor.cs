using Unity.VisualScripting;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip foostep;
    private CharacterController controller;

    private bool isGrounded;

    public float gravity  = -9.8f;

    private Vector3 playerVelocity;

    private bool isMoving = false;
    private float jumpHeight = 0f;

    private float speed = 5f;

    private float time;
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
        time += Time.deltaTime;
        isGrounded = controller.isGrounded;
        if (isMoving)
        {
            if (time > 0.6f){
            float pitch = Random.Range(0.8f, 1.2f);
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(foostep, 0.6f);
            time = 0f;
            }
        }
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
      if (Mathf.Abs(input.x) > 0 || Mathf.Abs(input.y) > 0)
        isMoving = true;
     else
        isMoving = false;
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
