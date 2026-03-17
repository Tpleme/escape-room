using System;
using TMPro;
using UnityEngine;

public class KeypadManager : MonoBehaviour {
    public TextMeshProUGUI codeText;
    public Animator paintingAnimator;
    public AudioSource paintingAudioSource;
    public string correctCode = "9726";

    string[] code = { "-", "-", "-", "-" };

    int digitsTyped = 0;

    public void PressKey(string key) {
        switch (key) {
            case "clear":
                OnClear();
                break;
            case "enter":
                OnEnter();
                break;
            default:
                OnDigit(key);
                break;
        }
    }

    private void OnClear() {
        digitsTyped = 0;
        code = new string[] { "-", "-", "-", "-" };
        codeText.text = String.Join("", code);
        //digit sound
    }

    private void OnEnter() {
        Debug.Log(String.Join("", code) == correctCode);
        Debug.Log(String.Join("", code));
        Debug.Log(correctCode);

        if (String.Join("", code) == correctCode) {

            if (paintingAnimator != null) {
                paintingAnimator.SetTrigger("slide");

                //success sound

                if (paintingAudioSource != null) {
                    paintingAudioSource.Play();
                }
            }

        }
        else {
            OnClear();
            //error sound
        }
    }

    private void OnDigit(string key) {
        if (digitsTyped >= 4) return;

        code[digitsTyped] = key;

        digitsTyped++;

        codeText.text = String.Join("", code);

        //digit sound
    }


}
