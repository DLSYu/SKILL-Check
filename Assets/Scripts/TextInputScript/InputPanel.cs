using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

[RequireComponent(typeof(RectTransform))]
public class InputPanel : MonoBehaviour
{
  public RectTransform rt;
  void Awake()
  {
    rt = GetComponent<RectTransform>();
  }


}