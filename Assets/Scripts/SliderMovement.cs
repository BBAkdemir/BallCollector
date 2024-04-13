using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderMovement : MonoBehaviour
{
    Click click;
    public float sliderMovementSpeed;

    public float xMin, xMax;
    private void Start()
    {
        click = GameObject.FindWithTag("LevelSystem").GetComponent<Click>();
        xMax = 22;
        xMin = -22;
    }
    public void Update()
    {
        if (click.SelectedObject != null)
        {
            click.SelectedObject.transform.position = Vector3.MoveTowards(click.SelectedObject.transform.position,
            new Vector3(/*bberkakdemir*/
             Mathf.Clamp(click.SliderLastPosition.x, xMin, xMax),
             click.SelectedObject.transform.position.y,
             click.SelectedObject.transform.position.z)
            , sliderMovementSpeed * Time.deltaTime);
        }
    }
}
