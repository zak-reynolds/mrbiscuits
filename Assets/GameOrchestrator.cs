using UnityEngine;

public class GameOrchestrator : MonoBehaviour {

	void Start () {
        MessageController.AddMessage("Look at it");
        MessageController.AddMessage("It's so thirsty");
        MessageController.AddMessage("Maybe you can help");
        MessageController.AddMessage("and find it a drink");
    }
}
