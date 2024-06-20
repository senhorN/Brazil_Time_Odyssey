using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TypeTextAnimation : MonoBehaviour
{
    public Action TypeFinished;

    public float typeDelay;
    public TextMeshProUGUI textObject;
    public string fullText;

    private Coroutine coroutine;

    public void StartTyping()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        textObject.text = fullText;
        textObject.maxVisibleCharacters = 0;

        for (int i = 0; i <= textObject.text.Length; i++)
        {
            textObject.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typeDelay);
        }

        TypeFinished?.Invoke();
    }

    public void SkipTyping()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            textObject.text = fullText;
            textObject.maxVisibleCharacters = fullText.Length;
            TypeFinished?.Invoke();
        }
    }
}
