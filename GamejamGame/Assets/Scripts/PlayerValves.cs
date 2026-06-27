using UnityEngine;
using TMPro;

public class PlayerValves : MonoBehaviour
{

    private float totalValves = 3f;

    public TextMeshProUGUI ui;

    public float currentValves = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentValves/totalValves < 1)
            ui.text = "Valves completed: " + currentValves + "/" + totalValves;
        else
            ui.text = "Escape";
    }

    public void IncrementValves()
    {
        currentValves ++;
    }

    public bool getCompletion()
    {
        return currentValves == totalValves;
    }
}
