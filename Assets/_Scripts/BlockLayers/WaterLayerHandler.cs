using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLayerHandler : BlockLayerHandler
{
    public int waterLevel = 1; // maybe generalize????

    protected override bool TryHandling(ChunkData chunkData, Vector3Int blockPos, int surfaceHeightNoise,
        Vector2Int mapSeedOffset)
    {
        Vector3Int tempPos = blockPos;
        if (blockPos.y > surfaceHeightNoise && blockPos.y < waterLevel)
        {
            Chunk.SetBlock(chunkData, tempPos, BlockType.Water);
            if (blockPos.y == surfaceHeightNoise + 1)
            {
                tempPos.y = surfaceHeightNoise;
                Chunk.SetBlock(chunkData, tempPos, BlockType.Sand); // refactor sand into a separate layer later
            }
            return true;
        }
        return false;
    }

}
