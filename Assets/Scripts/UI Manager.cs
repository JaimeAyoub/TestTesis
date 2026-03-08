using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    public GameObject mainMenu;
    public GameObject controlesMenu;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenu.SetActive(true);
        controlesMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToControles()
    {
        mainMenu.SetActive(false);
        controlesMenu.SetActive(true);
    }
    
    public void ChangeToMainMenu()
    {
        mainMenu.SetActive(true);
        controlesMenu.SetActive(false);
    }
    
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
