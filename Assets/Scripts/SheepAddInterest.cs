using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public enum Islem
{
    arti,
    eksi,
    carpi,
    oyunSonu
}
public class SheepAddInterest : MonoBehaviour
{
    public Islem islem;
    public int sayi;
    public GameObject SheepGenerateGameobject;
    public List<Material> Colors;
    public List<float> Scales;
    SheepGenerate sheepGenerateScript;
    Score scoreScript;
    Gecenler gecenlerScript;
    LevelSystem levelSystem;
    public ParticleSystem patlama;
    public ParticleSystem Yutma;
    public GameObject EksiYazisi;
    public int FirlatmaGucu;
    System.Random rnd;
    private void Start()
    {
        sheepGenerateScript = SheepGenerateGameobject.GetComponent<SheepGenerate>();
        scoreScript = GameObject.FindWithTag("LevelSystem").GetComponent<Score>();
        levelSystem = GameObject.FindWithTag("LevelSystem").GetComponent<LevelSystem>();
        gecenlerScript = gameObject.transform.parent.gameObject.GetComponent<Gecenler>();
        rnd = new System.Random();
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (islem == Islem.arti && gecenlerScript.GecenlerList.Any(x => x == collision.gameObject) == false)
            {
                gecenlerScript.GecenlerList.Add(collision.gameObject);
                for (int i = 0; i < sayi; i++)
                {
                    GameObject obje = sheepGenerateScript.HavuzdanObjeCek();
                    int randColor = rnd.Next(Colors.Count);
                    obje.GetComponent<MeshRenderer>().material = collision.gameObject.GetComponent<MeshRenderer>().material;
                    int randScales = rnd.Next(Scales.Count);
                    obje.transform.localScale = new Vector3(Scales[randScales], Scales[randScales], Scales[randScales]);
                    obje.transform.position = collision.gameObject.transform.position;
                    gecenlerScript.GecenlerList.Add(obje);
                    if (levelSystem.CreatedBall.Any(x => x == obje) == false)
                    {
                        levelSystem.CreatedBall.Add(obje);
                    }
                }
            }
            if (islem == Islem.carpi && gecenlerScript.GecenlerList.Any(x => x == collision.gameObject) == false)
            {
                gecenlerScript.GecenlerList.Add(collision.gameObject);
                for (int i = 0; i < sayi - 1; i++)
                {
                    GameObject obje = sheepGenerateScript.HavuzdanObjeCek();
                    int randColor = rnd.Next(Colors.Count);
                    obje.GetComponent<MeshRenderer>().material = collision.gameObject.GetComponent<MeshRenderer>().material;
                    int randScales = rnd.Next(Scales.Count);
                    obje.transform.localScale = new Vector3(Scales[randScales], Scales[randScales], Scales[randScales]);
                    obje.transform.position = collision.gameObject.transform.position;
                    gecenlerScript.GecenlerList.Add(obje);
                    if (levelSystem.CreatedBall.Any(x => x == obje) == false)
                    {
                        levelSystem.CreatedBall.Add(obje);
                    }
                }
            }
            
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (islem == Islem.eksi)
            {
                levelSystem.CreatedBall.Remove(collision.gameObject);
                if (patlama != null && EksiYazisi != null)
                {
                    Instantiate(patlama, collision.gameObject.transform.position, Quaternion.identity);
                    GameObject YaziNew = Instantiate(EksiYazisi, collision.gameObject.transform.position, Quaternion.identity);
                    YaziNew.GetComponent<Rigidbody>().AddForce(-FirlatmaGucu, FirlatmaGucu, 0, ForceMode.Impulse);
                }
                if (Yutma != null)
                {
                    Instantiate(Yutma, collision.gameObject.transform.position, Quaternion.identity);
                }
                sheepGenerateScript.HavuzaObjeEkle(collision.gameObject);
            }
            if (islem == Islem.oyunSonu)
            {
                if (levelSystem.CreatedBall.Any(x => x == collision.gameObject) == false)
                {
                    levelSystem.CreatedBall.Add(collision.gameObject);
                }
                scoreScript.Puan += sayi;
                if (scoreScript.Puan >= 500)
                {
                    GameObject.FindWithTag("UIObject").transform.GetChild(1).gameObject.SetActive(true);
                }
                collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                //sheepGenerateScript.HavuzaObjeEkle(collision.gameObject);
            }
        }
    }
}
