using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private Button m_Resume = null;
    [SerializeField]
    private Button m_Restart = null;
    [SerializeField]
    private Button m_Quit = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }

    private void OnEnable()
    {
        if (m_Resume != null)
        {
            m_Resume.onClick.AddListener(Resume);
        }
        if (m_Restart != null)
        {
            m_Restart.onClick.AddListener(Restart);
        }
        if (m_Quit != null)
        {
            m_Quit.onClick.AddListener(Quit);
        }
    }

    private void OnDisable()
    {
        if (m_Resume != null)
        {
            m_Resume.onClick.RemoveListener(Resume);
        }
        if (m_Restart != null)
        {
            m_Restart.onClick.RemoveListener(Restart);
        }
        if (m_Quit != null)
        {
            m_Quit.onClick.RemoveListener(Quit);
        }
    }

    private void Resume()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }

    private void Restart()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
        Time.timeScale = 1f;
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
             Application.Quit();
#endif
    }
}
