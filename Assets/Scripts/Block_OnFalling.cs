using UnityEngine;
using System.Collections;

public class Block_OnFalling : MonoBehaviour
{

	[SerializeField]
	private float m_FallingSpeed = 1.5f;
	private float m_ActualFallingSpeed = 0f;

	[SerializeField]
	private float m_Acceleration = 2f;

	[SerializeField]
	private GameObject m_GameManager = null;

	private bool m_Collided = false;

	[SerializeField]
	private float m_MovementSpeed = 10f;

	[SerializeField]
	private int m_StopFramesStep = 1;
	private int countStopFramesStep = 0;

	private bool repeatGeneration = true;

	void Awake()
	{
		Rigidbody2D myRB2D = GetComponent<Rigidbody2D> ();
		myRB2D.gravityScale = 0f;
		m_GameManager = GameObject.Find ("GameManager");
	}

    private void Update()
    {
        if(gameObject.tag == "P1_Block")
        {
            // 회전 (Space)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameObject.transform.Rotate(new Vector3(0f, 0f, +90f));
            }
        }
        else if (gameObject.tag == "P2_Block")
        {
            // 회전 (RightShift)
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                gameObject.transform.Rotate(new Vector3(0f, 0f, +90f));
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate ()
	{
		Vector3 position = transform.position;
		position += m_ActualFallingSpeed * Vector3.down * Time.fixedDeltaTime;
		transform.position = position;

		m_ActualFallingSpeed = m_FallingSpeed;
		position = transform.position;

        if (gameObject.tag == "P1_Block")
        {
            // 좌측 이동 (A)
            if (Input.GetKey(KeyCode.A))
            {
                if (countStopFramesStep > m_StopFramesStep)
                {
                    position.x -= m_MovementSpeed * Time.deltaTime;
                    gameObject.transform.position = position;
                    countStopFramesStep = 0;
                }
                else
                {
                    countStopFramesStep++;
                }
            }
            // 우측 이동 (D)
            if (Input.GetKey(KeyCode.D))
            {
                if (countStopFramesStep > m_StopFramesStep)
                {
                    position.x += m_MovementSpeed * Time.deltaTime;
                    gameObject.transform.position = position;
                    countStopFramesStep = 0;
                }
                else
                {
                    countStopFramesStep++;
                }
            }
            
            // 빠른 하강 (S)
            if (Input.GetKey(KeyCode.S))
            {
                m_ActualFallingSpeed *= m_Acceleration;
            }
        }
        else if (gameObject.tag == "P2_Block")
        {
            // 좌측 이동 (J)
            if (Input.GetKey(KeyCode.J))
            {
                if (countStopFramesStep > m_StopFramesStep)
                {
                    position.x -= m_MovementSpeed * Time.deltaTime;
                    gameObject.transform.position = position;
                    countStopFramesStep = 0;
                }
                else
                {
                    countStopFramesStep++;
                }
            }
            // 우측 이동 (L)
            if (Input.GetKey(KeyCode.L))
            {
                if (countStopFramesStep > m_StopFramesStep)
                {
                    position.x += m_MovementSpeed * Time.deltaTime;
                    gameObject.transform.position = position;
                    countStopFramesStep = 0;
                }
                else
                {
                    countStopFramesStep++;
                }
            }
            // 빠른 하강 (K)
            if (Input.GetKey(KeyCode.K))
            {
                m_ActualFallingSpeed *= m_Acceleration;
            }
        }
    }

	void OnCollisionEnter2D (Collision2D other)
	{
		if (m_Collided)
			return;

		m_Collided = true;

		if(other.gameObject.tag.Equals("P1_Block") || other.gameObject.tag.Equals("P2_Block") || 
			other.gameObject.tag.Equals("P1_Base") || other.gameObject.tag.Equals("P2_Base") ||
			other.gameObject.tag.Equals("P1_Limite") || other.gameObject.tag.Equals("P2_Limite"))
		{
			Rigidbody2D myRB2D = GetComponent<Rigidbody2D> ();
			myRB2D.gravityScale = 1f;

            string tag = other.gameObject.tag;
            if (m_GameManager!=null && repeatGeneration)
			{
				Blocks_Generator generator = m_GameManager.GetComponent<Blocks_Generator>();
				generator.GenerateBlock(tag);
				repeatGeneration = false;
			}
			this.enabled = false;
		}
	}
}
