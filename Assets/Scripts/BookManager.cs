using UnityEngine;

public class BookManager : MonoBehaviour {
    [Header("Configurações")]
    private int totalBooksNedded = 7;
    public string bookTag = "Books";

    public Animator secretDrawerAnimator;
    public AudioSource drawerAudioSource;
    public string animationTriggerName = "open";

    private int currentBooksInPlace = 0;
    private bool puzzleSolved = false;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(bookTag)) {
            currentBooksInPlace++;
            CheckPuzzle();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag(bookTag)) {
            currentBooksInPlace--;
        }
    }

    private void CheckPuzzle() {

        if (currentBooksInPlace >= totalBooksNedded && !puzzleSolved) {
            puzzleSolved = true;
            Debug.Log("Puzzle Solved");

            if (secretDrawerAnimator != null) {
                secretDrawerAnimator.SetTrigger(animationTriggerName);

                if (drawerAudioSource != null) {
                    drawerAudioSource.Play();
                }
            }
        }
    }
}
