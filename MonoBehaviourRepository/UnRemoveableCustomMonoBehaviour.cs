using UnityEngine;

/// <inheritdoc />
// ReSharper disable once CheckNamespace
public class UnRemoveableCustomMonoBehaviour : MonoBehaviour
{
  public void Start()
  {
    gameObject.GetComponent<Piece>().m_canBeRemoved = false;
  }
}