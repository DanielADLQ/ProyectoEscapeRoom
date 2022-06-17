using UnityEngine;

public class NextButtonScript : MonoBehaviour
{
  private InkManager _inkManager;

  void Start()
  {
    _inkManager = FindObjectOfType<InkManager>();
  }

  public void OnClick()
  {
    _inkManager?.DisplayNextLine();
  }
}