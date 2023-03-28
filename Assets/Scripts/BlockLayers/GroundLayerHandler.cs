using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundLayerHandler : BlockLayerHandler
{
    public BlockType surfaceBlockType;
    protected override bool TryHandling(ChunkData chunkData, Vector3Int blockPos, int surfaceHeightNoise, Vector2Int mapSeedOffset)
    {
        if (blockPos.y == surfaceHeightNoise)
        {
            
            Chunk.SetBlock(chunkData, blockPos, surfaceBlockType);
            return true;
        }

        return false;
    }
}
