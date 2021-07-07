using Sirenix.OdinInspector;

public class ColoredTitleGroupAttribute : PropertyGroupAttribute
{
    public string Subtitle;
    public TitleAlignments Alignment;
    public bool HorizontalLine;
    public bool BoldTitle;
    public bool Indent;

    private string titleColor;
    public string TitleColor
    {
        get => titleColor;
        set
        {
            titleColor = value;
            HasDefinedTitleColor = true;
        }
    }

    private string subtitleColor;
    public string SubtitleColor
    {
        get => subtitleColor;
        set
        {
            subtitleColor = value;
            HasDefinedSubtitleColor = true;
        }
    }

    private string horizontalLineColor;
    public string HorizontalLineColor
    {
        get => horizontalLineColor;
        set
        {
            horizontalLineColor = value;
            HasDefinedHorizontalLineColor = true;
        }
    }

    public bool HasDefinedTitleColor;
    public bool HasDefinedSubtitleColor;
    public bool HasDefinedHorizontalLineColor;

    public ColoredTitleGroupAttribute(
        string title, string subtitle = null, TitleAlignments alignment = TitleAlignments.Left,
        bool horizontalLine = true, bool boldTitle = true, bool indent = false, float order = 0f)
        : base(title, order)
    {
        Subtitle = subtitle;
        Alignment = alignment;
        HorizontalLine = horizontalLine;
        BoldTitle = boldTitle;
        Indent = indent;
    }

    protected override void CombineValuesWith(PropertyGroupAttribute other)
    {
        var titleGroupAttribute = other as ColoredTitleGroupAttribute;

        if (Subtitle != null) titleGroupAttribute.Subtitle = Subtitle;
        else Subtitle = titleGroupAttribute.Subtitle;

        if (Alignment != 0) titleGroupAttribute.Alignment = Alignment;
        else Alignment = titleGroupAttribute.Alignment;

        if (!HorizontalLine) titleGroupAttribute.HorizontalLine = HorizontalLine;
        else HorizontalLine = titleGroupAttribute.HorizontalLine;

        if (!BoldTitle) titleGroupAttribute.BoldTitle = BoldTitle;
        else BoldTitle = titleGroupAttribute.BoldTitle;

        if (Indent) titleGroupAttribute.Indent = Indent;
        else Indent = titleGroupAttribute.Indent;

        if (HasDefinedTitleColor) titleGroupAttribute.TitleColor = TitleColor;
        else TitleColor = titleGroupAttribute.TitleColor;

        if (HasDefinedSubtitleColor) titleGroupAttribute.SubtitleColor = SubtitleColor;
        else SubtitleColor = titleGroupAttribute.SubtitleColor;

        if (HasDefinedHorizontalLineColor) titleGroupAttribute.HorizontalLineColor = HorizontalLineColor;
        else HorizontalLineColor = titleGroupAttribute.HorizontalLineColor;
    }
}
