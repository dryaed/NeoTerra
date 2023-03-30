using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirLayerHandler : BlockLayerHandler
{
    protected override bool TryHandling(ChunkData chunkData, Vector3Int blockPos, int surfaceHeightNoise, Vector2Int mapSeedOffset)
    {
        if (blockPos.y > surfaceHeightNoise)
        {
            Chunk.SetBlock(chunkData, blockPos, BlockType.Air);
            return true;
        }

        return false;
    }
}
