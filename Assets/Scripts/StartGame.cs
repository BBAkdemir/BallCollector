using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public int KacTaneGecmeli;
    public int KacTaneGecti;
    public List<GameObject> CamPositions;
    bool tetikle =  false;
    public bool KapilariAc = false;
    public bool KamerayiHareketEttir = false;
    public GameObject Kapi1;
    public GameObject Kapi2;

    public float CamMovementSpeed;

    Score BolumScript;

    Quaternion kapi1Yedek;
    Quaternion kapi2Yedek;
    void Start()
    {
        BolumScript = GameObject.FindWithTag("LevelSystem").GetComponent<Score>();
        BolumScript.Bolum = 0;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Sheep" && KacTaneGecti <= KacTaneGecmeli && KapilariAc == false)
        {
            KacTaneGecti += 1;
            tetikle = true;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (KacTaneGecti >= KacTaneGecmeli && KapilariAc == false && tetikle == true)
        {
            kapi1Yedek = Kapi1.transform.localRotation;
            kapi2Yedek = Kapi2.transform.localRotation;
            tetikle = false;
            KapilariAc = true;
        }
        if (KapilariAc == true)
        {
            Kapi1.transform.localRotation = Quaternion.RotateTowards(Kapi1.transform.localRotation, Quaternion.Euler(kapi1Yedek.x, kapi1Yedek.y, kapi1Yedek.z + 90), 100 * Time.deltaTime);
            Kapi2.transform.localRotation = Quaternion.RotateTowards(Kapi2.transform.localRotation, Quaternion.Euler(kapi2Yedek.x, kapi2Yedek.y, kapi2Yedek.z - 90), 100 * Time.deltaTime);
            if (Kapi1.transform.eulerAngles.z >= 90 && Kapi2.transform.eulerAngles.z <= 270)
            {
                KapilariAc = false;
                BolumScript.Bolum += 1;
                KamerayiHareketEttir = true;
            }
        }
        if (KamerayiHareketEttir == true)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, CamPositions[BolumScript.Bolum].transform.position, CamMovementSpeed * Time.deltaTime);
            if (Camera.main.transform.position == CamPositions[BolumScript.Bolum].transform.position)
            {
                KamerayiHareketEttir = false;
                //GameObject.Find("BallGenerateScript").GetComponent<SlidersChange>().hangiSlider += 1;
                BolumScript.Bolum += 1;
            }
        }
    }
}
