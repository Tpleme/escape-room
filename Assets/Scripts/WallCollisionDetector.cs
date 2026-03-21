using UnityEngine;
using System.Collections;

public class WallCollisionDetector : MonoBehaviour {

    public CanvasGroup fadeGroup;
    public float fadeSpeed = 5f;

    public string wallTag = "Wall";

    private int wallsIntersecting = 0;

    void Update() {
        float targetAlpha = (wallsIntersecting > 0) ? 1.0f : 0.0f;

        if (!Mathf.Approximately(fadeGroup.alpha, targetAlpha)) {
            fadeGroup.alpha = Mathf.MoveTowards(fadeGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(wallTag)) {
            wallsIntersecting++;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag(wallTag)) {
            wallsIntersecting = Mathf.Max(0, wallsIntersecting - 1);
        }
    }
}
