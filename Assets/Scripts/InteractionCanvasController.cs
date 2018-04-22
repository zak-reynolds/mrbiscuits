using UnityEngine;
using UnityEngine.UI;

public class InteractionCanvasController : MonoBehaviour {

    public enum Interaction { None, Take, Feed, Eat }
    public Sprite take;
    public Sprite feed;
    public Sprite eat;

    private Image image;

    void SetInteraction(Interaction interaction)
    {
        switch(interaction)
        {
            case Interaction.None:
                image.enabled = false;
                break;
            case Interaction.Take:
                image.enabled = true;
                image.sprite = take;
                break;
            case Interaction.Feed:
                image.enabled = true;
                image.sprite = feed;
                break;
            case Interaction.Eat:
                image.enabled = true;
                image.sprite = eat;
                break;
        }
    }

    void Start()
    {
        image = transform.GetChild(0).gameObject.GetComponent<Image>();
    }
}
