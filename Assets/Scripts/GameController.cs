using System.Linq.Expressions;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool spawnCoins = false;
    public GameObject coin;
    public float Coinspawndistance = 1500f;
    float NextSpwanPos;
    GameObject go;

    public bool spawnCars = false;
    public GameObject[] car;
    public float Carspawnrate = 4f;
    float Carnextspawn = 0.0f;
    public float carspawndistance = 1500f;

    public GameObject player;
    float playerXMovment = 12;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerXMovment = player.GetComponent<PlayerController>().Xmovment;
        spawncoin();

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCars)
        {
            spawncar();
        }
        if (go.transform.position.z < Coinspawndistance && spawnCoins)
        {
            spawncoin();
        }
    }

    void spawncar()
    {
        int randcar = Random.Range(0, car.Length);
        if (Time.time > Carnextspawn)
        {
            Carnextspawn = Time.time + Carspawnrate;
            float random = Random.Range(-3f, 3f);
            // wheretospawn = new Vector2(random, transform.position.y);
            //   wheretospawn = new Vector2(player.position + m_NewPosition, player.position  + m_NewPosition);
            Vector3 playerx = new Vector3(0, 0, player.transform.position.z);
            Vector3 wheretospawn = (Vector3)playerx + new Vector3(random, 0, carspawndistance);
            Debug.Log("enemy" + car);
            Vector3 angle = new Vector3(0, 90, 0);
            Instantiate(car[randcar], wheretospawn, Quaternion.Euler(angle));

        }
    }
    void spawncoin()
    {
        int noumberofcoins = 8;//Random.Range(4, 10);
        int random = Mathf.FloorToInt(Random.Range(-1f, 2f));
        float preposition = 0;
        Vector3 wheretospawn = Vector3.zero;
        for (int i = 0; i <= noumberofcoins; i++)
        {
            wheretospawn = new Vector3(random * playerXMovment, 0.8f, Coinspawndistance + preposition + NextSpwanPos);
            go =  Instantiate(coin, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(transform);
            go.transform.localPosition = wheretospawn;
         
            preposition += 2;
        }
        //setting the new Coins pos ( the larger the Fixed Number is the closer the Lines are to each other )
        NextSpwanPos = wheretospawn.z - 72;
    }
}
