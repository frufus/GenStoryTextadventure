using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Chapter %&c")]
public class Chapter : ScriptableObject
{
  public string chapterName;
  public int chapterNumber;
  public string id;
  [TextArea]
  public string descrition;


  public Action[] actions;


  public string[] reqFlags;
  public List<Flag> denFlags;
  public List<Flag> trigFlags;
  public List<Flag> remFlags;

}
