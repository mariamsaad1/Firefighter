using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;

namespace MikeNspired.XRIStarterKit
{
    public class VehicleTeleportPlayer : MoveToLocation
    {
        public UnityEvent VehicleExited;

        [SerializeField] Transform enterLocation, exitLocation, playerPositionConstraint;
        [SerializeField] bool autoEnter = true; // ← خيار لتحديد إذا يبدأ اللاعب داخل السيارة

        private UnityEngine.XR.Interaction.Toolkit.Locomotion.LocomotionProvider[] moveProviders;
        private bool isActive;

        protected override void Awake()
        {
            base.Awake();
            moveProviders = rig.GetComponentsInChildren<LocomotionProvider>();
        }

        private void Start()
        {
            if (autoEnter)
                EnterVehicle(); // ← ينفذ الدخول مباشرة إذا autoEnter مفعل
        }

        public void EnterVehicle()
        {
            location = enterLocation;
            TeleportWithHeadAtLocationAndRotate();
            SetCharacterControllersState(false);
            rig.GetComponent<CharacterController>().enabled = false;
            playerPositionConstraint.position = rig.transform.position;
            playerPositionConstraint.rotation = rig.transform.rotation;
            isActive = true;
        }

        public void ExitVehicle()
        {
            location = exitLocation;
            isActive = false;
            VehicleExited.Invoke();
            TeleportWithFeetAtLocation();
            SetCharacterControllersState(true);
            rig.GetComponent<CharacterController>().enabled = true;
        }

        public void ExitVehicle1()
        {
            isActive = false;
            VehicleExited.Invoke();
            SetCharacterControllersState(true);
            rig.GetComponent<CharacterController>().enabled = true;
        }

        private void LateUpdate()
        {
            if (!isActive) return;
            rig.transform.position = playerPositionConstraint.transform.position;
            rig.transform.rotation = playerPositionConstraint.transform.rotation;
        }

        private void SetCharacterControllersState(bool state)
        {
            foreach (var moveProvider in moveProviders) moveProvider.enabled = state;
        }
    }
}
