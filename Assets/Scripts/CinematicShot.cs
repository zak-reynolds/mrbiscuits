using System.Collections;
using UnityEngine;

public class CinematicShot : MonoBehaviour {

    private static CinematicShot _instance;

	void Start () {
        _instance = this;
	}

    public static void MoveToTarget(Transform target, float time, float hold, bool changePerspective = true)
    {
        _instance.StartCoroutine(_instance.Move(target, time, hold, changePerspective));
    }

    private IEnumerator Move(Transform target, float time, float hold, bool changePerspective = true)
    {
        float starttime = Time.time;
        Vector3 currentVelocity = Vector3.zero;
        if (changePerspective)
        {
            PerspectiveShifter.ShiftToPerspective(time / 2);
        }
        var pm = GameObject.Find("PlayerMesh");
        pm.SetActive(false);
        while (Time.time < starttime + time)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref currentVelocity, time - (Time.time - starttime));
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, 0.5f);
            yield return null;
        }
        while (Time.time < starttime + time + hold) {
            transform.position = target.position;
            transform.rotation = target.rotation;
            yield return null;
        }
        if (changePerspective)
        {
            PerspectiveShifter.ShiftToOrtho(time / 2);
        }
        while (Time.time < starttime + time + time + hold)
        {
            transform.position = Vector3.SmoothDamp(transform.position, transform.parent.position, ref currentVelocity, time - (Time.time - starttime));
            transform.rotation = Quaternion.Slerp(transform.rotation, transform.parent.rotation, 0.5f);
            yield return null;
        }
        pm.SetActive(true);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
