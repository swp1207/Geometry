using UnityEditor;

[CustomEditor(typeof(Portal)), CanEditMultipleObjects]
public class PortalEditor : Editor
{
    public enum DisplayCategory
    {
        GameMode, Speed, Gravity
    }
    public DisplayCategory categoryToDisplay;

    bool FirstTime = true;

    public override void OnInspectorGUI()
    {
        if (FirstTime)
        {
            switch (serializedObject.FindProperty("State").intValue)
            {
                case 0:
                    categoryToDisplay = DisplayCategory.GameMode;
                    break;
                case 1:
                    categoryToDisplay = DisplayCategory.Speed;
                    break;
                case 2:
                    categoryToDisplay = DisplayCategory.Gravity;
                    break;
            }
        }
        else
            categoryToDisplay = (DisplayCategory)EditorGUILayout.EnumPopup("Display", categoryToDisplay);

        EditorGUILayout.Space();

        switch (categoryToDisplay)
        {
            case DisplayCategory.GameMode:
                DisplayProperty("gameMode", 0);
                break;

            case DisplayCategory.Speed:
                DisplayProperty("speed", 1);
                break;

            case DisplayCategory.Gravity:
                DisplayProperty("gravity", 2);
                break;
        }

        FirstTime = false;
        serializedObject.ApplyModifiedProperties();
    }

    void DisplayProperty(string property, int PropNumb)
    {
        try
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(property));
        }
        catch
        { }
        serializedObject.FindProperty("State").intValue = PropNumb;
    }
}