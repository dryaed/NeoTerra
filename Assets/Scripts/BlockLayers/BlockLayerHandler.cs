using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockLayerHandler : MonoBehaviour
{
    [SerializeField] private BlockLayerHandler Next;

    public bool Handle(ChunkData chunkData, Vector3Int blockPos, int surfaceHeightNoise, Vector2Int mapSeedOffset)
    {
        if (TryHandling(chunkData, blockPos, surfaceHeightNoise, mapSeedOffset)) return true;
        if (Next != null) return Next.Handle(chunkData, blockPos, surfaceHeightNoise, mapSeedOffset);
        return false;
    }

    protected abstract bool TryHandling(ChunkData chunkData, Vector3Int blockPos, int surfaceHeightNoise,
        Vector2Int mapSeedOffset);
}
