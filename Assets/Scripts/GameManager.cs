using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{

	[SerializeField]
	private Image P1_m_GameOver = null;
	[SerializeField]
	private Image P1_m_Winner = null;
    [SerializeField]
    private Image P2_m_GameOver = null;
    [SerializeField]
    private Image P2_m_Winner = null;

    [SerializeField]	
	private int P1_m_numLifes = 3;          // numero di vite del giocatore
    [SerializeField]
    private int P2_m_numLifes = 3;          // numero di vite del giocatore

    [SerializeField]
	private float m_safeTime = 2;		// tempo prima di perdere un'altra vita

	[SerializeField]
	private Health p1_m_Health = null;
	[SerializeField]
    private Health p2_m_Health = null;

    private float P1_m_timeBeforeLosingAnotherLife = 0; // usato in combinazione con m_safeTime
    private float P2_m_timeBeforeLosingAnotherLife = 0; // usato in combinazione con m_safeTime
    private bool P1_m_canYouLoseLife = true;                // usato in combinazione con m_safeTime
    private bool P2_m_canYouLoseLife = true;                // usato in combinazione con m_safeTime

    // ------------------------------------

    private bool m_GameRunning = false;
	private bool P1_m_Victory = false;

    private GameObject currentP1Block = null;
    private GameObject currentP2Block = null;

    void Start ()
	{
		m_GameRunning = true;

		if (P1_m_GameOver != null)
		{
			P1_m_GameOver.enabled = false;
		}

		if (P1_m_Winner != null)
		{
			P1_m_Winner.enabled = false;
		}

        if (P2_m_GameOver != null)
        {
            P2_m_GameOver.enabled = false;
        }

        if (P2_m_Winner != null)
        {
            P2_m_Winner.enabled = false;
        }


        if (p1_m_Health == null || p2_m_Health == null)
		{
			Debug.Log ("Inserisci il Prefab della vita nel GameObject");
			return;
		}
		Blocks_Generator generator = GameObject.Find("GameManager").GetComponent<Blocks_Generator>();
		generator.GenerateBlock("Start");
		Time.timeScale = 1f;

	}

	// Update is called once per frame
	void Update ()
	{
		if (m_GameRunning)
		{

			// se hai perso tutte le vite il gioco finisce
			if (P1_m_numLifes == 0)
			{
                P1_m_Victory = false;
				m_GameRunning = false;
				EndGame();
			}
            if (P2_m_numLifes == 0)
			{
                P1_m_Victory = true;
                m_GameRunning = false;
                EndGame();
            }

            if (P1_m_timeBeforeLosingAnotherLife <= 0)
			{
                P1_m_canYouLoseLife = true;
			}
			else
			{
				P1_m_timeBeforeLosingAnotherLife -= Time.deltaTime;
			}
            if (P2_m_timeBeforeLosingAnotherLife <= 0)
            {
                P2_m_canYouLoseLife = true;
            }
            else
            {
                P2_m_timeBeforeLosingAnotherLife -= Time.deltaTime;
            }

        }
		else
		{
			EndGame ();
		}

	}

	public bool isGameRunning()
	{
		return m_GameRunning;
	}

	public void setGameRunning(bool state)
	{
		m_GameRunning = state;
	}

	public void setVictory(bool state)
	{
        P1_m_Victory = state;
	}

	public void youLoseLife (string player)
	{
		if (P1_m_canYouLoseLife == true)
		{
			switch(player)
			{
                case "P1":
                    P1_m_timeBeforeLosingAnotherLife = m_safeTime;
                    P1_m_canYouLoseLife = false;
                    --P1_m_numLifes;

                    p1_m_Health.removeHeart();
                    break;
                case "P2":
                    P2_m_timeBeforeLosingAnotherLife = m_safeTime;
                    P2_m_canYouLoseLife = false;
                    --P2_m_numLifes;

                    p2_m_Health.removeHeart();
                    break;
            }
		}
	}

	private void EndGame()
	{
		if (P1_m_Victory)
		{
			if (P1_m_Winner != null)
			{
				P1_m_Winner.enabled = true;
			}
            if (P2_m_GameOver != null)
            {
                P2_m_GameOver.enabled = true;
            }
        }
		else
		{
			if (P1_m_GameOver != null)
			{
				P1_m_GameOver.enabled = true;
			}
            if (P2_m_Winner != null)
            {
                P2_m_Winner.enabled = true;
            }
        }
		Time.timeScale = 0f;

	}
    public void SetCurrentBlock(string tag, GameObject block)
    {
        if (tag == "P1_Block")
            currentP1Block = block;
        else if (tag == "P2_Block")
            currentP2Block = block;
    }

    public GameObject GetCurrentBlock(string tag)
    {
        return tag == "P1_Block" ? currentP1Block :
               tag == "P2_Block" ? currentP2Block : null;
    }

    public void ForceChangeCurrentBlock(string playerTag, GameObject[] blockPrefabs)
    {
        GameObject currentBlock = playerTag == "P1_Block" ? currentP1Block : currentP2Block;

        if (currentBlock == null) return;

        Vector3 position = currentBlock.transform.position;
        Quaternion rotation = currentBlock.transform.rotation;
        Destroy(currentBlock);

        int newIndex = Random.Range(0, blockPrefabs.Length);
        GameObject newBlock = Instantiate(blockPrefabs[newIndex], position, rotation);
        newBlock.tag = playerTag;

        // 비활성화 후 조금 뒤에 다시 활성화
        newBlock.GetComponent<Block_OnFalling>().enabled = false;
        StartCoroutine(EnableBlockAfterDelay(newBlock, 0.1f));

        if (playerTag == "P1_Block")
            currentP1Block = newBlock;
        else
            currentP2Block = newBlock;

        Debug.Log("이 블록이 부딪혔음: " + gameObject.name);
    }

    private IEnumerator EnableBlockAfterDelay(GameObject block, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (block != null)
            block.GetComponent<Block_OnFalling>().enabled = true;
    }
}
