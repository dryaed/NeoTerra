using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneLayerHandler : BlockLayerHandler
{
    [Range(0,1)]
    public float stoneThreshold = 0.5f;

    [SerializeField] private NoiseSettings stoneNoiseSettings;
    
    public DomainWarping domainWarping;

    protected override bool TryHandling(ChunkData chunkData, Vector3Int blockPos, int surfaceHeightNoise, Vector2Int mapSeedOffset)
    {
        if (chunkData.worldPosition.y > surfaceHeightNoise) return false;

        stoneNoiseSettings.worldOffset = mapSeedOffset;
        //float stoneNoise = MyNoise.OctavePerlin(chunkData.worldPosition.x + blockPos.x,
        //    chunkData.worldPosition.z + blockPos.z, stoneNoiseSettings);
        float stoneNoise = domainWarping.GenerateDomainNoise(chunkData.worldPosition.x + blockPos.x,
            chunkData.worldPosition.z + blockPos.z, stoneNoiseSettings);

        int endPosition = surfaceHeightNoise;
        if (chunkData.worldPosition.y < 0)
        {
            endPosition = chunkData.worldPosition.y + chunkData.chunkHeight;
        }

        if (stoneNoise > stoneThreshold)
        {
            for (var i = chunkData.worldPosition.y; i <= endPosition; i++)
            {
                Vector3Int tempPos = new Vector3Int(blockPos.x, i, blockPos.z);
                Chunk.SetBlock(chunkData, tempPos, BlockType.Stone);
            }

            return true;
        }

        return false;
    }
}
