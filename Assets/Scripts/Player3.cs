using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3 : MonoBehaviour {

    public Joypad joypad1;
    public Joypad joypad2;
    public Rigidbody rb;
    public Transform cameraTransform;
    public Transform togetherCameraPosition;
    public Transform readyToSeparateCameraPosition;
    public GameObject[] playerComplexes;
    public GameObject player1;
    public GameObject player2;
    public float force;
    public float separateDegrees = 135f;
    public float separationStartTime = 1f;
    public float separationEndTime = 2f;
    public float separationDeadzone = .5f;
    private bool _separationStarted = false;
    private float _separationAmount = 0f;
    
    private void OnEnable() {
        _separationAmount = separationEndTime;
    }
    private void Update() {
        SeparationLogic();
    }
    private void SeparationLogic() {
        var input1 = joypad1.input;
        var input2 = joypad2.input;
        if (
            // Input.GetKey(KeyCode.Space)||
            (input1.magnitude > separationDeadzone 
            && input2.magnitude > separationDeadzone 
            && Vector2.Angle(input1.normalized, input2.normalized) > separateDegrees))
            _separationAmount += Time.deltaTime;
        else
            _separationAmount -= Time.deltaTime;

        if (_separationAmount > separationEndTime) {
            //separate
            player1.transform.position = transform.position + (Vector3.forward * -1f);
            player2.transform.position = transform.position + (Vector3.forward * 1f);
            playerComplexes[2].SetActive(false);
            playerComplexes[1].SetActive(true);
            playerComplexes[0].SetActive(true);
        }
        else {
            _separationAmount = Mathf.Clamp(_separationAmount, 0f, separationEndTime);
            var lerp = Mathf.Clamp(_separationAmount, separationStartTime, separationEndTime);
            lerp -= separationStartTime;
            lerp /= separationEndTime - separationStartTime;
            cameraTransform.localPosition = Vector3.Lerp(
                    togetherCameraPosition.localPosition,
                    readyToSeparateCameraPosition.localPosition,
                    lerp);
            cameraTransform.rotation = Quaternion.Lerp(
                togetherCameraPosition.localRotation,
                readyToSeparateCameraPosition.localRotation, 
                lerp);
        }
    }

    private void FixedUpdate() {
        var input1 = joypad1.input;
        var input2 = joypad2.input;
        var input = input1 + input2;
        
        
        
        var directionForce = new Vector3(input.x, 0f, input.y);
        rb.AddForce(directionForce * force);
    }
    
    
}
