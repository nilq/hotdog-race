using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo {
    public Transform leftWheel_t;
    public Transform rightWheel_t;
    public bool motor;
    public bool steering;
}

public class CarController : MonoBehaviour {
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    public void ApplyLocalPositionToVisuals(WheelCollider collider, Transform visualWheel) {
        Vector3 position;
        Quaternion rotation;
        
        collider.GetWorldPose(out position, out rotation);

        //visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate() {

        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos) {

            WheelCollider leftWheel = axleInfo.leftWheel_t.GetComponent<WheelCollider>();
            WheelCollider rightWheel = axleInfo.leftWheel_t.GetComponent<WheelCollider>();

            if (axleInfo.steering) {
                leftWheel.steerAngle = steering;
                rightWheel.steerAngle = steering;
            }

            if (axleInfo.motor) {
                leftWheel.motorTorque = motor;
                rightWheel.motorTorque = motor;
            }

            ApplyLocalPositionToVisuals(leftWheel, axleInfo.leftWheel_t);
            ApplyLocalPositionToVisuals(rightWheel, axleInfo.rightWheel_t);
        }
    }
}