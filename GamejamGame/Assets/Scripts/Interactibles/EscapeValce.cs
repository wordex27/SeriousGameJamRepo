using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeValce : Interactible
{

    private GameObject player;
    private PlayerValves playerValves;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Valve");
        playerValves = player.GetComponent<PlayerValves>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        if (playerValves.getCompletion())
        {
            SceneManager.LoadSceneAsync(2);
        }
    }
}
