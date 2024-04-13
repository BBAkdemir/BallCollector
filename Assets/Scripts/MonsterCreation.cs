using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterCreation : MonoBehaviour
{
    public List<GameObject> Balls;
    public GameObject MonsterSpawnPoint;
    public float BallSpeed;
    int BallCount;
    int Health;
    void Start()
    {
        Balls = new List<GameObject>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (Balls.Any(x => x == other.gameObject) == false)
        {
            BallCount++;
            Health++;
            other.GetComponent<Rigidbody>().useGravity = false;
            Balls.Add(other.gameObject);
        }
    }
    void Update()
    {
        if (Balls != null && BallCount > 0)
        {
            foreach (var item in Balls)
            {
                if (item != null)
                {
                    item.transform.position = Vector3.MoveTowards(item.transform.position, MonsterSpawnPoint.transform.position, BallSpeed * Time.deltaTime);
                    if (item.transform.position == MonsterSpawnPoint.transform.position)
                    {
                        Destroy(item);
                        BallCount--;
                    }
                }
            }
        }
        if (BallCount == 0)
        {

        }
    }
}
