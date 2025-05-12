using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
	[SerializeField]
	private Button m_CloseButton = null;
	[SerializeField]
	private Button m_PlayButton = null;

	private void OnEnable(){
		if (m_CloseButton != null) {
			m_CloseButton.onClick.AddListener (OnCloseButtonClicked);
		}

		if (m_PlayButton != null) {
			m_PlayButton.onClick.AddListener (OnPlayButtonClicked);
		}
	}

	private void OnDisable(){
		if (m_CloseButton != null) {
			m_CloseButton.onClick.AddListener (OnCloseButtonClicked);
		}

		if (m_PlayButton != null) {
			m_PlayButton.onClick.RemoveListener (OnPlayButtonClicked);
		}
	}
		
	private void OnCloseButtonClicked(){
		Application.Quit();
	}

	private void OnPlayButtonClicked(){
		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}
}
