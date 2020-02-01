using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public Vector3 targetPoint;
    public float targetOrthographicSize;
    public float transitionTimeSeconds = 1f;

    Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start() {
        StartCoroutine(DelayedCameraTransition());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player") {
            StartCoroutine(CameraTransitionCoroutine());
            Destroy(this.gameObject);
        }
    }

    private IEnumerator CameraTransitionCoroutine() {
        Vector3 startingPoint = mainCamera.transform.position;
        float startingOrthographicSize = mainCamera.orthographicSize;
        
        for (float f = 0f; f < transitionTimeSeconds; f += Time.deltaTime) {
            Vector3 currentPoint = Vector3.Lerp(startingPoint, targetPoint, f / transitionTimeSeconds);
            mainCamera.transform.SetPositionAndRotation(currentPoint, mainCamera.transform.rotation);

            float currentOrthographicSize = Mathf.Lerp(startingOrthographicSize, targetOrthographicSize, f / transitionTimeSeconds);
            mainCamera.orthographicSize = currentOrthographicSize;

            yield return null;
        }

        mainCamera.transform.SetPositionAndRotation(targetPoint, mainCamera.transform.rotation);
        mainCamera.orthographicSize = targetOrthographicSize;
    }

    private IEnumerator DelayedCameraTransition() {
        yield return new WaitForSeconds(2f);
        yield return CameraTransitionCoroutine();
    }
}
