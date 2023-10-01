using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerammove : MonoBehaviour
{
    private Transform player;
    PlayerController playerController;
    private Vector3 startoffset;
    private Vector3 movevector;
    private Vector3 animationoffset = new Vector3(0, 5, 3);
    private float transition = 0f;
    private float transitioncamera = 0f;
    private float animationduration = 2f;
    public bool followhorixontal = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();
        startoffset = transform.position - player.position;

    }
    void LateUpdate()
    {
        movevector = player.position + startoffset;
        //x
        if(followhorixontal == false)
        {
            movevector.x = 0;

        }
        else
        {
            movevector.x = player.position.x;
        }
        //y
        movevector.y = player.position.y + 5f;
        if(transition > 1f)
        {
            transform.position = Vector3.Lerp(transform.position, movevector, 1f);
            Vector3 rot = new Vector3(20, 0, 0);
            Vector3 currentror  = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.localEulerAngles = Vector3.Lerp(currentror ,rot ,transitioncamera);
            transitioncamera += Time.deltaTime * 1 / animationduration;
        }
        else
        {
            //animation at start of the game
            transform.position = Vector3.Lerp(movevector + animationoffset, movevector, transition);
            transition += Time.deltaTime * 1 / animationduration;
            transform.LookAt(player.position + Vector3.up);
        }
    }
}
