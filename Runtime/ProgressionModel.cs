using UnityEngine;

namespace Gameframe.Progressions
{
  public abstract class ProgressionModel<T> : ScriptableObject, IProgressionModel<T> where T : struct
  {
    public abstract int Count { get; }
    public abstract T Get(int index);
  }
}