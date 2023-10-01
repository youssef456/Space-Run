using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carcontroller : MonoBehaviour
{
    GameObject player;
    public int MoveSpeed = 45;
    float MaxDist = 0.5f;
    public float MinDist = 0.5f;
    public AudioSource carsound;
    public AudioClip[] track;
    int tracknumber;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, 12);
        tracknumber = Random.Range(0, track.Length);
        carsound.clip = track[tracknumber];
        carsound.spatialBlend = 1f;
        carsound.volume = 0.6f;
        carsound.Play();
        carsound.loop = true;

    }

    void Update()
    {
        //  transform.LookAt(Player);
       
        if (Vector3.Distance(transform.position, player.transform.position) >= MinDist)
        {
           
               //  transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            transform.Translate(MoveSpeed * Time.deltaTime,0 , 0);
         
            



            if (Vector3.Distance(transform.position, player.transform.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
              
            }

        }
      
    }

}
