using System.Collections;
using UnityEngine;

public class ScreenShaker : MonoBehaviour {

    private static ScreenShaker _instance;

	void Start () {
        _instance = this;
	}
	
	public static void Shake(float intensity, float length)
    {
        _instance.StartCoroutine(_instance.DoShake(intensity, length));
    }

    private IEnumerator DoShake(float intensity, float length)
    {
        var startTime = Time.time;
        while (startTime + length > Time.time)
        {
            transform.localPosition = new Vector3(
                Mathf.PerlinNoise(Time.time * 100 * intensity, 0),
                Mathf.PerlinNoise(Time.time * 100 * intensity, 1000)) * intensity;
            yield return null;
        }
        transform.localPosition = Vector3.zero;
    }
}
