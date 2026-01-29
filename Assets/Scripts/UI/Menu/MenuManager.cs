using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("Special Panels")]
    public GameObject deckPanel;
    public GameObject pausePanel;
    public GameObject losePanel;
    public GameObject winPanel;

    //For Reopen Deck Panel With return from game play scene to lobby
    public static bool isOpenDeckPanel = false;

    //Stack Of Opened Menus(Panels)
    public Stack<GameObject> menuStack = new Stack<GameObject>();
    private void Awake()
    {
 
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //Reopen Deck Panel With return from game play scene to lobby
        if (SceneManager.GetActiveScene().buildIndex == 0 && isOpenDeckPanel)
        {
            OpenMenu(deckPanel);
            isOpenDeckPanel = false;
        }
    }

    //Open Menu/Panel
    public void OpenMenu(GameObject menu)
    {
        if (menu != null && !menu.activeInHierarchy)
        {
            menu.SetActive(true);
            menuStack.Push(menu);
        }
    }

    //Close Last Menu/Panel
    public void CloseLastMenu()
    {
        if (menuStack.Count > 0)
        {
            GameObject lastMenu = menuStack.Pop();
            lastMenu.SetActive(false);
        }
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//ESC Key Pause|Close
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (!losePanel.activeInHierarchy && !winPanel.activeInHierarchy && menuStack.Count == 0)
                {
                    PauseGame();
                }
                else
                {
                    return;
                }
            }
            else
            {
                CloseLastMenu();
            }
        }

    }

    //Load Scene with index
    public void LoadScene(int sceneId)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneId);
        if(sceneId == 1)
            isOpenDeckPanel = true;
    }

    //Pause Game
    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    //Resume Game
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    //Exit from game
    public void ExitGame()
    {
        Application.Quit();
    }
}
