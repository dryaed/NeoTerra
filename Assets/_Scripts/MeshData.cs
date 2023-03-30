using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData
{
    public List<Vector3> vertices = new List<Vector3>(); // Lists are better than arrays, because different chunks can have differing numbers of vertices|trinagles|uvs
    public List<int> triangles = new List<int>();
    public List<Vector2> uv = new List<Vector2>();

    public List<Vector3> colliderVertices = new List<Vector3>();  // a separate collider list is needed so we can pass through water
    public List<int> colliderTriangles = new List<int>(); 

    public MeshData waterMesh; // separate mesh for water
    private bool isMainMesh = true; // helper boolean for the constructor

    public MeshData(bool isMainMesh)
    {
        if (isMainMesh) // this if statement is needed so that the constructor doesn't go into a recursion loop
        {
            waterMesh = new MeshData(false);
        }
    }

    public void AddVertex(Vector3 vertex, bool vertexGeneratesCollider) // basically "corners" of voxels in the mesh
    {
        vertices.Add(vertex);
        if (vertexGeneratesCollider) // used so that water and air have no collision
        {
            colliderVertices.Add(vertex);
        }
    }

    public void AddQuadTriangles(bool quadGenerateCollider) // a quad is a square made from two triangles, a quad is basically a "face" of a voxel in the mesh
    {
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);
        
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 1);

        if (quadGenerateCollider)
        {
            colliderTriangles.Add(vertices.Count - 4);
            colliderTriangles.Add(vertices.Count - 3);
            colliderTriangles.Add(vertices.Count - 2);
        
            colliderTriangles.Add(vertices.Count - 4);
            colliderTriangles.Add(vertices.Count - 2);
            colliderTriangles.Add(vertices.Count - 1);
        }
    }
}
