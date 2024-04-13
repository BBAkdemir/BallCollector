using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SheepGenerate : MonoBehaviour
{
    public GameObject SheepObject;
    public int BaslangictaKacObjeOlusacak;
    public List<GameObject> camPositions;
    public List<Material> Colors;
    public List<float> Scales;
    LevelSystem levelSystem;
    System.Random rnd;

    private Stack<GameObject> objeHavuzu = new Stack<GameObject>();

    public void Start()
    {
        GameObject.FindWithTag("LevelSystem").GetComponent<LevelSystem>().createdObj = false;
        levelSystem = GameObject.FindWithTag("LevelSystem").GetComponent<LevelSystem>();
        rnd = new System.Random();
        Camera.main.transform.position = camPositions[0].transform.position;
        Camera.main.transform.rotation = camPositions[0].transform.rotation;
    }
    private void Update()
    {
        if (camPositions[0] != null && Camera.main.transform.position == camPositions[0].transform.position && GameObject.FindWithTag("LevelSystem").GetComponent<LevelSystem>().createdObj == false)
        {
            if (camPositions != null)
            {
                int a = 1;
                for (int i = 0; i < BaslangictaKacObjeOlusacak; i++)
                {
                    // Havuzdan obje çekip konumunu deðiþtir
                    GameObject obje = HavuzdanObjeCek();
                    int randColor = rnd.Next(Colors.Count);
                    obje.GetComponent<MeshRenderer>().material = Colors[randColor];
                    int randScales = rnd.Next(Scales.Count);
                    obje.transform.localScale = new Vector3(Scales[randScales], Scales[randScales], Scales[randScales]);
                    levelSystem.CreatedBall.Add(obje);
                    obje.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                }
            }
            GameObject.FindWithTag("LevelSystem").GetComponent<LevelSystem>().createdObj = true;
        }
    }
    public GameObject HavuzdanObjeCek()
    {
        // Havuzda obje var mý kontrol et
        if (objeHavuzu.Count > 0)
        {
            // Havuzdaki en son objeyi çek
            GameObject obje = objeHavuzu.Pop();

            // Objeyi aktif hale getir
            obje.gameObject.SetActive(true);

            // Objeyi döndür
            return obje;
        }
        // Havuz boþ, mecburen yeni bir obje Instantiate et
        return Instantiate(SheepObject);
    }

    public void HavuzaObjeEkle(GameObject obje)
    {
        // Objeyi inaktif hale getir (böylece obje artýk ekrana çizilmeyecek ve objede
        // Update vs. fonksiyonlar varsa, bu fonksiyonlar obje havuzdayken çalýþtýrýlmayacak)/*bberkakdemir*/
        obje.gameObject.SetActive(false);

        // Objeyi havuza ekle
        objeHavuzu.Push(obje);
        if (levelSystem.CreatedBall.Count <= 0)
        {
            levelSystem.FailPanel.SetActive(true);
            levelSystem.RestartButton.SetActive(false);
        }
        

    }
}
