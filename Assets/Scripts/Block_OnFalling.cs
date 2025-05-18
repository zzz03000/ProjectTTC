using UnityEngine;

public class Block_OnFalling : MonoBehaviour
{
    [SerializeField] private float m_FallingSpeed = 1.5f;
    [SerializeField] private float m_Acceleration = 2f;
    [SerializeField] private float m_MovementSpeed = 10f;
    [SerializeField] private int m_StopFramesStep = 1;

    [SerializeField] private GameObject m_GameManager = null;
    [SerializeField] private GameObject block7Prefab = null; // Prefabs 안의 Block7 연결

    private float m_ActualFallingSpeed = 0f;
    private int countStopFramesStep = 0;
    private bool m_Collided = false;
    private bool repeatGeneration = true;

    private BlockInfo blockInfo;
    private bool hasProcessedInteraction = false;
    void Awake()
    {
        Rigidbody2D myRB2D = GetComponent<Rigidbody2D>();
        myRB2D.gravityScale = 0f;
        m_GameManager = GameObject.Find("GameManager");

        blockInfo = GetComponent<BlockInfo>();
        if (blockInfo == null)
        {
            Debug.LogWarning("BlockInfo 컴포넌트가 없습니다.");
        }
    }

    void Update()
    {
        if (gameObject.tag == "P1_Block")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Rotate(0f, 0f, 90f);
            }
        }
        else if (gameObject.tag == "P2_Block")
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                transform.Rotate(0f, 0f, 90f);
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 position = transform.position;
        position += m_ActualFallingSpeed * Vector3.down * Time.fixedDeltaTime;
        transform.position = position;

        m_ActualFallingSpeed = m_FallingSpeed;

        if (gameObject.tag == "P1_Block")
        {
            HandleMovement(KeyCode.A, KeyCode.D, KeyCode.S);
        }
        else if (gameObject.tag == "P2_Block")
        {
            HandleMovement(KeyCode.J, KeyCode.L, KeyCode.K);
        }
    }

    private void HandleMovement(KeyCode left, KeyCode right, KeyCode down)
    {
        Vector3 position = transform.position;

        if (Input.GetKey(left))
        {
            if (countStopFramesStep > m_StopFramesStep)
            {
                position.x -= m_MovementSpeed * Time.deltaTime;
                transform.position = position;
                countStopFramesStep = 0;
            }
            else countStopFramesStep++;
        }

        if (Input.GetKey(right))
        {
            if (countStopFramesStep > m_StopFramesStep)
            {
                position.x += m_MovementSpeed * Time.deltaTime;
                transform.position = position;
                countStopFramesStep = 0;
            }
            else countStopFramesStep++;
        }

        if (Input.GetKey(down))
        {
            m_ActualFallingSpeed *= m_Acceleration;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (m_Collided || blockInfo == null)
            return;

        m_Collided = true;

        bool processed = false;

        switch (blockInfo.blockType)
        {
            case BlockInfo.BlockType.FireBlock:
            case BlockInfo.BlockType.IceBlock:
                BlockInfo otherInfo = other.gameObject.GetComponent<BlockInfo>();
                if (otherInfo != null && ((blockInfo.blockType == BlockInfo.BlockType.FireBlock && otherInfo.blockType == BlockInfo.BlockType.IceBlock) || (blockInfo.blockType == BlockInfo.BlockType.IceBlock && otherInfo.blockType == BlockInfo.BlockType.FireBlock)))
                {
                    if (block7Prefab != null)
                    {
                        string newTag = gameObject.tag == "P1_Block" ? "P1_Block" : "P2_Block";
                        GameObject newBlock7 = Instantiate(block7Prefab, transform.position, Quaternion.identity);
                        newBlock7.tag = newTag;
                        repeatGeneration = false;
                        this.enabled = false;
                    }
                    Destroy(gameObject);
                    Destroy(other.gameObject);
                    processed = true;
                }
                break;

            case BlockInfo.BlockType.LeafBlock:
            case BlockInfo.BlockType.WoodBlock:
                BlockInfo otherInfo1 = other.gameObject.GetComponent<BlockInfo>();
                if (otherInfo1 != null &&
                    ((blockInfo.blockType == BlockInfo.BlockType.LeafBlock && otherInfo1.blockType == BlockInfo.BlockType.WoodBlock) ||
                     (blockInfo.blockType == BlockInfo.BlockType.WoodBlock && otherInfo1.blockType == BlockInfo.BlockType.LeafBlock)))
                {
                    if (block7Prefab != null)
                    {
                        string newTag = gameObject.tag == "P1_Block" ? "P1_Block" : "P2_Block";
                        Vector3 spawnPosition = (blockInfo.blockType == BlockInfo.BlockType.WoodBlock) ? transform.position : other.transform.position;
                        GameObject newBlock7 = Instantiate(block7Prefab, spawnPosition, Quaternion.identity);
                        newBlock7.tag = newTag;
                        repeatGeneration = false;
                        this.enabled = false;
                    }

                    // 나무 블록만 파괴
                    if (blockInfo.blockType == BlockInfo.BlockType.WoodBlock)
                    {
                        Destroy(gameObject);
                    }
                    else if (otherInfo1.blockType == BlockInfo.BlockType.WoodBlock)
                    {
                        Destroy(other.gameObject);
                    }

                    Rigidbody2D rb = (blockInfo.blockType == BlockInfo.BlockType.LeafBlock) ? GetComponent<Rigidbody2D>() : other.gameObject.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.WakeUp();
                        rb.velocity = Vector2.zero;
                        rb.AddForce(Vector2.down * 0.5f, ForceMode2D.Impulse);
                    }

                    processed = true;
                }
                break;


            case BlockInfo.BlockType.RockBlock:
            case BlockInfo.BlockType.WaterBlock:
                BlockInfo otherInfo2 = other.gameObject.GetComponent<BlockInfo>();
                if (otherInfo2 != null && ((blockInfo.blockType == BlockInfo.BlockType.RockBlock && otherInfo2.blockType == BlockInfo.BlockType.WaterBlock) || (blockInfo.blockType == BlockInfo.BlockType.WaterBlock && otherInfo2.blockType == BlockInfo.BlockType.RockBlock)))
                {
                    if (blockInfo.blockType == BlockInfo.BlockType.RockBlock)
                    {
                        Destroy(gameObject);
                    }
                    else if (otherInfo2.blockType == BlockInfo.BlockType.RockBlock)
                    {
                        Destroy(other.gameObject);
                    }
                    processed = true;
                }
                break;
        }

        if ((other.gameObject.CompareTag("P1_Block") || other.gameObject.CompareTag("P2_Block") ||
             other.gameObject.CompareTag("P1_Base") || other.gameObject.CompareTag("P2_Base") ||
             other.gameObject.CompareTag("P1_Limite") || other.gameObject.CompareTag("P2_Limite")) && repeatGeneration)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1f;

            if (m_GameManager != null)
            {
                Blocks_Generator generator = m_GameManager.GetComponent<Blocks_Generator>();
                generator.GenerateBlock(gameObject.tag);
                repeatGeneration = false;
            }

            this.enabled = false;
        }

        if (processed && repeatGeneration)
        {
            if (m_GameManager != null)
            {
                Blocks_Generator generator = m_GameManager.GetComponent<Blocks_Generator>();
                generator.GenerateBlock(gameObject.tag);
                repeatGeneration = false;
            }

            this.enabled = false;
        }
    }
}
