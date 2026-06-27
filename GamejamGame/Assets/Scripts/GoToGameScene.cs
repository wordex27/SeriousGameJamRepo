using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToGameScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void GoToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}


