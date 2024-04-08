using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PipeMoveScript : MonoBehaviour
{
    public float moveSpeed;
    public float deadZone = -45;
    public BirdScript bird;
    // Start is called before the first frame update
    void Start()
    {
        bird = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdScript>();
    }  

    // Update is called once per frame
    void Update()
    {
        if (bird.birdIsAlive == true)
        {
            transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
            if (transform.position.x < deadZone)
            {
                Debug.Log("Object destoyed!");
                Destroy(gameObject);
            }
        }
    }
}
