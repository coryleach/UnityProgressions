using UnityEngine;

namespace Gameframe.Progressions
{
  [CreateAssetMenu(menuName ="Gameframe/Progressions/Curve")]
  public class CurveProgressionModel : FloatProgressionModel
  {
    [SerializeField]
    private AnimationCurve curve;

    public override int Count
    {
      get {
        if ( curve == null || curve.length < 1 )
        {
          return 0;
        }
        return (int)curve.keys[curve.length-1].time;
      }
    }

    public override float Get(int index)
    {
      return curve.Evaluate(index);
    }
  }
}
