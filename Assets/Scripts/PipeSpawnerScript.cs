using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PipeSpawnerScript : MonoBehaviour
{
    private GameObject pipe;
    public GameObject pipe1;
    public GameObject pipe2;
    public GameObject pipe3;
    public float spawnRate=2;
    private float timer=0;
    public float heightOffset = 10;
    public BirdScript bird;
    //private Random random = new Random();

    

    // Start is called before the first frame update
    void Start()
    {
        spawnPipe();
        bird = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bird.birdIsAlive==true)
        {

        
         if (timer < spawnRate) {
            timer = timer + Time.deltaTime;
         }
         else
         {
            spawnPipe();
            timer = 0;
         }
        }
    }
    void spawnPipe()
    {
        pipeSelector();
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Instantiate(pipe, new Vector3(transform.position.x,Random.Range(lowestPoint,highestPoint),0), transform.rotation);
    }

    void pipeSelector()
    {
        int randomNumber = UnityEngine.Random.Range(0, 3);
        int[] tuple = { 1, 2, 3 };
        switch (tuple[randomNumber])
        {
            case 1:
                pipe=pipe1;
                break;
            case 2:
                pipe = pipe2;
                break;
            case 3:
                pipe = pipe3;
                break;
            default:
                pipe = pipe1;
                break;
        }
    }
}
