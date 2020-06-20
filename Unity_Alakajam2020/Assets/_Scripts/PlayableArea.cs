using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableArea : MonoBehaviour
{
    public Canvas canvas;
    public MeshFilter meshFilter;
    public Vector2 gameAreaSize;

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public float yOffset = 1;
    public Vector2 offset = new Vector2(0,20);

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        meshFilter.mesh = mesh;

        triangles = new int[]
        {
            0,1,2,
            1,3,2
        };
        
        //CreateShape();
        //UpdateMesh();
    }


    void Update()
    {
        //if (canvas.pixelRect.size == gameAreaSize) //Early out if canvas hasn't changed
        //    return;

        gameAreaSize = canvas.pixelRect.size;
        
        float x = gameAreaSize.x*0.5f*0.1f;
        float y = gameAreaSize.y*0.1f*0.1f;
        float yTop = y*4.5f;
        float yBot = -y*5.5f;
        //float y = gameAreaSize.y*0.5f;
        
        vertices = new Vector3[]
        {
            new Vector3 (-x,yBot,0),
            new Vector3 (-x,yTop,0),
            new Vector3 (x,yBot,0),
            new Vector3 (x,yTop,0)
        };

        UpdateMesh();
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
