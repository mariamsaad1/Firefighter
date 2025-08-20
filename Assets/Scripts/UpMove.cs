using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpMove : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
// public float moveSpeed = 10f;

    public float moveSpeed = 1.0f;
    public float height = 5.0f;
    private float initialY;

    // void Update()
    // {
    //     // transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    //             // تحريك الكائن لأسفل على محور الـY
    //     transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    // }


     void Start()
    {
        initialY = transform.position.y;
    }

    void Update()
    {
        float newY = Mathf.PingPong(Time.time * moveSpeed, height * 2) - height + initialY;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
