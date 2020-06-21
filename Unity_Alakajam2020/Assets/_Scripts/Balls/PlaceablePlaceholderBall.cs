using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlaceablePlaceholderBall : MonoBehaviour
{
    Camera cam;
    Vector2 point;
    Vector2 mousePos;

    //Alt efter type af ball can denne skifte farve.

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        if(!cam)
            cam = Camera.main;
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        transform.localPosition = cam.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y,40));


        if(Input.GetMouseButtonDown(0))
        {
            if (GameManager.Instance.gameHasStarted)
            {
                GameManager.Instance.PlacePlaceableUpgrade(transform.localPosition);
            }
            else
            {
                GameManager.Instance.PlaceFirstInfected(transform.localPosition);
            }

            gameObject.SetActive(false);
        }

    }
}
