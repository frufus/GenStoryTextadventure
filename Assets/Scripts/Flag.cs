using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Flag")]
public class Flag : ScriptableObject
{

  public string id;
  public string displayName;
  [TextArea]
  public string triggerText;
  [TextArea]
  public string unsetText;
  [TextArea] 
  public string description;

  public string type;

}