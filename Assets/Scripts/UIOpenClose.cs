using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOpenClose : MonoBehaviour
{
    public float ActiveTime;
    private void Start()
    {
        if (ActiveTime != 0)
        {
            Destroy(gameObject, ActiveTime);
        }
        else
        {
            Destroy(gameObject, 4);
        }
    }
}
