using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastGate : MonoBehaviour
{
    public int howManyBallsShouldPass;
    public int howManyBallsPass;
    public float CamPositionSpeed1;
    public float CamPositionSpeed2;
    public float CamRotationSpeed2;

    public bool OpenTheDoor = false;
    public bool CameraMove1 = false;
    public bool CameraMove2 = false;
    public bool Switch = false;

    public GameObject CamPosition1;
    public GameObject CamPosition2;
    public GameObject Door1;
    public GameObject Door2;

    LevelSystem levelSystem;

    Quaternion Door1Spare;
    Quaternion Door2Spare;
    private void Start()
    {
        levelSystem = GameObject.FindWithTag("LevelSystem").GetComponent<LevelSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sheep" && howManyBallsPass <= levelSystem.CreatedBall.Count && CameraMove1 == false)
        {
            howManyBallsPass += 1;
            Switch = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (howManyBallsPass >= levelSystem.CreatedBall.Count && CameraMove1 == false && Switch == true)
        {
            Door1Spare = Door1.transform.localRotation;
            Door2Spare = Door2.transform.localRotation;
            Switch = false;
            CameraMove1 = true;
        }
        if (CameraMove1 == true)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, CamPosition1.transform.position, CamPositionSpeed1 * Time.deltaTime);
            if (Camera.main.transform.position == CamPosition1.transform.position)
            {
                CameraMove1 = false;
                OpenTheDoor = true;
            }
        }
        if (OpenTheDoor == true)
        {
            Door1.transform.localRotation = Quaternion.RotateTowards(Door1.transform.localRotation, Quaternion.Euler(Door1Spare.x, Door1Spare.y, Door1Spare.z - 90), 100 * Time.deltaTime);
            Door2.transform.localRotation = Quaternion.RotateTowards(Door2.transform.localRotation, Quaternion.Euler(Door2Spare.x, Door2Spare.y, Door2Spare.z + 90), 100 * Time.deltaTime);
            if (Door1.transform.eulerAngles.z <= -90 || Door2.transform.eulerAngles.z >= 90)
            {
                OpenTheDoor = false;
                CameraMove1 = false;
                CameraMove2 = true;
            }
        }
        if (CameraMove2 == true)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, CamPosition2.transform.position, CamPositionSpeed2 * Time.deltaTime);
            Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.localRotation, CamPosition2.transform.rotation, CamRotationSpeed2 * Time.deltaTime);
            if (Camera.main.transform.position == CamPosition2.transform.position)
            {
                CameraMove2 = false;
                CameraMove1 = false;
            }
        }
    }
}
