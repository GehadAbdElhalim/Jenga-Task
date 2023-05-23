using UnityEngine;

namespace JengaTask
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] float maxDistanceFromTarget = 12f;
        [SerializeField] float mouseSensitivity = 10;
        [SerializeField] Vector2 pitchMinMax = new Vector2(10, 80);
        [SerializeField] float rotationSmoothTime = .12f;

        private Vector3 target;
        private float dstFromTarget;

        private Vector3 rotationSmoothVelocity;
        private Vector3 currentRotation;

        private float yaw;
        private float pitch;

        public void SetTarget(Vector3 target)
        {
            this.target = target;
            dstFromTarget = Vector3.Distance(target, transform.position);
            if (dstFromTarget > maxDistanceFromTarget) 
            {
                dstFromTarget = maxDistanceFromTarget;
                transform.position = target - transform.forward * maxDistanceFromTarget;
            }
            currentRotation = transform.localEulerAngles;
            yaw = transform.localEulerAngles.y;
            pitch = transform.localEulerAngles.x;
            transform.LookAt(target);
        }

        void LateUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
                pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
                pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

                currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
                transform.eulerAngles = currentRotation;

                transform.position = target - transform.forward * dstFromTarget;
            }
        }
    }
}