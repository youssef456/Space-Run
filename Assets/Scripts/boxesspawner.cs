using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxesspawner : MonoBehaviour
{
    public GameObject[] objects;
    GameObject player;
    float nextspawn = 0.0f;
    public float spawnrate = 4f;
    float random;
    int randomobject;
    public float spawndistance = 2000f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextspawn)
        {
            nextspawn = Time.time + spawnrate;
            random = Random.Range(17.03f, -21.41f);
            randomobject = Random.Range(0, objects.Length);
            Vector3 playerx = new Vector3(0, 1f, player.transform.position.z);
            Vector3 wheretospawn = (Vector3)playerx + new Vector3(random, 0, spawndistance);
            Vector3 angle = new Vector3(0, 90, 0);
            Instantiate(objects[randomobject], wheretospawn, Quaternion.Euler(angle));
         //   Destroy(objects[randomobject], 30);

        }
    }
}
