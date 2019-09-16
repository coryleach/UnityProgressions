using UnityEngine;

namespace Gameframe.Progressions
{
  [CreateAssetMenu(menuName = "GameJam/Progressions/Calculated/Int")]
  public class CalculatedIntProgressionModel : IntProgressionModel
  {
    [SerializeField]
    int maxLevel = 100;

    [SerializeField]
    float multiplier = 1f;

    [SerializeField]
    float exponent = 1f;

    [SerializeField]
    AnimationCurve curve = AnimationCurve.Linear(0, 0, 100, 100);

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

    void OnValidate()
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