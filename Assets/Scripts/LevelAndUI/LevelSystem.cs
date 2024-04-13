using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    public List<GameObject> Levels;
    public int ActiveLevel = 0;
    public GameObject nextLevelButton;
    public bool createdObj;
    public List<GameObject> CreatedBall;

    public GameObject SuccessPanel;
    public GameObject FailPanel;
    public GameObject RestartButton;
    void Start()
    {
        CreatedBall = new List<GameObject>();
        //PlayerPrefs.SetInt("LastLevel", 3);
        ActiveLevel = PlayerPrefs.GetInt("LastLevel");
        foreach (var item in Levels)
        {
            if (item != Levels[ActiveLevel])
            {
                item.SetActive(false);
            }
        }
        Levels[ActiveLevel].SetActive(true);
    }
    public void NextLevel()
    {
        GameObject.FindWithTag("LevelSystem").GetComponent<Score>().Bolum = 0;
        GameObject.FindWithTag("LevelSystem").GetComponent<Score>().Puan = 0;
        SuccessPanel.SetActive(false);
        if (Levels[ActiveLevel] == Levels[Levels.Count - 1])
        {
            ActiveLevel = 0;
        }
        else
        {
            ActiveLevel += 1;
        }
        PlayerPrefs.SetInt("LastLevel", ActiveLevel);
        if (ActiveLevel == 0 && Levels[Levels.Count - 1].activeSelf == true)
        {
            Levels[Levels.Count - 1].SetActive(false);
            SceneManager.LoadScene("NewScene");
        }
        else
        {
            Levels[ActiveLevel - 1].SetActive(false);
        }
        CreatedBall.Clear();
        RestartButton.SetActive(true);
        Levels[ActiveLevel].SetActive(true);
        

    }
    public void RestartLevel()
    {
        GameObject.FindWithTag("LevelSystem").GetComponent<Score>().Bolum = 0;
        GameObject.FindWithTag("LevelSystem").GetComponent<Score>().Puan = 0;
        RestartButton.SetActive(true);
        FailPanel.SetActive(false);
        SceneManager.LoadScene("NewScene");
    }

    public void LevelFinished()
    {
        LeanTween.delayedCall(gameObject, 2f, () => {
            SuccessPanel.SetActive(true);
            RestartButton.SetActive(false);
        });
    }
    public void LevelField()
    {
        LeanTween.delayedCall(gameObject, 2f, () => {
            FailPanel.SetActive(true);
            RestartButton.SetActive(false);
        });/*bberkakdemir*/

    }
}
