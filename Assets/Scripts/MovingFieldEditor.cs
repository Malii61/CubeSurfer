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

        serializedObject.ApplyModifiedProperties();
    }
}
#endif