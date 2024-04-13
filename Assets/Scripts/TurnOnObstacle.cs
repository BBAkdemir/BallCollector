using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Yon
{
    Sag,
    Sol
}
public class TurnOnObstacle : MonoBehaviour
{
    public float rotationSpeed;
    public Yon yon;

    void Update()
    {
        if (gameObject.tag == "SpinningObstacle")
        {
            if (yon == 0)
            {
                Vector3 rot = transform.eulerAngles;
                transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z + Time.deltaTime * rotationSpeed);
            }
            else
            {
                Vector3 rot = transform.eulerAngles;
                transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z - Time.deltaTime * rotationSpeed);
            }
            
        }
        if (gameObject.tag == "SwingingObstacle")
        {
            Vector3 rot = transform.eulerAngles;
            float newZ = Mathf.Sin(Time.time * rotationSpeed);
            transform.localRotation = Quaternion.Euler(rot.x, rot.y, newZ * 60);
        }

    }
}
