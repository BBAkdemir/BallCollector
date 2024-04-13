using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidersChange : MonoBehaviour
{
    Click click;
    public int hangiSlider = 0;
    public List<GameObject> Sliders;
    // Start is called before the first frame update
    void Start()
    {
        click = GameObject.FindWithTag("LevelSystem").GetComponent<Click>();
    }

    // Update is called once per frame
    void Update()
    {
        click.SelectedObject = Sliders[hangiSlider];
    }
}
