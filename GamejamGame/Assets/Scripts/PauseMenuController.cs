using UnityEngine;

public class PauseMenuController : MonoBehaviour
{

    public GameObject pauseMenu;
    private PlayerInput playerInput;
    public PlayerInput.WalkActions walk;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerInput = new PlayerInput();
        walk = playerInput.Walk;
        walk.Pause.performed += ctx => pauseGame();
    }

    void Start()
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        walk.Enable();
    }

    private void OnDisable()
    {
        walk.Disable();
    }

    public void pauseGame()
    {
        Cursor.visible = !Cursor.visible;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        Cursor.visible = !Cursor.visible;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
