using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgemanager : MonoBehaviour
{
    public GameObject[] bridgeprephab;
    private float spawnz = -5.03f;
    public float tilelenght = 5.03f;
    public int notilesonscreen = 25;

    //for destroying bridges
    private List<GameObject> activetiles;
    public float safezone = 15f;

    //for generating randombridges
    private int lastprefab = 0;
    public static bool movenow = true;
    public float movespeed;

    GameObject currentbridge;


    void Start()
    {
        movenow = true;
        activetiles = new List<GameObject>();
        //spawnfirst Bridge in array list
        for(int i = 0 ; i < notilesonscreen ; i++)
        {
            if (i < 8)
            {
                spawntile(0);

            }
            else
            {
                spawntile();
            }
        }
        
    }

    void Update()
    {
        if(currentbridge.transform.position.z < safezone)
        {
          
            spawntile();
            deletebridge();

        }
        if (movenow)
        {
            moveparent();
        }
    }
    void moveparent()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - movespeed * Time.deltaTime);
    }
    private void spawntile(int prephabindex = -1)
    {
        if (prephabindex == -1)
        {
            currentbridge = Instantiate(bridgeprephab[Randomprefab()]);
        }
        else
        {
            currentbridge = Instantiate(bridgeprephab[prephabindex]);

        }
        Vector3 pos = new Vector3(transform.position.x,transform.position.y,spawnz);
        currentbridge.transform.SetParent(transform);
        currentbridge.transform.localPosition = pos;
        spawnz += tilelenght;
        activetiles.Add(currentbridge);
    }
    private void deletebridge()
    {
        Destroy(activetiles[0]);
        activetiles.RemoveAt(0);
    }
    private int Randomprefab()
    {
        if(bridgeprephab.Length <= 1)
        {
            return 0;
        }
        int randomindex = lastprefab;
        //mae sure it isnt the same last bridge gnerated
        while(randomindex == lastprefab)
        {
            randomindex = Random.Range(0, bridgeprephab.Length);
        }
        lastprefab = randomindex;
        return randomindex;
    }
}
