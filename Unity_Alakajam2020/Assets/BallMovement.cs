using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Transform myTransform;
    public float speed;

    private Rigidbody rb;

    private Vector3 lastVelocity;

    private RaycastHit hit;

    private Transform currentPosition;

    private Transform reflectedObject;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        rb = GetComponent<Rigidbody>();

        
        //ChangeDirection();
    }

    void ChangeDirection(Vector3 direction)
    {
        //myTransform.Rotate(0, 0, Random.Range(0, 360));

        //myTransform.Rotate(Quaternion.Inverse(myTransform.rotation));

        //myTransform.rotation = transform.rotation = Quaternion.Inverse(myTransform.rotation);

        //myTransform.Rotate(0, 0, myTransform.rotation.z - myTransform.rotation.z);

        //myTransform.Rotate(Vector3.Reflect(myTransform.position.normalized, Vector3.forward));
        Debug.Log(direction);

        //myTransform = direction;

        myTransform.rotation = Quaternion.LookRotation(direction);


    }

    // Update is called once per frame
    void Update()
    {
        myTransform.Translate(Vector3.up * (speed * Time.deltaTime));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (Physics.Raycast(transform.position, transform.up, out hit, LayerMask.NameToLayer("Wall")))
            {
                Debug.Log(hit.normal);
                Vector3 newDirection = Vector3.Reflect(transform.position, hit.normal);
                ChangeDirection(newDirection);
            }

        }

    }
}
