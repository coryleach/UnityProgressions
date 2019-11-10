using UnityEngine;

namespace Gameframe.Progressions
{
  [CreateAssetMenu(menuName ="Gameframe/Progressions/Calculated/Float")]
  public class CalculatedFloatProgressionModel : FloatProgressionModel
  {
    [SerializeField]
    private float startValue = 0;

    [System.Serializable]
    public class ProgressionSegment
    {
      [SerializeField]
      private float exponent = 1;

      [SerializeField]
      private float multiplier = 1;

      public float Get(int level)
      {
        return multiplier * Mathf.Pow(level, exponent);
      }
    }

    [SerializeField]
    private ProgressionSegment[] segments;

    [SerializeField, HideInInspector]
    private AnimationCurve curve;

    public override int Count => int.MaxValue;

    public override float Get(int level)
    {
      if ( segments == null )
      {
        return 0;
      }

      float sum = 0;
      for ( int i = 0; i < segments.Length; i++ )
      {
        sum += segments[i].Get(level);
      }
      return Mathf.Round(startValue + sum);
   }

    private void OnValidate()
    {
      curve = new AnimationCurve();
      for ( int i = 0; i < 100; i++ )
      {
        curve.AddKey(i, Get(i));
      }
    }

  }
}