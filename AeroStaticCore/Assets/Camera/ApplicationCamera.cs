using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationCamera : MonoBehaviour {
    private Camera _camera;

    private AudioListener _audioListener;
    // Start is called before the first frame update
    void Start() {
        _camera = GetComponentInChildren<Camera>();
        _audioListener = GetComponentInChildren<AudioListener>();

    }

    public void DisableCamera() {
        _camera.enabled = false;
        _audioListener.enabled = false;
    }
    
    public void EnableCamera() {
        _camera.enabled = true;
        _audioListener.enabled = true;

    }
}
