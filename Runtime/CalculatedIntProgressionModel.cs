using UnityEngine;

namespace Gameframe.Progressions
{
  [CreateAssetMenu(menuName = "Gameframe/Progressions/Calculated/Int")]
  public class CalculatedIntProgressionModel : IntProgressionModel
  {
    [SerializeField]
    private int maxLevel = 100;

    [SerializeField]
    private float multiplier = 1f;

    [SerializeField]
    private float exponent = 1f;

    [SerializeField]
    private AnimationCurve curve = AnimationCurve.Linear(0, 0, 100, 100);

    public override int Count
    {
      get
      {
        return maxLevel;
      }
    }

    public override int Get(int index)
    {
      if (index < 0)
      {
        index = 0;
      }
      else if (index >= maxLevel)
      {
        index = maxLevel - 1;
      }
      return Mathf.FloorToInt(Mathf.Pow(index, exponent) * multiplier);
    }

    private void OnValidate()
    {
      Debug.Log("Validate");
      curve = new AnimationCurve();
      for (int i = 0; i < maxLevel; i++)
      {
        curve.AddKey(i, Get(i));
      }
    }
  }
}