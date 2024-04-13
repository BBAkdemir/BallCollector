using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punchEffect : MonoBehaviour
{
    public GameObject target;
    bool calistir = false;
    // Start is called before the first frame update /*bberkakdemir*/
    void Start()
    {
        yokEt();
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame /*bberkakdemir*/
    void Update()
    {
        if (calistir == true)
        {
            if (Time.frameCount % 2 == 0 && gameObject.transform.localScale.x <= 0.5f) // Her 10 frame'de bir çalýþýr /*bberkakdemir*/
            {
                gameObject.transform.localScale += new Vector3(0.07f, 0.07f, 0.07f);
            }
            gameObject.transform.position = target.transform.position;
            gameObject.transform.localRotation = target.transform.localRotation;
        }
        
    }

    public void yokEt()
    {
        LeanTween.delayedCall(gameObject, 0.4f, () => {
            calistir = true;
        });
    }
}
