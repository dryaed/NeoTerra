using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkData
{
    public BlockType[] blocks; // all of the blocks
    public int chunkSize = 16; // chunk size valuexvalue
    public int chunkHeight = 100; // Y
    public World worldReference; // this reference is needed so when a voxel on the edge of a chunk gets modified the bordering voxels in other chunks react
    public Vector3Int worldPosition; // global chunk position

    public bool modifiedByThePlayer = false; // used to save chunks that were modified in some way by the player

    public ChunkData(int chunkSize, int chunkHeight, World world, Vector3Int worldPosition) // constructor for a chunk
    {
        this.chunkSize = chunkSize;
        this.chunkHeight = chunkHeight;
        this.worldReference = world;
        this.worldPosition = worldPosition;
        blocks = new BlockType[chunkSize * chunkHeight * chunkSize];
    }
}
