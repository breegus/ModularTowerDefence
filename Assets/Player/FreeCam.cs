using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class FlyCamera : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 15f;  // Movement speed
        public float sprintMultiplier = 2f;  // Speed boost
        public float lookSensitivity = 0.2f;  // Mouse look sensitivity

        private InputSystem_Actions _controls;
        
        private bool _cameraFocused;
        private float _yaw;
        private float _pitch;

        private void Awake()
        {
            _controls = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            _controls.FreeCam.Enable();
            SetFocus(true);
        }

        private void OnDisable()
        {
            _controls.FreeCam.Disable();
            SetFocus(false);
        }

        private void Start()
        {
            SetFocus(false);
        }
        
        private void SetFocus(bool focus)
        {
            _cameraFocused = focus;
            Cursor.visible = !focus;
            Cursor.lockState = focus ? CursorLockMode.Locked : CursorLockMode.None;
        }
    
        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus) 
                SetFocus(true);
        }

        private void Update()
        {
            if (!Application.isFocused) return;  // Only respond if the window is focused

            if (!_cameraFocused && _controls.FreeCam.Unfreeze.IsPressed())  // Unfreeze
                SetFocus(true);
            
            if (_cameraFocused && _controls.FreeCam.Freeze.IsPressed())  // Freeze
                SetFocus(false);

            if (!_cameraFocused) return;  // If frozen, do nothing
            
            var move = _controls.FreeCam.Move.ReadValue<Vector2>();  // Move
            var look = _controls.FreeCam.Look.ReadValue<Vector2>();  // Look
            var elevate = _controls.FreeCam.Elevate.ReadValue<float>();  // Elevate
            
            var speed = _controls.FreeCam.Sprint.IsPressed()  // Sprint
                ? moveSpeed * sprintMultiplier
                : moveSpeed;

            _yaw += look.x * lookSensitivity;
            _pitch -= look.y * lookSensitivity;
            _pitch = Mathf.Clamp(_pitch, -89f, 89f);
            transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);

            var dir =
                transform.right * move.x +
                transform.forward * move.y +
                transform.up * elevate;

            transform.position += dir * (speed * Time.deltaTime);
        }
    }
}
