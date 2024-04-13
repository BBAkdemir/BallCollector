using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    public List<GameObject> Girenler;
    void Start()
    {
        Girenler = new List<GameObject>();
    }
    private void OnTriggerExit(Collider collision)
    {
        Girenler.Add(collision.gameObject);
        collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
