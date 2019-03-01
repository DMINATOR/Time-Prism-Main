using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldController : MonoBehaviour
{
    //Cached blocks
    [ReadOnly]
    [Tooltip("Currently loaded and active blocks")]
    public GameObject[] Blocks;

    [ReadOnly]
    [Tooltip("Current block position of the center")]
    public long BlockX;

    [ReadOnly]
    [Tooltip("Current block position of the center")]
    public long BlockZ;

    [ReadOnly]
    [Tooltip("Half Block Size")]
    public int HalfBlockSize;

    [ReadOnly]
    [Tooltip("Current size of the block in Unity units (loaded from Settings)")]
    public int BlockSize;

    [ReadOnly]
    [Tooltip("Settings to load BLOCK_SIZE value")]
    public SettingsConstants.Name BLOCK_SIZE_SETTING_NAME = SettingsConstants.Name.BLOCK_SIZE;

    //Public instance to game controller
    public static OpenWorldController Instance = null;


    public GameObject OpenWorldBlockPrefab;

    private void Awake()
    {
        //Create instance
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        BlockSize = SettingsController.Instance.GetValue<int>(BLOCK_SIZE_SETTING_NAME);
        HalfBlockSize = BlockSize / 2;

        UpdateBlocks();
    }


    private void UpdateBlocks()
    {
        if( Blocks.Length != 9 )
        {
            //initial load
            Blocks = new GameObject[9];

            //instantiate initial array of blocks
            Blocks[0] = Instantiate(OpenWorldBlockPrefab, new Vector3(-BlockSize, 0, -BlockSize), Quaternion.identity, this.gameObject.transform);
            Blocks[1] = Instantiate(OpenWorldBlockPrefab, new Vector3(0, 0, -BlockSize), Quaternion.identity, this.gameObject.transform);
            Blocks[2] = Instantiate(OpenWorldBlockPrefab, new Vector3(BlockSize, 0, -BlockSize), Quaternion.identity, this.gameObject.transform);

            Blocks[3] = Instantiate(OpenWorldBlockPrefab, new Vector3(-BlockSize, 0, 0), Quaternion.identity, this.gameObject.transform);
            Blocks[4] = Instantiate(OpenWorldBlockPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.gameObject.transform);
            Blocks[5] = Instantiate(OpenWorldBlockPrefab, new Vector3(BlockSize, 0, 0), Quaternion.identity, this.gameObject.transform);

            Blocks[6] = Instantiate(OpenWorldBlockPrefab, new Vector3(-BlockSize, 0, BlockSize), Quaternion.identity, this.gameObject.transform);
            Blocks[7] = Instantiate(OpenWorldBlockPrefab, new Vector3(0, 0, BlockSize), Quaternion.identity, this.gameObject.transform);
            Blocks[8] = Instantiate(OpenWorldBlockPrefab, new Vector3(BlockSize, 0, BlockSize), Quaternion.identity, this.gameObject.transform);
        }
        else
        {
            //update cycle
        }
    }

    public void Reposition(long BlockX, long BlockZ )
    {
        if( (this.BlockX == BlockX ) && (this.BlockZ == BlockZ) )
        {
            //nothing changed - skip
        }
        else
        {
            //block position changed
            UpdateBlocks();
        }
    }
}
