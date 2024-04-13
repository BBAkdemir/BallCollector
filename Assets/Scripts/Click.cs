using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public GameObject SelectedObject;
    public Vector3 MouseMovePosition;
    public Vector3 MouseFirstPosition;
    public Vector3 SliderFirstPosition;
    public Vector3 SliderLastPosition;
    public float SliderPosition;/*bberkakdemir*/

    private void Update()
    {
        if (MouseFirstPosition.x > MouseMovePosition.x)
        {
            SliderPosition = -Mathf.Abs(MouseFirstPosition.x - MouseMovePosition.x);
            SliderLastPosition.x = SliderFirstPosition.x + SliderPosition;/*bberkakdemir*/
        }
        else
        {
            SliderPosition = Mathf.Abs(MouseFirstPosition.x - MouseMovePosition.x);
            SliderLastPosition.x = SliderFirstPosition.x + SliderPosition;/*bberkakdemir*/
        }
    }
}
