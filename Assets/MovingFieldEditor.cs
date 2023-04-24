#if UNITY_ANDROID && !UNITY_EDITOR
#else
using UnityEditor;

[CustomEditor(typeof(MovingField))]
public class MovingFieldEditor : Editor
{
    override public void OnInspectorGUI()
    {
        serializedObject.Update();
        var movingField = target as MovingField;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("moveType"));

        switch (movingField.moveType)
        {
            case MovingField.MoveType.position:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("positionParams"));
                break;
            case MovingField.MoveType.rotation:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationParams"));
                break;
            case MovingField.MoveType.both:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("positionParams"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationParams"));
                break;
        }

        //using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(movingField.hideBool)))
        //{
        //    if (group.visible == false)
        //    {
        //        EditorGUI.indentLevel++;
        //        EditorGUILayout.PrefixLabel("Color");
        //        movingField.someColor = EditorGUILayout.ColorField(movingField.someColor);
        //        EditorGUILayout.PrefixLabel("Text");
        //        movingField.someString = EditorGUILayout.TextField(movingField.someString);
        //        EditorGUILayout.PrefixLabel("Number");
        //        movingField.someNumber = EditorGUILayout.IntSlider(movingField.someNumber, 0, 10);
        //        EditorGUI.indentLevel--;
        //    }
        //}

        //myScript.disableBool = GUILayout.Toggle(myScript.disableBool, "Disable Fields");

        //using (new EditorGUI.DisabledScope(myScript.disableBool))
        //{
        //    myScript.someColor = EditorGUILayout.ColorField("Color", myScript.someColor);
        //    myScript.someString = EditorGUILayout.TextField("Text", myScript.someString);
        //    myScript.someNumber = EditorGUILayout.IntField("Number", myScript.someNumber);
        //}
        serializedObject.ApplyModifiedProperties();
    }
}
#endif