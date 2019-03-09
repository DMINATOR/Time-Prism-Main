﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class OpenWorldController : MonoBehaviour
{
    [Tooltip("Locator")]
    public OpenWorldControllerLocator Locator;

    [ReadOnly]
    [Tooltip("Logging source")]
    public static string LOG_SOURCE = "OpenWorld";

    [ReadOnly]
    [Tooltip("Currently loaded and active blocks")]
    //These are loaded initially and re-used
    public OpenWorldBlock[] Blocks = null;

    //position in the oldes block
    private int BlocksOldestElement = 0;

    [ReadOnly]
    [Tooltip("Current block position of the center")]
    public long BlockX;

    [ReadOnly]
    [Tooltip("Current block position of the center")]
    public long BlockZ;

    [ReadOnly]
    [Tooltip("Current block the player is in")]
    public OpenWorldBlock CurrentBlock;

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

        UpdateBlocks(0,0);
    }

    private void UpdateBlock(int element, long BlockX, long BlockZ)
    {
        OpenWorldBlock openWorldBlock = null;
            
        if (Blocks[element] == null )
        {
            //doesn't exist yet, first time load, create new instance:
            var gameObject = Instantiate(OpenWorldBlockPrefab, new Vector3(-BlockSize, 0, -BlockSize), Quaternion.identity, this.Locator.BlocksGameObject.gameObject.transform);
            Blocks[element] = gameObject.GetComponent<OpenWorldBlock>();
        }
        //else, already exists

        openWorldBlock =  Blocks[element];

        //correct block positions
        openWorldBlock.BlockX = BlockX;
        openWorldBlock.BlockZ = BlockZ;

        if (BlockX > 0)
        {
            BlockX -= 1;
        }
        //else, ignore

        if (BlockZ > 0)
        {
            BlockZ -= 1;
        }
        //else, ignore

        openWorldBlock.transform.position = new Vector3(BlockSize * BlockX, 0, BlockSize * BlockZ);

        openWorldBlock.Refresh();

        CurrentBlock = openWorldBlock;
    }

    private void UpdateBlocks(long BlockX, long BlockZ)
    {
        if (Blocks.Length == 0)
        {
            //initial load - create blocks and fill them up
            Blocks = new OpenWorldBlock[9];

            UpdateBlock(0, -1, -1);
            UpdateBlock(1, 1, -1);
            UpdateBlock(2, 2, -1);

            UpdateBlock(3, -1, 1);
            UpdateBlock(4, 1, 1);
            UpdateBlock(5, 2, 1);

            UpdateBlock(6, -1, 2);
            UpdateBlock(7, 1, 2);
            UpdateBlock(8, 2, 2);

            //assign first block as the oldest one
            BlocksOldestElement = 0;
        }
        else
        {
            //find matching block
            var index = Array.FindIndex(Blocks, x => x.BlockX == BlockX && x.BlockZ == BlockZ);

            if (index == -1)
            {
                UpdateBlock(BlocksOldestElement, BlockX, BlockZ);

                //move to the next
                BlocksOldestElement = (BlocksOldestElement + 1) >= Blocks.Length ? 0 : (BlocksOldestElement + 1);
            }
            else
            {
                //already loaded, remember current block
                CurrentBlock = Blocks[index];
            }
        }
    }

    public void Reposition(long BlockX, long BlockZ)
    {
        if( (this.BlockX == BlockX ) && (this.BlockZ == BlockZ) )
        {
            //nothing changed - skip
        }
        else
        {
            //block position changed
            UpdateBlocks(BlockX, BlockZ);

            //remember current position
            this.BlockX = BlockX;
            this.BlockZ = BlockZ;

            //assign current block and load it
            OpenWorldController.Instance.CurrentBlock.GenerateIfNotLoaded();
        }
    }
}
