using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Chapter")]
public class Chapter : ScriptableObject
{
  [TextArea]
  public string descrition;

  public string chapterName;

  public Action[] actions;
}
