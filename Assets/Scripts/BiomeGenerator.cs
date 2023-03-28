using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeGenerator : MonoBehaviour
{
    //public int waterThreshold = 50;
    public NoiseSettings biomeNoiseSettings;
    
    public DomainWarping domainWarping;
    public bool useDomainWarping = true;
    
    public BlockLayerHandler startLayerHandler;
    public List<BlockLayerHandler> additionalLayerHandlers;
    public ChunkData ProcessChunkColumn(ChunkData data, int x, int z, Vector2Int mapSeedOffset)
    {

        biomeNoiseSettings.worldOffset = mapSeedOffset;
        int groundPosition = GetSurfaceHeightNoise(data.worldPosition.x + x, data.worldPosition.z + z, data.chunkHeight);
        for (int y = 0; y < data.chunkHeight; y++)
        {
            Vector3Int blockPos = new Vector3Int(x, y, z);
            startLayerHandler.Handle(data, blockPos, groundPosition, mapSeedOffset);
        }

        foreach (var layer in additionalLayerHandlers)
        {
            Vector3Int blockPos = new Vector3Int(x, data.worldPosition.y, z);
            layer.Handle(data, blockPos, groundPosition, mapSeedOffset);
        }

        return data;
    }

    private int GetSurfaceHeightNoise(int x, int z, int chunkHeight)
    {
        float terrainHeight;
        terrainHeight = useDomainWarping == false ? MyNoise.OctavePerlin(x, z, biomeNoiseSettings) : domainWarping.GenerateDomainNoise(x, z, biomeNoiseSettings);
        terrainHeight = MyNoise.Redistribution(terrainHeight, biomeNoiseSettings);
        int surfaceHeight = MyNoise.RemapValueFromPerlinToInt(terrainHeight, 0, chunkHeight);
        return surfaceHeight;
    }
}
