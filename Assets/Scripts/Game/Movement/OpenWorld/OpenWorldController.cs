using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldController : MonoBehaviour
{
    //Cached blocks
   // [ReadOnly]
    //[Tooltip("Currently loaded and active blocks")]
    //public GameObject[] BlocksAsGameObjects;

    [ReadOnly]
    [Tooltip("Currently loaded and active blocks")]
    public OpenWorldBlock[] Blocks;

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
        if(Blocks.Length != 9 )
        {
            //initial load - create blocks and fill them up
            var blocksAsGameObjects = new GameObject[9];

            //instantiate initial array of blocks
            blocksAsGameObjects[0] = Instantiate(OpenWorldBlockPrefab, new Vector3(-BlockSize, 0, -BlockSize), Quaternion.identity, this.gameObject.transform);
            blocksAsGameObjects[1] = Instantiate(OpenWorldBlockPrefab, new Vector3(0, 0, -BlockSize), Quaternion.identity, this.gameObject.transform);
            blocksAsGameObjects[2] = Instantiate(OpenWorldBlockPrefab, new Vector3(BlockSize, 0, -BlockSize), Quaternion.identity, this.gameObject.transform);

            blocksAsGameObjects[3] = Instantiate(OpenWorldBlockPrefab, new Vector3(-BlockSize, 0, 0), Quaternion.identity, this.gameObject.transform);
            blocksAsGameObjects[4] = Instantiate(OpenWorldBlockPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.gameObject.transform);
            blocksAsGameObjects[5] = Instantiate(OpenWorldBlockPrefab, new Vector3(BlockSize, 0, 0), Quaternion.identity, this.gameObject.transform);

            blocksAsGameObjects[6] = Instantiate(OpenWorldBlockPrefab, new Vector3(-BlockSize, 0, BlockSize), Quaternion.identity, this.gameObject.transform);
            blocksAsGameObjects[7] = Instantiate(OpenWorldBlockPrefab, new Vector3(0, 0, BlockSize), Quaternion.identity, this.gameObject.transform);
            blocksAsGameObjects[8] = Instantiate(OpenWorldBlockPrefab, new Vector3(BlockSize, 0, BlockSize), Quaternion.identity, this.gameObject.transform);

            //get instances
            Blocks = new OpenWorldBlock[9];

            Blocks[0] = blocksAsGameObjects[0].GetComponent<OpenWorldBlock>();
            Blocks[1] = blocksAsGameObjects[1].GetComponent<OpenWorldBlock>();
            Blocks[2] = blocksAsGameObjects[2].GetComponent<OpenWorldBlock>();

            Blocks[3] = blocksAsGameObjects[3].GetComponent<OpenWorldBlock>();
            Blocks[4] = blocksAsGameObjects[4].GetComponent<OpenWorldBlock>();
            Blocks[5] = blocksAsGameObjects[5].GetComponent<OpenWorldBlock>();

            Blocks[6] = blocksAsGameObjects[6].GetComponent<OpenWorldBlock>();
            Blocks[7] = blocksAsGameObjects[7].GetComponent<OpenWorldBlock>();
            Blocks[8] = blocksAsGameObjects[8].GetComponent<OpenWorldBlock>();

            //correct block positions
            Blocks[0].BlockX = -1; Blocks[0].BlockZ = -1;
            Blocks[1].BlockX = 0; Blocks[1].BlockZ = -1;
            Blocks[2].BlockX = 1; Blocks[2].BlockZ = -1;

            Blocks[3].BlockX = -1; Blocks[3].BlockZ = 0;
            Blocks[4].BlockX = 0; Blocks[4].BlockZ = 0;
            Blocks[5].BlockX = 1; Blocks[5].BlockZ = 0;

            Blocks[6].BlockX = -1; Blocks[6].BlockZ = 1;
            Blocks[7].BlockX = 0; Blocks[7].BlockZ = 1;
            Blocks[8].BlockX = 1; Blocks[8].BlockZ = 1;
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

            //remember current position
            this.BlockX = BlockX;
            this.BlockZ = BlockZ;
        }
    }
}
