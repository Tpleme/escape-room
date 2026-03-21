using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class KeypadManager : MonoBehaviour {
	public TextMeshProUGUI codeText;
	public Animator paintingAnimator;
	public AudioSource paintingAudioSource;
	public AudioSource keyTypeAudioSource;
	public AudioSource successAudioSource;
	public AudioSource errorAudioSource;
	public string correctCode = "9726";

	private bool correctCodeEntered = false;

	string[] code = { "-", "-", "-", "-" };

	int digitsTyped = 0;

	public void PressKey(string key) {
		Debug.Log(key);
		if(correctCodeEntered) return;

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

		if (keyTypeAudioSource != null) {
			keyTypeAudioSource.Play();
		}
	}

	private void OnEnter() {
		if (String.Join("", code) == correctCode) {
			correctCodeEntered = true;
			codeText.text = "OK";

			if (successAudioSource != null) {
				successAudioSource.Play();
			}

			if (paintingAnimator != null) {
				paintingAnimator.SetTrigger("slide");

				if (paintingAudioSource != null) {
					paintingAudioSource.Play();
				}
			}

		}
		else {
			if (errorAudioSource != null) {
				errorAudioSource.Play();
			}

			StartCoroutine(OnError());
		}
	}

	private IEnumerator OnError() {
		codeText.text = "ERR";

		yield return new WaitForSeconds(2.0f);

		code = new string[] { "-", "-", "-", "-" };

		codeText.text = String.Join("", code);

		digitsTyped = 0;
	}

	private void OnDigit(string key) {
		if (digitsTyped >= 4) return;

		code[digitsTyped] = key;

		digitsTyped++;

		codeText.text = String.Join("", code);

		if (keyTypeAudioSource != null) {
			keyTypeAudioSource.Play();
		}
	}


}
