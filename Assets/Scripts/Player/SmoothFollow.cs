using System;
using System.ComponentModel;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public enum CameraState
    {
        Default,
        ZoomIn,
        ZoomOut,
        Zoomed
    }

    public class SmoothFollow : ResetableBehaviour
    {
        private const float ZoomDuration = 0.5f;
        private const float ZoomFactor = 2f;

        private readonly FloatAnimation distanceAnimation = new FloatAnimation();

        private CameraState cameraState;

        public float Distance = 10.0f;
        public float Height = 5.0f;

        public float HeightDamping = 2.0f;
        public float RotationDamping = 3.0f;

        public Transform Target;

        private void LateUpdate()
        {
            if (GameManager.Instance.GameState == GameState.GameOver || GameManager.Instance.GameState == GameState.Pause)
            {
                return;
            }

            // Early out if we don't have a target
            if (!Target)
            {
                return;
            }

            // Calculate the current rotation angles
            var wantedRotationAngle = Target.eulerAngles.y;
            var wantedHeight = Target.position.y + Height;

            var currentRotationAngle = transform.eulerAngles.y;
            var currentHeight = transform.position.y;

            var zoomDistance = Distance;

            // Damp the rotation around the y-axis
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, RotationDamping * Time.deltaTime);

            // Damp the Height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, HeightDamping * Time.deltaTime);

            // Convert the angle into a rotation
            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            void SetAnimationState(CameraState state)
            {
                cameraState = state;
                GameManager.Instance.IsHighSpeed = state == CameraState.ZoomIn;
                var startDistance = (transform.position - Target.transform.position).magnitude;
                var endDistance = cameraState == CameraState.ZoomIn ? zoomDistance / ZoomFactor : zoomDistance;
                distanceAnimation.Start(startDistance, endDistance, ZoomDuration);
            }

            // Check if space is up or down on the frame
            if (GameManager.Instance.GameState != GameState.StartMenu)
            {
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    SetAnimationState(CameraState.ZoomOut);
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    SetAnimationState(CameraState.ZoomIn);
                }
            }

            switch (cameraState)
            {
                case CameraState.ZoomIn:
                case CameraState.ZoomOut:
                    if (distanceAnimation.Update(Time.deltaTime))
                    {
                        cameraState = cameraState == CameraState.ZoomIn ? 
                            CameraState.Zoomed : CameraState.Default;
                    }
                    zoomDistance = distanceAnimation.Value;
                    break;
                case CameraState.Default:
                case CameraState.Zoomed:
                    zoomDistance = cameraState == CameraState.Default ? zoomDistance : zoomDistance / ZoomFactor;
                    break;
            }

            // Set the position of the camera on the x-z plane to:
            // Distance meters behind the target
            var newPosition = Target.position - currentRotation * Vector3.forward * zoomDistance;
            newPosition.y = currentHeight;
            transform.position = newPosition;

            // Always look at the target
            transform.LookAt(Target);
        }

        protected override void OnReset()
        {
            cameraState = CameraState.Default;
        }
    }
}