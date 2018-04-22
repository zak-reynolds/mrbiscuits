using System.Collections;
using UnityEngine;

public class BlendshapeAnim : MonoBehaviour {

    public float animspeed = 1.5f;
    public float frames = 2;
    private SkinnedMeshRenderer smr;
    private float weight = 0;
    private int frame = 0;
    private float starttime;

    void Start () {
        smr = GetComponent<SkinnedMeshRenderer>();
        StartCoroutine(UpdateFrame());
	}
	
	void Update () {
        weight = (1 - (Mathf.Cos((Time.time - starttime) * animspeed * 2 * Mathf.PI) / 2 + 0.5f)) * 100;
        smr.SetBlendShapeWeight(frame, weight);
	}

    private IEnumerator UpdateFrame()
    {
        starttime = Time.time;
        while (true)
        {
            yield return new WaitForSeconds(1f / animspeed);
            starttime = Time.time;
            frame++;
            if (frame == frames) frame = 0;
            for (int i = 0; i < frames; ++i)
            {
                smr.SetBlendShapeWeight(i, 0);
            }
        }
    }
}
