using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyData))]
public class FieldTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EnemyData instance = target as EnemyData;

        //// UseSetColor が True のときのみ、[ObjectColor]のフィールド変数を表示する
        if (instance.IsStraight)
        {
            instance.Direction = EditorGUILayout.Vector2Field("ObjectColor", instance.Direction);
        }
    }
}