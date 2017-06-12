using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action
{

  public string keyString;
  public string actionDescription;

  public List<Flag> flagsToSet;
  public List<Flag> flagsToUnset;

}
