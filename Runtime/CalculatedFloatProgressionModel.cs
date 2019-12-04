using UnityEditor;
using UnityEngine;

namespace Gameframe.Progressions
{
  
  [System.Serializable]
  public class ProgressionSegment
  {
    //Formula is : A * x^2 + B * X + C
    public float exponent = 2;
    public float multiplierA = 1;
    public float multiplierB = 1f;
    public float constant = 0;
    public int minLevel = 0;    
    
    public float Get(int level)
    {
      level -= minLevel;
      return multiplierA * Mathf.Pow(level, exponent) + multiplierB*level + constant;
    }
  }

  [CreateAssetMenu(menuName ="Gameframe/Progressions/Calculated/Float")]
  public class CalculatedFloatProgressionModel : FloatProgressionModel
  {
    [SerializeField] 
    private int maxLevel = 100;

    [SerializeField] 
    private float scale = 1.0f;
    
    [SerializeField]
    private ProgressionSegment[] segments = new ProgressionSegment[0];

    [SerializeField, HideInInspector]
    private AnimationCurve curve;

    public override int Count => int.MaxValue;

    public override float Get(int level)
    {
      if ( segments == null || segments.Length == 0)
      {
        return 0;
      }
      return segments[GetSegmentIndex(level)].Get(level) * scale;
    }

    private int GetSegmentIndex(int level)
    {
      int index = 0;
      for (; index+1 < segments.Length; index++)
      {
        var nextIndex = index + 1;
        if (level < segments[nextIndex].minLevel )
        {
          return index;
        }
      }
      return index;
    }

    #if UNITY_EDITOR
    private void OnValidate()
    {
      for (int i = 1; i < segments.Length; i++)
      {
        //Make sure each segment increases the min level
        if (segments[i].minLevel <= segments[i-1].minLevel)
        {
          segments[i].minLevel = segments[i].minLevel + 1;
        }
        //Constant of each segment should be the final value of the previous segment
        segments[i].constant = segments[i - 1].Get(segments[i].minLevel);
      }
      
      curve = new AnimationCurve();
      
      for ( int i = 0; i < maxLevel; i++ )
      {
        curve.AddKey(i, Get(i));
      }

      for ( int i = 0; i < maxLevel; i++ )
      {
        AnimationUtility.SetKeyLeftTangentMode(curve, i, AnimationUtility.TangentMode.Linear);
        AnimationUtility.SetKeyRightTangentMode(curve, i, AnimationUtility.TangentMode.Linear);
      }
      
    }
    #endif
    
  }
}