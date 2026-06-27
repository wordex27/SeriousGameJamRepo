using UnityEngine;

public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    public PlayerInput.WalkActions walk;

    private PlayerMotor playerMotor;

    private PlayerLook playerLook;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerInput = new PlayerInput();
        walk = playerInput.Walk;
        playerMotor = GetComponent<PlayerMotor>();
        walk.Jump.performed += ctx => playerMotor.jump();
        playerLook = GetComponent<PlayerLook>();
    }

    void FixedUpdate()
    {
        playerMotor.processMove(walk.Move.ReadValue<Vector2>()); 
    }

    void LateUpdate()
    {
        playerLook.processLook(walk.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        walk.Enable();
    }

    private void OnDisable()
    {
        walk.Disable();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
