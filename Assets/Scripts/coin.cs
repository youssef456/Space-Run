using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class coin : MonoBehaviour
{
    public float revolveSpeed;
    void Update()
    {
        //rotate about y axis
        transform.Rotate(Vector3.up * revolveSpeed *  Time.deltaTime);

    }
}
