using System.Collections.Generic;
using UnityEngine;

namespace Gameframe.Progressions
{
  [CreateAssetMenu(menuName = "GameJam/Progressions/Serialized/Int")]
  public class SerializedIntProgressionModel : IntProgressionModel
  {
    [SerializeField]
    List<int> progression = new List<int>();

    public override int Count => progression.Count;

    public override int Get(int index)
    {
      if (index < 0)
      {
        index = 0;
      }
      else if (index >= progression.Count)
      {
        index = progression.Count - 1;
      }
      return progression[index];
    }
  }
}