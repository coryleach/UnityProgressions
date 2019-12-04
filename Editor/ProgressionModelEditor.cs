using Gameframe.Progressions;
using UnityEngine;
using UnityEditor;

namespace Gameframe.Progressions
{
  [CustomEditor(typeof(CalculatedFloatProgressionModel),true)]
  public class ProgressionModelEditor : UnityEditor.Editor
  {
    private SerializedProperty curve;
    private SerializedProperty segments;
    private SerializedProperty maxLevel;
    private SerializedProperty scale;
    
    private void OnEnable()
    {
      curve = serializedObject.FindProperty("curve");
      segments = serializedObject.FindProperty("segments");
      maxLevel = serializedObject.FindProperty("maxLevel");
      scale = serializedObject.FindProperty("scale");
    }

    public override void OnInspectorGUI()
    {
      //base.OnInspectorGUI();

      serializedObject.Update();

      EditorGUILayout.PropertyField(maxLevel);
      EditorGUILayout.PropertyField(scale);
      
      int segmentCount = segments.arraySize;
      if (segmentCount < 1)
      {
        segmentCount = 1;
        segments.arraySize = segmentCount;
      }

      EditorGUI.BeginChangeCheck();
      segmentCount = EditorGUILayout.IntField("Segments", segmentCount);
      if (EditorGUI.EndChangeCheck())
      {
        segments.arraySize = segmentCount;
      }
      
      for (int i = 0; i < segments.arraySize; i++)
      {
        EditorGUILayout.BeginHorizontal();

        var segment = segments.GetArrayElementAtIndex(i);

        var exponent = segment.FindPropertyRelative("exponent");
        var multiplierA = segment.FindPropertyRelative("multiplierA");
        var multiplierB = segment.FindPropertyRelative("multiplierB");
        var constant = segment.FindPropertyRelative("constant");
        var minLevel = segment.FindPropertyRelative("minLevel");

        minLevel.intValue = Mathf.RoundToInt((maxLevel.intValue / (float)segmentCount)  * i);
        
        multiplierA.floatValue = EditorGUILayout.FloatField(multiplierA.floatValue);
        GUILayout.Label(" * X^");
        exponent.floatValue = EditorGUILayout.FloatField(exponent.floatValue);
        GUILayout.Label(" + ");
        multiplierB.floatValue = EditorGUILayout.FloatField(multiplierB.floatValue);
        GUILayout.Label(" * X + ");
        constant.floatValue = EditorGUILayout.FloatField(constant.floatValue);
        
        GUILayout.Label($" if X >= {minLevel.intValue}");
        
        EditorGUILayout.EndHorizontal();
      }
      
      if ( curve != null )
      {
        curve.animationCurveValue = EditorGUILayout.CurveField(curve.animationCurveValue, GUILayout.Height(200));
      }

      EditorGUILayout.BeginVertical("Box");

      var progressionModel = (FloatProgressionModel)target;
      for ( int i = 0; i < maxLevel.intValue; i++ )
      {
        EditorGUILayout.LabelField($"{i}: {progressionModel.Get(i)}");
      }

      EditorGUILayout.EndVertical();
      
      serializedObject.ApplyModifiedProperties();
    }

  }
}
