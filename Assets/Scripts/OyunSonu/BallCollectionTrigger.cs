using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollectionTrigger : MonoBehaviour
{
    List<GameObject> gecenler;
    public GameObject CharacterPlayer;
    public GameObject dogmaNoktasi;
    public GameObject DovusObject;
    public GameObject PlayerCanObject;
    public GameObject EnemyCanObject;
    public GameObject DovusKonumu;
    public GameObject CamPosition1;
    public GameObject CamPosition2;

    LevelSystem levelSystem;

    int GecmeSayisi = 0;
    int BallCount = 0;
    bool yuru = false;
    bool Cam1Movement = false;
    bool Cam2Movement = false;
    bool Toplan = false;

    public int KotuTopSayisi;
    public int KotuHealth;
    public int KotuPower;
    public int NormalTopSayisi;
    public int NormalHealth;
    public int NormalPower;
    public int IyiTopSayisi;
    public int IyiHealth;
    public int IyiPower;
    public float CharacterPositionSpeed;
    public float CamPositionSpeed1;
    public float CamRotationSpeed1;
    public float CamPositionSpeed2;
    public float CamRotationSpeed2;
    public float BallCollectPositionSpeed;

    private void OnTriggerExit(Collider other)
    {
        if (Toplan == false)
        {
            //other.gameObject.GetComponent<Collider>().isTrigger = true;
            //gecenler.Add(other.gameObject);
            //BallCount++;
            foreach (var item in levelSystem.CreatedBall)
            {
                Destroy(item.gameObject.GetComponent<Rigidbody>());
                item.GetComponent<Collider>().isTrigger = true;
            }
            Toplan = true;
            BallCount = levelSystem.CreatedBall.Count;
        }
        

    }
    void Start()
    {
        //CharacterPlayer.GetComponent<Animator>().speed = 0;
        gecenler = new List<GameObject>();
        levelSystem = GameObject.FindWithTag("LevelSystem").GetComponent<LevelSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Toplan == true)
        {
            foreach (var item in levelSystem.CreatedBall)
            {
                if (item != null)
                {
                    item.transform.position = Vector3.MoveTowards(item.transform.position, dogmaNoktasi.transform.position, BallCollectPositionSpeed * Time.deltaTime);
                    if (item.transform.position == dogmaNoktasi.transform.position)
                    {
                        GecmeSayisi++;
                        if (GecmeSayisi == 1)
                        {
                            CharacterPlayer.transform.position = dogmaNoktasi.transform.position;
                            CharacterPlayer.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                            CharacterPlayer.SetActive(true);
                            CharacterPlayer.GetComponent<Animator>().SetTrigger("Kukreme");
                            Cam1Movement = true;
                        }
                        if (GecmeSayisi > 1)
                        {
                            CharacterPlayer.transform.localScale += new Vector3(0.08f, 0.08f, 0.08f);
                        }
                        if (GecmeSayisi == levelSystem.CreatedBall.Count / 2)
                        {
                            CharacterPlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                            CharacterPlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        }
                        if (GecmeSayisi == levelSystem.CreatedBall.Count)
                        {
                            if (levelSystem.CreatedBall.Count >= KotuTopSayisi && levelSystem.CreatedBall.Count < NormalTopSayisi)
                            {
                                CharacterPlayer.transform.localScale = new Vector3(8f, 8f, 8f);
                                CharacterPlayer.GetComponent<Character>().health = KotuHealth;
                                CharacterPlayer.GetComponent<Character>().Power = KotuPower;
                                DovusObject.GetComponent<AnimatorController>().shouldTapTime = 2;
                                DovusObject.GetComponent<AnimatorController>().TapShouldCount = 10;
                                DovusObject.GetComponent<AnimatorController>().buyumeOrani = 1 / 10;
                            }
                            if (levelSystem.CreatedBall.Count >= NormalTopSayisi && levelSystem.CreatedBall.Count < IyiTopSayisi)
                            {
                                CharacterPlayer.transform.localScale = new Vector3(10f, 10f, 10f);
                                CharacterPlayer.GetComponent<Character>().health = NormalHealth;
                                CharacterPlayer.GetComponent<Character>().Power = NormalPower;
                                DovusObject.GetComponent<AnimatorController>().shouldTapTime = 2;
                                DovusObject.GetComponent<AnimatorController>().TapShouldCount = 7;
                                DovusObject.GetComponent<AnimatorController>().buyumeOrani = 1 / 7;
                            }
                            if (levelSystem.CreatedBall.Count >= IyiTopSayisi)
                            {
                                CharacterPlayer.transform.localScale = new Vector3(12f, 12f, 12f);
                                CharacterPlayer.GetComponent<Character>().health = IyiHealth;
                                CharacterPlayer.GetComponent<Character>().Power = IyiPower;
                                DovusObject.GetComponent<AnimatorController>().shouldTapTime = 2;
                                DovusObject.GetComponent<AnimatorController>().TapShouldCount = 5;
                                DovusObject.GetComponent<AnimatorController>().buyumeOrani = 1 / 5;
                            }
                            yuru = true;
                            Cam1Movement = false;
                            Cam2Movement = true;
                            CharacterPlayer.GetComponent<Animator>().SetTrigger("Yuru");
                            Toplan = false;
                        }
                        Destroy(item);
                    }
                }
            }
        }
        if (Cam1Movement == true)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, CamPosition1.transform.position, CamPositionSpeed1 * Time.deltaTime);
            Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.localRotation, CamPosition1.transform.rotation, CamRotationSpeed1 * Time.deltaTime);
        }
        if (Cam2Movement == true)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, CamPosition2.transform.position, CamPositionSpeed2 * Time.deltaTime);
            Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.localRotation, CamPosition2.transform.rotation, CamRotationSpeed2 * Time.deltaTime);
            if (Camera.main.transform.position == CamPosition2.transform.position && Camera.main.transform.rotation == CamPosition2.transform.rotation)
            {
                Cam2Movement = false;
            }
        }
        if (yuru == true)
        {
            CharacterPlayer.transform.position = Vector3.MoveTowards(CharacterPlayer.transform.position, DovusKonumu.transform.position, CharacterPositionSpeed * Time.deltaTime);
            if (CharacterPlayer.transform.position.x == DovusKonumu.transform.position.x && CharacterPlayer.transform.position.z == DovusKonumu.transform.position.z)
            {
                yuru = false;
                DovusObject.SetActive(true);
                PlayerCanObject.SetActive(true);
                EnemyCanObject.SetActive(true);
                PlayerCanObject.transform.GetChild(0).GetChild(0).GetComponent<CanBar>().enabled = true;
                EnemyCanObject.transform.GetChild(0).GetChild(0).GetComponent<CanBar>().enabled = true;
                DovusObject.GetComponent<AnimatorController>().enabled = true;
            }
        }
    }
}
