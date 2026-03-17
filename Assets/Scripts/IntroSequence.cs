using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class IntroSequence : MonoBehaviour {
    public TextMeshProUGUI introText;
    public Image blackScreen;
    
    [TextArea(3,5)]
    public string fullMessage = "Eles deixaram-te para trás. A cabeça pesa-te, os sentidos falham, mas o instinto de sobrevivência grita mais alto. Onde quer que estejas, este não é o teu lugar.\nSai daí.";
    public float letterDelay = 0.05f;
    public float pontuationDelay = 1.0f;
    public float textFadeDuration = 2.0f;
    public float imageFadeDuration = 3.0f;

    [Header("Audio")]
    public AudioSource doorSound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        introText.text = "";
        introText.alpha = 1f;
        
        Color tempColor = blackScreen.color;
        tempColor.a = 1f;
        blackScreen.color = tempColor;
        
        StartCoroutine(StartIntro());
    }

    IEnumerator StartIntro() {
        yield return new WaitForSeconds(2.0f);

        if(doorSound != null) doorSound.Play();

        yield return new WaitForSeconds(4.0f);

        foreach (char letter in fullMessage.ToCharArray()) {
            introText.text += letter;
            
            float delay = letter == '.' ? pontuationDelay : letterDelay;

            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(2.0f);

        float timer = 0;
        while (timer < textFadeDuration)
        {
            timer += Time.deltaTime;
            introText.alpha = Mathf.Lerp(1f, 0f, timer / textFadeDuration);
            yield return null;
        }
        introText.alpha = 0f;

        timer = 0;
        while (timer < imageFadeDuration)
        {
            timer += Time.deltaTime;
            Color c = blackScreen.color;
            c.a = Mathf.Lerp(1f, 0f, timer / imageFadeDuration);
            blackScreen.color = c;
            yield return null;
        }

        gameObject.SetActive(false);

    }
}
