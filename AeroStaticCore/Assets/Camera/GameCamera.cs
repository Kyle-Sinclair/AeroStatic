using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using MouseButton = UnityEngine.InputSystem.LowLevel.MouseButton;


public class GameCamera : MonoBehaviour {
    [SerializeField,Range(1f,50f)] private float _cameraMoveSpeed = 10f;
    [SerializeField,Range(30f,100f)] private float _cameraRotSpeed = 10f;
    [SerializeField,Range(1f,50f)] private float _cameraZoomSpeed = 10f;
    
    
    private Camera _camera;
    private AudioListener _audioListener;

    [SerializeField]
    private InputActionAsset _inputMap;

    private Vector2 _moveValue = new Vector2(0,0);
    
    private float _rotValue = 0;

    private Vector2 _zoomValue = new Vector2(0f,0f);
    
    private float _leftMouseButtonClicked = 0f;

    [SerializeField] private float _minZoom, _maxZoom;


    private Transform _stick, _swivel;
    public bool _debugRayCast = false;

    void Start() {
        _camera = GetComponentInChildren<Camera>();
        _audioListener = GetComponentInChildren<AudioListener>();
        _swivel = transform.GetChild(0);
        _stick = _swivel.transform.GetChild(0);
    }
    public void LateUpdate() {
        float delta = Time.deltaTime;
        MoveCamera(delta);
        RotateCamera(delta);
        ZoomCamera(delta);
        ProcessLeftClick(delta);
    }

    private void MoveCamera(float deltaTime) {
        //Debug.Log("Move value is currently " + _moveValue.x);
        transform.Translate(_moveValue.x * _cameraMoveSpeed * deltaTime, 0f, _moveValue.y * _cameraMoveSpeed * deltaTime);
    }

    private void RotateCamera(float deltaTime) {
        transform.Rotate(0f,(float)-1 * _rotValue * deltaTime * _cameraRotSpeed,0f);

    }

    private void ZoomCamera (float deltaTime){
        //_stick.transform.position
            //Vector3.SmoothDamp()
        _stick.transform.Translate(0f, 0f, _zoomValue.y * deltaTime * _cameraZoomSpeed);
    }
    private void ProcessLeftClick (float deltaTime) {
        //Debug.Log("Value stored for left mouse button click is : " + _leftMouseButtonClicked);


        if(_leftMouseButtonClicked != 0){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000)) {
                int xcoord = Mathf.FloorToInt(hit.point.x);
                int zcoord = Mathf.FloorToInt(hit.point.z);

                if (_debugRayCast) {
                    Debug.DrawLine(hit.point,50f * Vector3.up, Color.black,5f);
                    Debug.DrawLine(new Vector3(xcoord, hit.point.y,zcoord),new Vector3 ((xcoord + 1),hit.point.y,zcoord + 1),Color.green,5f);
                    String tileDescription = "Selected tile is : x-coord - " + xcoord + ", ";
                    tileDescription += "y-coord - " + hit.point.y + ", ";
                    tileDescription += "z-coord - " + zcoord;
                    
                    Debug.Log(tileDescription);
                    
                }
                //Debug.Log(hit.point);

                }
            }
        }

        /*if (_leftMouseButtonClicked != 0f) {
            //RaycastHit hit = new
            Vector2 MouseScreenPosition = Input.mousePosition;
            RaycastHit
            Debug.Log(MouseScreenPosition);
        }*/
    

    public void DisableCamera() {
        _camera.enabled = false;
        _audioListener.enabled = false;
    }
    
    public void EnableCamera() {
        _camera.enabled = true;
        _audioListener.enabled = true;

    }

    public void OnClick(InputAction.CallbackContext context) {
            _leftMouseButtonClicked = context.ReadValue<float>();
        
    }
    
    public void OnMove(InputAction.CallbackContext context) {
        _moveValue = context.ReadValue<Vector2>().normalized;
    }

    public void OnRotate(InputAction.CallbackContext context) {
        _rotValue = context.ReadValue<float>();
    }

    public void OnZoom(InputAction.CallbackContext context) {
        _zoomValue = context.ReadValue<Vector2>();
    }

  
}
