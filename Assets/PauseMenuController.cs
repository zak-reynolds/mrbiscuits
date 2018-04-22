using UnityEngine;

public class PauseMenuController : MonoBehaviour {

    public bool paused = false;

    void Start()
    {
        transform.Find("Log").gameObject.SetActive(paused);
    }

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
	}

    private void TogglePause()
    {
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(paused);
        }
    }
}
