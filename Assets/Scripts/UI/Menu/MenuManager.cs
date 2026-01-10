using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    //public static float isOpenLevelsScreen = -1;
    //public GameObject levelsScreen;
    //public Scrollbar levelsScreenScrollbar;
    public GameObject exitScreen;

    //public  List<GameObject> openMenus = new List<GameObject>();
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
        /*if (SceneManager.GetActiveScene().buildIndex == 0 &&  isOpenLevelsScreen  != -1)
        {
            OpenMenu(levelsScreen);
            LoadLevelsInUI.instance.StartTimer();
            levelsScreenScrollbar.value = isOpenLevelsScreen;
            isOpenLevelsScreen = -1;
        }*/
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
                OpenMenu(exitScreen);
            }
        }
    }

    public GameObject pauseScreen;
    public GameObject startGameScreen;
    public GameObject gameoverScreen;
    public GameObject winScreen;
    public GameObject rewardScreen;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1 && !pauseScreen.activeInHierarchy && !startGameScreen.activeInHierarchy && menuStack.Count == 0)
            {
                if (!gameoverScreen.activeInHierarchy && !winScreen.activeInHierarchy)
                {
                    OpenMenu(pauseScreen);
                }
            }
            else
            {
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    if (gameoverScreen.activeInHierarchy || winScreen.activeInHierarchy)
                        return;
                    if(rewardScreen.activeInHierarchy)
                    {
                        CloseLastMenu();
                        OpenMenu(winScreen);
                        return;
                    }

                }
                CloseLastMenu();
            }
        }

    }

    public void LoadScene(int sceneId)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneId);
    }
    public void LoadSceneWithSkipLevelScreen(int sceneId)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneId);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
