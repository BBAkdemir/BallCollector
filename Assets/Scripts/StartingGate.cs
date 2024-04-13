using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingGate : MonoBehaviour
{
    public int howManyBallsShouldPass;
    public int howManyBallsPass;
    public float CamMovementSpeed;
    
    public bool OpenTheDoor = false;
    public bool CameraMove = false;
    public bool Switch = false;

    public GameObject CamPositionStart;
    public GameObject CamPositionSecondary;
    public GameObject Door1;
    public GameObject Door2;
    public GameObject Slider;
    public GameObject SliderSwipeUI;

    LevelSystem levelSystem;

    Quaternion Door1Spare;
    Quaternion Door2Spare;
    private void Start()
    {
        Camera.main.transform.position = CamPositionStart.transform.position;
        GameObject.FindWithTag("LevelSystem").GetComponent<SliderChangeNew>().Slider = Slider;
        SliderSwipeUI.SetActive(true);
        levelSystem = GameObject.FindWithTag("LevelSystem").GetComponent<LevelSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sheep" && howManyBallsPass <= levelSystem.CreatedBall.Count && OpenTheDoor == false)
        {
            howManyBallsPass += 1;
            Switch = true;
        }
    }
    void Update()
    {
        if (howManyBallsPass >= levelSystem.CreatedBall.Count && OpenTheDoor == false && Switch == true)
        {
            Door1Spare = Door1.transform.localRotation;
            Door2Spare = Door2.transform.localRotation;
            Switch = false;
            OpenTheDoor = true;
        }
        if (OpenTheDoor == true)
        {
            Door1.transform.localRotation = Quaternion.RotateTowards(Door1.transform.localRotation, Quaternion.Euler(Door1Spare.x, Door1Spare.y, Door1Spare.z - 90), 100 * Time.deltaTime);
            Door2.transform.localRotation = Quaternion.RotateTowards(Door2.transform.localRotation, Quaternion.Euler(Door2Spare.x, Door2Spare.y, Door2Spare.z + 90), 100 * Time.deltaTime);
            if (Door1.transform.eulerAngles.z <= -90 || Door2.transform.eulerAngles.z >= 90)
            {
                OpenTheDoor = false;
                CameraMove = true;
            }
        }
        if (CameraMove == true)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, CamPositionSecondary.transform.position, CamMovementSpeed * Time.deltaTime);
            if (Camera.main.transform.position == CamPositionSecondary.transform.position)
            {
                CameraMove = false;
            }
        }
    }
}
