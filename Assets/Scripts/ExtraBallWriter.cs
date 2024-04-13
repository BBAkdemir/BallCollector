using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExtraBallWriter : MonoBehaviour
{
    public GameObject PuanObject;
    void Start()
    {
        if (PuanObject.GetComponent<SheepAddInterest>().islem == Islem.carpi || PuanObject.GetComponent<SheepAddInterest>().islem == Islem.oyunSonu)
        {
            gameObject.GetComponent<TMP_Text>().text = "X" + PuanObject.GetComponent<SheepAddInterest>().sayi.ToString();
        }
        if (PuanObject.GetComponent<SheepAddInterest>().islem == Islem.arti)
        {
            gameObject.GetComponent<TMP_Text>().text = "+" + PuanObject.GetComponent<SheepAddInterest>().sayi.ToString();
        }
    }
}
