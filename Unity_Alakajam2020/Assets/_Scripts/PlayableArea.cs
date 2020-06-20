﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayableArea : MonoBehaviour
{
    public Vector4 gameAreaEdges;
    public MeshFilter meshFilter;
    public Vector2 gameAreaSize;
    float oldAspect;
    private Camera mainCam;
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public Vector2 offset = new Vector2(0,3);
    public Vector2 padding = new Vector2(10,15);

    void Start()
    {
        mesh = new Mesh();
        meshFilter.mesh = mesh;

        triangles = new int[]
        {
            0,1,2,
            1,3,2
        };

        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        if (oldAspect == mainCam.aspect) //Early out if aspect hasn't changed
            return;

        Debug.Log("Update");

        float height = 2.0f * Mathf.Tan(0.5f * mainCam.fieldOfView * Mathf.Deg2Rad)* 40.0f;
        float width = height * mainCam.aspect;

        gameAreaSize = new Vector2(width-padding.x,height-padding.y);
        
        float x = gameAreaSize.x*0.5f;
        float y = gameAreaSize.y*0.5f;
        gameAreaEdges = new Vector4(-x+offset.x,x+offset.x,-y+offset.y,y+offset.y);
        
        vertices = new Vector3[]
        {
            new Vector3 (gameAreaEdges.x,gameAreaEdges.z,0),
            new Vector3 (gameAreaEdges.x,gameAreaEdges.w,0),
            new Vector3 (gameAreaEdges.y,gameAreaEdges.z,0),
            new Vector3 (gameAreaEdges.y,gameAreaEdges.w,0)
        };

        UpdateMesh();
        oldAspect = mainCam.aspect;
        GameManager.Instance.gameAreaEdges = gameAreaEdges;
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
