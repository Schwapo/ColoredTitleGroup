using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class ColoredTitleGroupAttributeDrawer : OdinGroupDrawer<ColoredTitleGroupAttribute>
{
    private ValueResolver<string> titleResolver;
    private ValueResolver<string> subtitleResolver;
    private ValueResolver<Color> titleColorResolver;
    private ValueResolver<Color> subtitleColorResolver;
    private ValueResolver<Color> horizontalLineColorResolver;

    protected override void Initialize()
    {
        titleResolver = ValueResolver.GetForString(Property, Attribute.GroupID);
        subtitleResolver = ValueResolver.GetForString(Property, Attribute.Subtitle);
        titleColorResolver = ValueResolver.Get<Color>(Property, Attribute.TitleColor);
        subtitleColorResolver = ValueResolver.Get<Color>(Property, Attribute.SubtitleColor);
        horizontalLineColorResolver = ValueResolver.Get<Color>(Property, Attribute.HorizontalLineColor);
    }

    protected override void DrawPropertyLayout(GUIContent label)
    {
        if (Property != Property.Tree.GetRootProperty(0))
        {
            EditorGUILayout.Space();
        }

        DrawTitle();

        GUIHelper.PushIndentLevel(EditorGUI.indentLevel + (Attribute.Indent ? 1 : 0));

        foreach (var child in Property.Children)
        {
            child.Draw(child.Label);
        }

        GUIHelper.PopIndentLevel();
    }

    private void DrawTitle()
    {
        var titleStyle = Attribute.Alignment switch
        {
            TitleAlignments.Left => Attribute.BoldTitle ? SirenixGUIStyles.BoldTitle : SirenixGUIStyles.Title,
            TitleAlignments.Centered => Attribute.BoldTitle ? SirenixGUIStyles.BoldTitleCentered : SirenixGUIStyles.TitleCentered,
            TitleAlignments.Right => Attribute.BoldTitle ? SirenixGUIStyles.BoldTitleRight : SirenixGUIStyles.TitleRight,
            _ => Attribute.BoldTitle ? SirenixGUIStyles.BoldTitle : SirenixGUIStyles.Title
        };

        var subtitleStyle = Attribute.Alignment switch
        {
            TitleAlignments.Left => SirenixGUIStyles.Subtitle,
            TitleAlignments.Centered => SirenixGUIStyles.SubtitleCentered,
            TitleAlignments.Right => SirenixGUIStyles.SubtitleRight,
            _ => SirenixGUIStyles.SubtitleRight
        };

        var title = titleResolver.GetValue();
        var subtitle = subtitleResolver.GetValue();

        if (Attribute.Alignment == TitleAlignments.Split)
        {
            var rect = GUILayoutUtility.GetRect(0f, 18f, titleStyle, GUILayoutOptions.ExpandWidth());

            GUI.Label(rect, title, titleStyle);
            GUI.Label(rect, subtitle, subtitleStyle);

            if (Attribute.HorizontalLine)
            {
                SirenixEditorGUI.HorizontalLineSeparator(SirenixGUIStyles.LightBorderColor);
                GUILayout.Space(3f);
            }

            return;
        }

        var titleRect = EditorGUI.IndentedRect(EditorGUILayout.GetControlRect(false, (GUILayoutOption[])(object)new GUILayoutOption[0]));

        if (Attribute.HasDefinedTitleColor)
        {
            GUIHelper.PushColor(titleColorResolver.GetValue());
            GUI.Label(titleRect, title, titleStyle);
            GUIHelper.PopColor();
        }
        else
        {
            GUI.Label(titleRect, title, titleStyle);
        }

        if (subtitle != null && !subtitle.IsNullOrWhitespace())
        {
            titleRect = EditorGUI.IndentedRect(GUILayoutUtility.GetRect(GUIHelper.TempContent(subtitle), subtitleStyle));

            if (Attribute.HasDefinedSubtitleColor)
            {
                GUIHelper.PushColor(subtitleColorResolver.GetValue());
                GUI.Label(titleRect, subtitle, subtitleStyle);
                GUIHelper.PopColor();
            }
            else
            {
                GUI.Label(titleRect, subtitle, subtitleStyle);
            }
        }

        if (Attribute.HorizontalLine)
        {
            var horizontalLineColor = SirenixGUIStyles.LightBorderColor;

            if (Attribute.HasDefinedHorizontalLineColor)
            {
                horizontalLineColor = horizontalLineColorResolver.GetValue();
            }

            SirenixEditorGUI.DrawSolidRect(titleRect.AlignBottom(1f), horizontalLineColor);
            GUILayout.Space(3f);
        }
    }
}
