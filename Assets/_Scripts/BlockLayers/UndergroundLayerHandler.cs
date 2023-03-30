using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundLayerHandler : BlockLayerHandler
{
    public BlockType undergroundBlockType;
    protected override bool TryHandling(ChunkData chunkData, Vector3Int blockPos, int surfaceHeightNoise, Vector2Int mapSeedOffset)
    {
        if (blockPos.y < surfaceHeightNoise)
        {
            
            Chunk.SetBlock(chunkData, blockPos, undergroundBlockType);
            return true;
        }

        return false;
    }
}
