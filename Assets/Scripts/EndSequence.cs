using UnityEngine;
using TMPro;
using System.Collections;
using System;
using UnityEngine.Audio;

public class EndSequence : MonoBehaviour {
    public TextMeshProUGUI text;
    public CanvasGroup canvasGroup;

    [TextArea(3, 5)]
    public string fullMessage = "Conseguiste abrir a porta e escapar. Vez uma floresta a rodear a casa.\nComeças a correr rapidamente sem olhar para trás.\n\nEscapaste em:";
    public float letterDelay = 0.05f;
    public float pontuationDelay = 1.0f;
    public float fadeDuration = 3.0f;
    public float ambienteSoundVolume = 0.3f;
    public float atmosphericSoundVolume = 0.3f;

    public AudioSource atmosphericSound;
    public AudioSource ambienteSound;

    public AudioMixer audioMixer;

    //timer related
    private float startTime;

    void Start() {
        startTime = Time.time;
    }

    public string GetFormattedTime() {
        float timeElapsed = Time.time - startTime;

        //format time
        TimeSpan t = TimeSpan.FromSeconds(timeElapsed);
        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void InitEndScreen() {
        text.text = "";

        StartCoroutine(StartEndScreen());
    }

    IEnumerator StartEndScreen() {
        string timeElapsed = GetFormattedTime();


        yield return new WaitForSeconds(2.0f);

        audioMixer.SetFloat("Storm", 15000f);

        yield return new WaitForSeconds(1.0f);

        
        float timer = 0;
        while (timer < fadeDuration) {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);

            if (ambienteSound != null) {
                ambienteSound.volume = Mathf.Lerp(ambienteSoundVolume, 0f, timer / fadeDuration);
            }

            yield return null;
        }

        foreach (char letter in fullMessage.ToCharArray()) {
            text.text += letter;

            float delay = letter == '.' ? pontuationDelay : letterDelay;

            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(1.0f);

        text.text += $" <color=#FFD700>{timeElapsed}</color>";

    }
}