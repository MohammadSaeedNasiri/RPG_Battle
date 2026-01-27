using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public static bool isOpenDeckPanel = false;
    public GameObject deckPanel;
    public GameObject exitPanel;

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
        if (SceneManager.GetActiveScene().buildIndex == 0 && isOpenDeckPanel)
        {
            OpenMenu(deckPanel);
            isOpenDeckPanel = false;
        }
    }

    public void OpenMenu(GameObject menu)
    {
        if (menu != null && !menu.activeInHierarchy)
        {
            menu.SetActive(true);
            menuStack.Push(menu);
        }
    }

    public void CloseLastMenu()
    {
        if (menuStack.Count > 0)
        {
            GameObject lastMenu = menuStack.Pop();
            if (lastMenu.GetComponentInChildren<UiMenuAnimation>() != null)
            {
                lastMenu.GetComponentInChildren<UiMenuAnimation>().Close();
            }
            else
            {
                lastMenu.SetActive(false);
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                OpenMenu(exitPanel);
            }
        }
    }

    public GameObject pausePanel;
    public GameObject losePanel;
    public GameObject winPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void LoadScene(int sceneId)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneId);
        if(sceneId == 1)
            isOpenDeckPanel = true;
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
