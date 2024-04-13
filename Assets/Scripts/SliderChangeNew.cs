using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderChangeNew : MonoBehaviour
{
    Click click;
    public GameObject Slider;
    void Start()
    {
        click = GameObject.FindWithTag("LevelSystem").GetComponent<Click>();
    }

    // Update is called once per frame
    void Update()
    {
        click.SelectedObject = Slider;
    }
}
