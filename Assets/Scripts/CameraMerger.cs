using System;
using UnityEngine;
public class CameraMerger : MonoBehaviour {
    public CameraPlayerSetup[] setups;
    [Serializable]
    public struct CameraPlayerSetup {
        public Transform player;
        public Transform cameraTransform;
        public Camera camera;
        public Transform togetherView;
        public Transform separateView;
        public Follower follower;
    }
    public float mergeStartDistance = 10f;
    public float mergeEndDistance = 1f;
    public float togetherFov = 30f;
    public float individualFov = 60f;
    private void Update() {
        var distance = Vector3.Distance(setups[0].player.position, setups[1].player.position);
        var a = Mathf.Clamp(distance, mergeEndDistance, mergeStartDistance);
        var lerpAmount = (a - mergeEndDistance) / (mergeStartDistance - mergeEndDistance);
        for (int i = 0; i < 2; i++) {
            var setup = setups[i];
            setup.cameraTransform.localPosition = Vector3.Lerp(setup.togetherView.localPosition, setup.separateView.localPosition, lerpAmount);
            setup.cameraTransform.localRotation = Quaternion.Lerp(setup.togetherView.localRotation, setup.separateView.localRotation, lerpAmount);
            setup.camera.fieldOfView = Mathf.Lerp(togetherFov, individualFov, lerpAmount);
            setup.follower.lerp = lerpAmount;
        }
    }
}
