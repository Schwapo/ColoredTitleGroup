using UnityEngine;

public class ColoredTitleGroupAttributeExample : MonoBehaviour
{
    [ColoredTitleGroup("$GetTitle", Subtitle = "$GetSubtitle", TitleColor = "TitleColor", SubtitleColor = "SubtitleColor", HorizontalLineColor = "HorizontalLineColor")]
    public string someField;

    private string GetTitle => "My Awesome Title";
    private string GetSubtitle => "My Awesome Subtitle";

    private Color TitleColor => new Color(0.98f, 0.46f, 0.41f, 1f);
    private Color SubtitleColor => new Color(0.69f, 0.72f, 0.70f, 1f);
    private Color HorizontalLineColor => new Color(0.09f, 0.29f, 0.27f, 1f);
}