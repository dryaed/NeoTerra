using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;




[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class ChunkRenderer : MonoBehaviour
{
    private MeshFilter _meshFilter; // this is used to display the mesh thru a renderer
    private MeshCollider _meshCollider; // reference to the collider which is used to do player | world collisions
    private Mesh _mesh; // the mesh

    public bool showGizmo = false; // full chunk size highlight to view when debugging

    public ChunkData ChunkData { get; private set; }

    public bool ModifiedByThePlayer
    {
        get
        {
            return ChunkData.modifiedByThePlayer;
        }
        set
        {
            ChunkData.modifiedByThePlayer = value;
        }
    }

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
        _mesh = _meshFilter.mesh;
    }

    public void InitializeChunk(ChunkData data)
    {
        this.ChunkData = data;
    }

    private void RenderMesh(MeshData meshData)
    {
        _mesh.Clear(); // start from a clean slate

        _mesh.subMeshCount = 2; // used for water, submeshes allow to use multiple materials in a mesh
        _mesh.vertices = meshData.vertices.Concat(meshData.waterMesh.vertices).ToArray(); // combines the main mesh with the water mesh, then converts it into an array
        
        _mesh.SetTriangles(meshData.triangles.ToArray(), 0); // the "ground" mesh
        _mesh.SetTriangles(meshData.waterMesh.triangles.Select(val => val +  meshData.vertices.Count).ToArray(), 1); // the "water" mesh, because the indexes start with 0 in the waterMesh, but here they are combined, we add the amount of regular mesh triangles to the index

        _mesh.uv = meshData.uv.Concat(meshData.waterMesh.uv).ToArray(); // data uv to rendered uv (ground+water)
        _mesh.RecalculateNormals(); // this corrects the lighting

        _meshCollider.sharedMesh = null;
        Mesh collisionMesh = new Mesh();
        collisionMesh.vertices = meshData.colliderVertices.ToArray();
        collisionMesh.triangles = meshData.triangles.ToArray();
        collisionMesh.RecalculateNormals(); // this is probably redundant as the collider mesh is not used for the visuals

        _meshCollider.sharedMesh = collisionMesh;
    }

    public void UpdateChunk()
    {
        //RenderMesh(Chunk.GetChunkMeshData(ChunkData));
    }

    public void UpdateChunk(MeshData data) // this is for multithreaded calculations
    {
        RenderMesh(data);
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            if (Application.isPlaying && ChunkData != null)
            {
                if (Selection.activeObject == gameObject) Gizmos.color = new Color(0, 1, 0, 0.4f);
                else Gizmos.color = new Color(1, 0, 1, 0.4f);
            
                Gizmos.DrawCube(transform.position + new Vector3(ChunkData.chunkSize / 2f, ChunkData.chunkHeight / 2f, ChunkData.chunkSize / 2f), new Vector3(ChunkData.chunkSize, ChunkData.chunkHeight, ChunkData.chunkSize));
            }
        }
    }
#endif
}
