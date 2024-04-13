using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class AnimatorController : MonoBehaviour, IPointerClickHandler
{
    public GameObject TapCountPanelObject;
    public GameObject ClickObject;
    public GameObject PlayerMonster;
    public GameObject EnemyMonster;
    public GameObject GameEndUI;
    public GameObject ShootPanel;
    public GameObject EnemyHeadPunchPosition;
    public GameObject EnemyHeadKickPosition;
    public GameObject PlayerHeadPunchPosition;
    public GameObject PlayerHeadKickPosition;
    public GameObject punchEffect;
    public GameObject GameEndConfeti;

    public float TapTime;
    public float shouldTapTime;
    public float TapShouldCount;
    public float TapCount;

    bool AttackAnimation = false;
    bool DamageAnimation = false;
    bool oyunBitti = false;

    int rnd;
    public float buyumeOrani;
    public float kuculmeHizi;
    /*bberkakdemir*/
    private void Start()
    {
        TapCountPanelObject.SetActive(true);
        ClickObject.SetActive(true);
        ShootPanel.SetActive(true);
        InvokeRepeating("TapCountLower", 0.05f, 0.05f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        TapCount++;
        ShootPanel.transform.localScale = new Vector3(TapCount/TapShouldCount, TapCount / TapShouldCount, 0);
    }
    void Update()
    {
        if (oyunBitti == false)
        {
            TapTime += Time.deltaTime;
            if (TapTime >= shouldTapTime || TapCount >= TapShouldCount)
            {
                if (TapCount >= TapShouldCount)
                {
                    AttackAnimation = true;
                    TapCount = 0;
                    TapTime = 0;
                    EnemyMonster.GetComponent<Character>().health -= PlayerMonster.GetComponent<Character>().Power;
                    ShootPanel.transform.localScale = new Vector3(0, 0, 0);
                }
                else
                {
                    DamageAnimation = true;
                    TapTime = 0;
                    PlayerMonster.GetComponent<Character>().health -= EnemyMonster.GetComponent<Character>().Power;
                }
            }
            if (AttackAnimation == true)
            {
                if (EnemyMonster.GetComponent<Character>().health <= 0)
                {
                    EnemyMonster.GetComponent<Character>().health = 0;
                    PlayerMonster.GetComponent<Animator>().SetTrigger("SonVurus");
                    EnemyMonster.GetComponent<Animator>().SetTrigger("Dead");
                    PlayerMonster.GetComponent<Animator>().SetTrigger("KazanmaKukremesi");
                    PlayerMonster.GetComponent<Animator>().SetTrigger("Dance");
                    GameObject objeNew = Instantiate(punchEffect, EnemyHeadPunchPosition.transform.position, EnemyHeadPunchPosition.transform.rotation);
                    objeNew.GetComponent<punchEffect>().target = EnemyHeadPunchPosition;
                    GameEndUI.SetActive(false);
                    GameEndConfeti.SetActive(true);
                    GameObject.FindWithTag("LevelSystem").GetComponent<LevelSystem>().LevelFinished();
                    oyunBitti = true;
                }
                else
                {
                    rnd = Random.Range(0, 2);
                    if (rnd == 0)
                    {
                        PlayerMonster.GetComponent<Animator>().SetTrigger("Attack1");
                        EnemyMonster.GetComponent<Animator>().SetTrigger("Damage2");
                        GameObject objeNew = Instantiate(punchEffect, EnemyHeadPunchPosition.transform.position, EnemyHeadPunchPosition.transform.rotation);
                        objeNew.GetComponent<punchEffect>().target = EnemyHeadPunchPosition;
                    }
                    if (rnd == 1)
                    {
                        PlayerMonster.GetComponent<Animator>().SetTrigger("Attack2");
                        EnemyMonster.GetComponent<Animator>().SetTrigger("Damage1");
                        GameObject objeNew = Instantiate(punchEffect, EnemyHeadKickPosition.transform.position, EnemyHeadKickPosition.transform.rotation);
                        objeNew.GetComponent<punchEffect>().target = EnemyHeadKickPosition;
                    }
                }
                AttackAnimation = false;
            }
            if (DamageAnimation == true)
            {
                if (PlayerMonster.GetComponent<Character>().health <= 0)
                {
                    PlayerMonster.GetComponent<Character>().health = 0;
                    EnemyMonster.GetComponent<Animator>().SetTrigger("SonVurus");
                    PlayerMonster.GetComponent<Animator>().SetTrigger("Dead");
                    EnemyMonster.GetComponent<Animator>().SetTrigger("KazanmaKukremesi");
                    EnemyMonster.GetComponent<Animator>().SetTrigger("Dance");
                    /*bberkakdemir*/
                    GameObject objeNew = Instantiate(punchEffect, PlayerHeadPunchPosition.transform.position, PlayerHeadPunchPosition.transform.rotation);
                    objeNew.GetComponent<punchEffect>().target = PlayerHeadPunchPosition;
                    GameEndUI.SetActive(false);
                    oyunBitti = true;
                    GameObject.FindWithTag("LevelSystem").GetComponent<LevelSystem>().LevelField();
                }
                else
                {
                    rnd = Random.Range(0, 2);
                    if (rnd == 0)
                    {
                        EnemyMonster.GetComponent<Animator>().SetTrigger("Attack2");
                        PlayerMonster.GetComponent<Animator>().SetTrigger("Damage1");
                        GameObject objeNew = Instantiate(punchEffect, PlayerHeadKickPosition.transform.position, PlayerHeadKickPosition.transform.rotation);
                        objeNew.GetComponent<punchEffect>().target = PlayerHeadKickPosition;
                    }
                    if (rnd == 1)
                    {
                        EnemyMonster.GetComponent<Animator>().SetTrigger("Attack1");
                        PlayerMonster.GetComponent<Animator>().SetTrigger("Damage2");
                        GameObject objeNew = Instantiate(punchEffect, PlayerHeadPunchPosition.transform.position, PlayerHeadPunchPosition.transform.rotation);
                        objeNew.GetComponent<punchEffect>().target = PlayerHeadPunchPosition;
                    }
                }
                DamageAnimation = false;
            }
        }
    }
    public void TapCountLower()
    {
        if (TapCount > 0)
        {
            TapCount -= 0.1f;
            ShootPanel.transform.localScale = new Vector3(TapCount / TapShouldCount, TapCount / TapShouldCount, 0);
        }
    }
    /*bberkakdemir*/
}
