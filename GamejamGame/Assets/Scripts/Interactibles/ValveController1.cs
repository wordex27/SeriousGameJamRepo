using UnityEngine;

public class ValveController : Interactible
{

    public bool valveCompleted = false;

    private PlayerValves playerValve;
    private GameObject player;

    private bool DoItOnce = true;
    private float timesInteract = 0f;
    private float rotateSpeed = 10f;
    protected override void Interact()
    {
        if (timesInteract < 10){
        transform.rotation *= Quaternion.Euler(0, rotateSpeed, 0);
        timesInteract ++;
        }
    }

    void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Valve");
        if (player != null)
            playerValve = player.GetComponent<PlayerValves>();

        if (valveCompleted && DoItOnce)
        {
            playerValve.IncrementValves();
            DoItOnce = false;
        }

        if (timesInteract >= 10)
        {
            timesInteract = 10;
            valveCompleted = true;
        }
    }

}
