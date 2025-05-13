using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
	public void OnCloseButtonClicked()
	{
		#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
		#else
				Application.Quit();
		#endif
    }

    public void OnPlayButtonClicked()
	{
		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}
}
