using Gameframe.Progressions;
using UnityEngine;
using UnityEditor;

namespace GameJam.Progressions
{
  [CustomEditor(typeof(FloatProgressionModel),true)]
  public class ProgressionModelEditor : UnityEditor.Editor
  {

    int min = 0;
    int max = 99;

    SerializedProperty curve;

    private void OnEnable()
    {
      curve = serializedObject.FindProperty("curve");
    }

    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      serializedObject.Update();

      if ( curve != null )
      {
        curve.animationCurveValue = EditorGUILayout.CurveField(curve.animationCurveValue, GUILayout.Height(200));
      }

      min = EditorGUILayout.IntField(min);
      max = EditorGUILayout.IntField(max);

      var progressionModel = target as FloatProgressionModel;
      for ( int i = min; i < max; i++ )
      {
        EditorGUILayout.LabelField(string.Format("{0}:{1}",i,progressionModel.Get(i)));
      }

      serializedObject.ApplyModifiedProperties();
    }

  }
}
