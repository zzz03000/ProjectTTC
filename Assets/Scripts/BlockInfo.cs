using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInfo : MonoBehaviour
{
    public enum BlockType
    {
        FireBlock,
        IceBlock,
        RockBlock,
        LeafBlock,
        WoodBlock,
        WaterBlock,
        ThunderBlock
    }

    public BlockType blockType;
}
