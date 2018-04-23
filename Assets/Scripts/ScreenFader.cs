using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour {

    public float fadeTime = 0.4f;

    private Image image;
    private static ScreenFader _instance;

    void Awake() {
        image = GetComponent<Image>();
        _instance = this;
    }

    public static void FadeOut()
    {
        _instance.image.CrossFadeAlpha(1, _instance.fadeTime, true);
    }
    public static void FadeIn()
    {
        _instance.image.CrossFadeAlpha(0, _instance.fadeTime, true);
    }
    public static void FadeOutThen(Action action)
    {
        _instance.image.CrossFadeAlpha(1, _instance.fadeTime, true);
        _instance.StartCoroutine(_instance.WaitAndRunAction(_instance.fadeTime, action));
    }

    private IEnumerator WaitAndRunAction(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }
}
