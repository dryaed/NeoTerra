using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockDataManager : MonoBehaviour
{
    public static float textureOffset = 0.001f; // removing artifacts
    public static float tileSizeX, tileSizeY;
    public static Dictionary<BlockType, TextureData> blockTextureDataDictionary =
        new Dictionary<BlockType, TextureData>(); // easy access to textures

    public BlockDataSO textureData;

    private void Awake()
    {
        foreach (var item in textureData.textureDataList.Where(item => blockTextureDataDictionary.ContainsKey(item.blockType) == false)) // linq magic
        {
            blockTextureDataDictionary.Add(item.blockType, item);
        }

        tileSizeX = textureData.textureSizeX;
        tileSizeY = textureData.textureSizeY;
    }
}
