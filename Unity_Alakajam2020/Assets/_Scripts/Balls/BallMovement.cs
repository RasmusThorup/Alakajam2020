using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Transform myTransform;
    public float speed;
    [Tooltip("X & Y = min og max værdierne på x-aksen, Z & W = min og max værdierne på y-aksen")]
    public Vector4 gameAreaOuterEdges = new Vector4(0, 40, 0, 25);
    public Vector2 currentPos;
    public Vector2 currentDir;
    public float ballRadius = 0.5f;

    private Rigidbody rb;


    // Start is called before the first frame update
    void Awake()
    {
        myTransform = transform;
        rb = GetComponent<Rigidbody>();

        //init();
        //ChangeDirection(myTransform.up);
    }

    void OnEnable()
    {
        init();
        currentPos = myTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = myTransform.position;

        if (currentPos.y <= gameAreaOuterEdges.z)
        {
            //Debug.Log("Ball Outside y lower edge");
            myTransform.position = new Vector3(myTransform.position.x,gameAreaOuterEdges.z,0);
            currentDir = Reflect(currentDir, Vector2.up);

        }
        else if (currentPos.x <= gameAreaOuterEdges.x)
        {
            //Debug.Log("Ball Outside x left edge");
            myTransform.position = new Vector3(gameAreaOuterEdges.x,myTransform.position.y,0);
            currentDir = Reflect(currentDir, Vector2.right);

        }
        else if (currentPos.y >= gameAreaOuterEdges.w)
        {
            //Debug.Log("Ball Outside y upper edge");
            myTransform.position = new Vector3(myTransform.position.x,gameAreaOuterEdges.w,0);
            currentDir = Reflect(currentDir, -Vector2.up);
            
        }
        else if (currentPos.x >= gameAreaOuterEdges.y)
        {
            //Debug.Log("Ball Outside x right edge");
            myTransform.position = new Vector3(gameAreaOuterEdges.y,myTransform.position.y,0);
            currentDir = Reflect(currentDir, -Vector2.right);           
        }
        else
        {
            //Ball is inside playable area
        }

        Vector3 dir = new Vector3 (currentDir.x, currentDir.y,0);
        myTransform.position += dir * speed * Time.deltaTime;
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
