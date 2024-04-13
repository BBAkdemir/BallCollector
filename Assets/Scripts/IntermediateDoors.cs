using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateDoors : MonoBehaviour
{
    public int howManyBallsShouldPass;
    int howManyBallsPass;
    public float CamPositionSpeed;
    public float CamRotationSpeed;

    bool OpenTheDoor = false;
    bool CameraMove = false;
    bool Switch = false;

    public GameObject CamPosition;
    public GameObject Door1;
    public GameObject Door2;
    public GameObject Slider;
    public GameObject SliderSwipeUI;

    LevelSystem levelSystem;

    Quaternion Door1Spare;
    Quaternion Door2Spare;
    private void Start()
    {
        levelSystem = GameObject.FindWithTag("LevelSystem").GetComponent<LevelSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sheep" && howManyBallsPass <= levelSystem.CreatedBall.Count && CameraMove == false)
        {
            howManyBallsPass += 1;
            Switch = true;
        }
    }
    void Update()
    {
        if (howManyBallsPass >= levelSystem.CreatedBall.Count && CameraMove == false && Switch == true)
        {
            if (Door1 != null && Door2 != null)
            {
                Door1Spare = Door1.transform.localRotation;
                Door2Spare = Door2.transform.localRotation;
            }
            Switch = false;
            CameraMove = true;
        }
        if (CameraMove == true)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, CamPosition.transform.position, CamPositionSpeed * Time.deltaTime);
            Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.localRotation, CamPosition.transform.rotation, CamRotationSpeed * Time.deltaTime);
            if (Camera.main.transform.position == CamPosition.transform.position && Camera.main.transform.rotation == CamPosition.transform.rotation)
            {
                CameraMove = false;
                if (Slider != null)
                {
                    GameObject.FindWithTag("LevelSystem").GetComponent<SliderChangeNew>().Slider = Slider;
                    SliderSwipeUI.SetActive(true);
                }
                if (Door1 != null && Door2 != null)
                {
                    OpenTheDoor = true;
                }
            }
        }
        if (OpenTheDoor == true)
        {
            Door1.transform.localRotation = Quaternion.RotateTowards(Door1.transform.localRotation, Quaternion.Euler(Door1Spare.x, Door1Spare.y, Door1Spare.z - 90), 100 * Time.deltaTime);
            Door2.transform.localRotation = Quaternion.RotateTowards(Door2.transform.localRotation, Quaternion.Euler(Door2Spare.x, Door2Spare.y, Door2Spare.z + 90), 100 * Time.deltaTime);
            if (Door1.transform.eulerAngles.z <= -90 || Door2.transform.eulerAngles.z >= 90)
            {
                OpenTheDoor = false;
            }
        }
        
    }
}
