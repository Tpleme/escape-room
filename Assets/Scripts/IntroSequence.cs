using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Audio;

public class IntroSequence : MonoBehaviour {
    public TextMeshProUGUI introText;
    public CanvasGroup blackScreenCanvasGroup;

    [TextArea(3, 5)]
    public string fullMessage = "Eles deixaram-te para trás. A cabeça pesa-te, os sentidos falham, mas o instinto de sobrevivência grita mais alto. Onde quer que estejas, este não é o teu lugar.\nSai daí.";
    public float letterDelay = 0.05f;
    public float pontuationDelay = 1.0f;
    public float textFadeDuration = 2.0f;
    public float blackScreenFadeDuration = 3.0f;

    public AudioSource doorSound;
    public AudioSource ambientMusic;
    public AudioSource atmosphericSound;

    public AudioMixer audioMixer;

    public float ambientMusicVolume = 0.3f;
    public float atmosphericSoundVolume = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        introText.text = "";
        introText.alpha = 1f;
        blackScreenCanvasGroup.alpha = 1f;

        StartCoroutine(StartIntro());
    }

    IEnumerator StartIntro() {

        float timer = 0;
        while (timer < 2f) {
            timer += Time.deltaTime;

            if (atmosphericSound != null) {
                atmosphericSound.Play();
                atmosphericSound.volume = Mathf.Lerp(0f, atmosphericSoundVolume, timer / 1f);
            }

            yield return null;
        }

        yield return new WaitForSeconds(2.0f);

        if (doorSound != null) doorSound.Play();

        yield return new WaitForSeconds(3.0f);

        audioMixer.SetFloat("Storm", 1000f);

        yield return new WaitForSeconds(2.0f);

        foreach (char letter in fullMessage.ToCharArray()) {
            introText.text += letter;

            float delay = letter == '.' ? pontuationDelay : letterDelay;

            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(2.0f);

        timer = 0;
        while (timer < textFadeDuration) {
            timer += Time.deltaTime;
            introText.alpha = Mathf.Lerp(1f, 0f, timer / textFadeDuration);

            yield return null;
        }

        introText.alpha = 0f;

        timer = 0;
        while (timer < blackScreenFadeDuration) {
            timer += Time.deltaTime;

            blackScreenCanvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / blackScreenFadeDuration);

            if (ambientMusic != null) {
                ambientMusic.Play();
                ambientMusic.volume = Mathf.Lerp(0f, ambientMusicVolume, timer / blackScreenFadeDuration);
            }

            yield return null;
        }

        gameObject.SetActive(false);

    }
}
