using System;

namespace AllRecalls
{
  /// <summary>
  /// Strongly-types WeakReference class
  /// </summary>
  /// <typeparam name="T">The type of item stored by this weak reference</typeparam>
  /// <remarks>
  /// NOTE: If you try and derive from the system's WeakReference, it will compile OK but your
  /// app will crash because the constructor is [SecuritySafeCritical]
  /// </remarks>
  public class WeakReference<T>
  {
    /// <summary>
    /// The actual reference
    /// </summary>
    WeakReference reference;

    /// <summary>
    /// Create a new weak reference with the specified object
    /// </summary>
    /// <param name="target">The object to track as a weak reference</param>
    public WeakReference(T target)
    {
      reference = new WeakReference(target);
    }

    /// <summary>
    /// Whether or not the item is alive
    /// </summary>
    public bool IsAlive
    {
      get
      {
        return reference.IsAlive;
      }
    }

    /// <summary>
    /// The item being tracked, or null if it is no longer alive
    /// </summary>
    public T Target
    {
      get
      {
        return (T)reference.Target;
      }
      set
      {
        reference.Target = value;
      }
    }
  }
}
