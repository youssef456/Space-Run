using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class groundspawner : MonoBehaviour
    
{
    public GameObject[] bridgeprephab;
    private Transform playertransform;
    private float spawnz = -5.03f;
    public float tilelenght = 5.03f;
    public int notilesonscreen = 25;
    //for destroying bridges
    private List<GameObject> activetiles;
    public float safezone = 15f;
    //for generating randombridges
    private int lastprefabe = 0;
    // Start is called before the first frame update
    void Start()
    {
        playertransform = GameObject.FindGameObjectWithTag("Player").transform;
        activetiles = new List<GameObject>();
        //spwanfirst BRidge in array list
        for(int i =0;i < notilesonscreen; i++)
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

    // Update is called once per frame
    void Update()
    {
        if(playertransform.position.z - safezone > (spawnz- notilesonscreen * tilelenght))
        {
          
            spawntile();
            deletebridge();

        }
    }
    private void spawntile(int prephabindex = -1)
    {
        GameObject go;
        if (prephabindex == -1)
        {
            go = Instantiate(bridgeprephab[Randomprefab()]) as GameObject;
        }
        else
        {
            go = Instantiate(bridgeprephab[prephabindex]) as GameObject;

        }
        Vector3 pos = new Vector3(transform.position.x,transform.position.y - 5f,spawnz);
        go.transform.SetParent(transform);
        //   go.transform.position = Vector3.forward * spawnz;
        go.transform.position = pos;
        //vector3.forward meeans (0,0,1)
        spawnz += tilelenght;
        activetiles.Add(go);
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
        int randomindex = lastprefabe;
        //mae sure it isnt the same last bridge gnerated
        while(randomindex == lastprefabe)
        {
            randomindex = Random.Range(0, bridgeprephab.Length);
        }
        lastprefabe = randomindex;
        return randomindex;
    }
}
