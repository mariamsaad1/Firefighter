using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace UnityEngine.XR.Interaction.Toolkit.Locomotion
{
    public class VRVehicleController : MonoBehaviour
    {
        [Header("Vehicle Settings")]
        public float maxMotorTorque = 1500f; // قوة الموتور لكل عجلة
        public float brakeForce = 3000f;     // قوة الفرملة
        public float steeringAngle = 30f;    // زاوية التوجيه

        [Header("Wheel Colliders & Visuals")]
        public WheelCollider frontLeftWheel;
        public WheelCollider frontRightWheel;
        public WheelCollider rearLeftWheel;
        public WheelCollider rearRightWheel;
        public Transform frontLeftVisual;
        public Transform frontRightVisual;
        public Transform rearLeftVisual;
        public Transform rearRightVisual;

        [Header("VR References (Input Actions)")]
        public InputActionProperty accelAction;      // Right Trigger
        public InputActionProperty brakeAction;      // Left Trigger
        public InputActionProperty steerAction;      // Joystick (x axis)

        [Header("Rigidbody Reference")]
        public Rigidbody rb;

        void Start()
        {
            // منع العجلات من التحرك عند بداية اللعبة
            // frontLeftWheel.motorTorque = 0f;
            // frontRightWheel.motorTorque = 0f;
            // rearLeftWheel.motorTorque = 0f;
            // rearRightWheel.motorTorque = 0f;

            // frontLeftWheel.brakeTorque = brakeForce;
            // frontRightWheel.brakeTorque = brakeForce;
            // rearLeftWheel.brakeTorque = brakeForce;
            // rearRightWheel.brakeTorque = brakeForce;

            // // ضبط Center of Mass للثبات
            // rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
        }

        void FixedUpdate()
        {
            HandleInput();
            UpdateWheelVisuals();
        }

       void HandleInput()
{
    // قراءة الإدخالات مع عكس المحور
    float accelInput = Mathf.Clamp01(accelAction.action.ReadValue<float>()); 
    float brakeInput = Mathf.Clamp01(brakeAction.action.ReadValue<float>()); 
    Vector2 steerInputVec = steerAction.action.ReadValue<Vector2>();
    float steerInput = steerInputVec.x; 

    // حساب قوة الموتور والفرملة
    float motor = accelInput * maxMotorTorque;
    float brake = brakeInput * brakeForce;

    // إذا هناك تسارع، نوقف الفرملة تلقائيًا
    if (motor > 0f)
        brake = 0f;

    // تطبيق الموتور
    frontLeftWheel.motorTorque = motor;
    frontRightWheel.motorTorque = motor;
    rearLeftWheel.motorTorque = motor;
    rearRightWheel.motorTorque = motor;

    // تطبيق الفرملة
    frontLeftWheel.brakeTorque = brake;
    frontRightWheel.brakeTorque = brake;
    rearLeftWheel.brakeTorque = brake;
    rearRightWheel.brakeTorque = brake;

    // تطبيق التوجيه
    float steer = steerInput * steeringAngle;
    frontLeftWheel.steerAngle = steer;
    frontRightWheel.steerAngle = steer;
}


        void UpdateWheelVisuals()
        {
            UpdateWheel(frontLeftWheel, frontLeftVisual);
            UpdateWheel(frontRightWheel, frontRightVisual);
            UpdateWheel(rearLeftWheel, rearLeftVisual);
            UpdateWheel(rearRightWheel, rearRightVisual);
        }

        void UpdateWheel(WheelCollider col, Transform visual)
        {
            Vector3 pos;
            Quaternion rot;
            col.GetWorldPose(out pos, out rot);
            visual.position = pos;
            visual.rotation = rot;
        }
    }
}
