using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBar : MonoBehaviour
{
    public GameObject Character;
    public GameObject canBar;
    float can;
    float fullCan;

    void Start()
    {
        fullCan = Character.GetComponent<Character>().health;
    }

    // Update is called once per frame/*bberkakdemir*/
    void Update()
    {
        can = Character.GetComponent<Character>().health;
        canBar.transform.localScale = new Vector3(can/fullCan,1,1);
    }
}
