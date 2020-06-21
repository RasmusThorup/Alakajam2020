﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallMovement_Sphere : MonoBehaviour
{
    public Vector2 gameAreaCenter;
    public float gameAreaRadius;

    public BaseBall baseBall;
    public float sphereRadius = 0.5f;
    Transform myTransform;
    Vector4 gameAreaOuterEdges = new Vector4(0, 40, 0, 25);
    Vector2 currentPos;
    Vector2 currentDir;
    Vector2 reflectNormal;
    

    void Awake()
    {
        myTransform = transform;
    }

    void OnEnable()
    {
        init();
        currentPos = myTransform.position;
    }
    
    void Update()
    {
        currentPos = myTransform.position;

        if (Vector2.Distance(currentPos,gameAreaCenter) >= gameAreaRadius-sphereRadius)
        {
            //Ball outside gamearea, reflect
            reflectNormal = currentPos - gameAreaCenter;
            currentDir = Reflect(currentDir, reflectNormal);

            float vX = currentPos.x - gameAreaCenter.x;
            float vY = currentPos.y - gameAreaCenter.y;
            float magV = Mathf.Sqrt(vX*vX + vY*vY);
            float aX = gameAreaCenter.x + vX / magV * (gameAreaRadius-sphereRadius);
            float aY = gameAreaCenter.y + vY / magV * (gameAreaRadius-sphereRadius);

            myTransform.position = new Vector3(aX, aY, 0);
        }

        /*
        gameAreaOuterEdges = GameManager.Instance.gameAreaEdges;

        if (currentPos.y-sphereRadius <= gameAreaOuterEdges.z)
        {
            //Debug.Log("Ball Outside y lower edge");
            myTransform.position = new Vector3(myTransform.position.x,gameAreaOuterEdges.z+sphereRadius,0);
            currentDir = Reflect(currentDir, Vector2.up);

        }
        else if (currentPos.x-sphereRadius <= gameAreaOuterEdges.x)
        {
            //Debug.Log("Ball Outside x left edge");
            myTransform.position = new Vector3(gameAreaOuterEdges.x+sphereRadius,myTransform.position.y,0);
            currentDir = Reflect(currentDir, Vector2.right);

        }
        else if (currentPos.y+sphereRadius >= gameAreaOuterEdges.w)
        {
            //Debug.Log("Ball Outside y upper edge");
            myTransform.position = new Vector3(myTransform.position.x,gameAreaOuterEdges.w-sphereRadius,0);
            currentDir = Reflect(currentDir, -Vector2.up);
            
        }
        else if (currentPos.x+sphereRadius >= gameAreaOuterEdges.y)
        {
            //Debug.Log("Ball Outside x right edge");
            myTransform.position = new Vector3(gameAreaOuterEdges.y-sphereRadius,myTransform.position.y,0);
            currentDir = Reflect(currentDir, -Vector2.right);           
        }
        else
        {
            //Ball is inside playable area
        }

        */
        Vector3 dir = new Vector3 (currentDir.x, currentDir.y,0);
        //myTransform.position += dir * baseBall.m_cachedSpeed * Time.deltaTime;
        myTransform.position += dir * 15 * Time.deltaTime;
    }
    void init()
    {
        myTransform.eulerAngles = new Vector3(0,0,Random.Range(0f,360f));
        ChangeDirection(myTransform.up);
    }

    void ChangeDirection(Vector3 direction)
    {
        currentDir = direction;
        myTransform.rotation = Quaternion.LookRotation(direction);
    }

    Vector2 Reflect(Vector2 inDir, Vector2 normal)
    {
        return inDir-2*(Vector2.Dot(inDir,normal)/Vector2.Dot(normal,normal))*normal;
    }
}