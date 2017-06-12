using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChapterNavigation : MonoBehaviour
{
  [HideInInspector] public Chapter currentChapter;
  public Dictionary<string, Flag> activeFlags = new Dictionary<string, Flag>();
  public Dictionary<int, List<Chapter>> chapterLib = new Dictionary<int, List<Chapter>>();
  public int chapterCounter;
  private GameController controller;
  private Dictionary<string, Action> actionDic = new Dictionary<string, Action>();


  private void Awake()
  {
    controller = GetComponent<GameController>();

    //initial array for all chapters
    for (int i = 1; i <= 12; i++)
    {
      chapterLib[i] = new List<Chapter>();
    }

    // sort all chapters by their chapterNumber
    foreach (var chapter in controller.allChapters)
    {
      chapterLib[chapter.chapterNumber].Add(chapter);
    }

    List<Chapter> chapterList = chapterLib[1];

    currentChapter = GetChapterWithFlags(chapterList);
  }


  public void UnpackChapter()
  {
    
    chapterCounter = currentChapter.chapterNumber;

    // add new flags from the chapter
    AddTriggeredFlags(currentChapter.trigFlags);

    // remove flags which this chapter unsets
    RemoveFlags(currentChapter.remFlags);
    
    // Log the generall description of the chapter
    controller.LogStringWithReturn(ReplacePlaceholderFlags(currentChapter.descrition));

    // Log the text for the new flags
    Flag[] trigFlags = currentChapter.trigFlags.ToArray();
    if (trigFlags != null && trigFlags.Length > 0)
    {
      for (int j = 0; j < trigFlags.Length; j++)
      {
        if (trigFlags[j] != null && trigFlags[j].triggerText != "")
          controller.LogStringWithReturn(trigFlags[j].triggerText);
      }
    }


    // Log the text for the removed flags
    Flag[] remFlags = currentChapter.remFlags.ToArray();

    if (remFlags != null && remFlags.Length > 0)
    {
      for (int k = 0; k < remFlags.Length; k++)
      {
        if (remFlags[k] != null && remFlags[k].unsetText != "")
          controller.LogStringWithReturn(remFlags[k].unsetText);
      }
    }


    // Log the text for avaliable actions
    if (currentChapter.actions.Length > 0)
    {
      for (int i = 0; i < currentChapter.actions.Length; i++)
      {
        actionDic.Add(currentChapter.actions[i].keyString, currentChapter.actions[i]);
        controller.actionsInChapter.Add(currentChapter.actions[i].actionDescription);
        controller.LogActionWithReturn(currentChapter.actions[i].actionDescription);
      }
    }
  }

  public void DisplayChangedChapter()
  {
    if (currentChapter != null)
    {
      controller.LogStringWithReturn("Kapitel " + currentChapter.chapterNumber + ": " + currentChapter.chapterName);
      controller.DisplayChapterText();
    }
    else
    {
      controller.LogStringWithReturn("Das kannst du nicht tun.");
    }
  }

  public bool HasAction(string action)
  {
    return actionDic.ContainsKey(action);
  }

  public void ChangeToNextChapter()
  {
    chapterCounter++;
    List<Chapter> chapterList = chapterLib[chapterCounter];

    currentChapter = GetChapterWithFlags(chapterList);
    DisplayChangedChapter();
  }

  public Chapter GetChapterWithFlags(List<Chapter> chapters)
  {
    if (chapters.Count > 0)
    {
      Chapter canidate = chapters[Random.Range(0, chapters.Count)];

      // check if all required types of flags are set 
      if (canidate.reqFlags != null && canidate.reqFlags.Length > 0)
      {
        foreach (var type in canidate.reqFlags)
        {
          bool contains = false;
          foreach (var flag in activeFlags)
          {
            if (flag.Key == type && flag.Value != null)
            {
              contains = true;
            }
          }
          if (!contains)
          {
            chapters.Remove(canidate);
            canidate = GetChapterWithFlags(chapters);
          }
        }
      }

      // check if no dening flag is set
      canidate.denFlags = canidate.denFlags.Where(item => item != null).ToList();
      if (canidate.denFlags != null && canidate.denFlags.Any())
      {
        foreach (var flag in canidate.denFlags)
        {
          foreach (var aFlag in activeFlags)
          {
            if (aFlag.Value == flag)
            {
              chapters.Remove(canidate);
              canidate = GetChapterWithFlags(chapters);
            }
          }
        }
      }

      return canidate;
    }
    return null;
  }

  public string ReplacePlaceholderFlags(string text)
  {
    if (text.Contains("#"))
    {
      string[] textArray = text.Split(' ');
      for (int i = 0; i < textArray.Length; i++)
      {
        string word = textArray[i];
        if (word.Contains("#"))
        {
          string type = word.Substring(1, word.Length - 1);;
          Debug.Log(type);
          if (activeFlags.ContainsKey(type))
          {
            textArray[i] = activeFlags[type].displayName;
          }
        }
      }
      text = string.Join(" ", textArray);
    }

    return text;
  }

  public void AddAction(Action action, string keyString, string description)
  {
    actionDic.Add(keyString, action);
    controller.actionsInChapter.Add(description);
  }

  public void AddTriggeredFlags(List<Flag> flags)
  {
    foreach (var flag in flags)
    {
      activeFlags[flag.type] = flag;
    }
  }

  public void RemoveFlags(List<Flag> flags)
  {
    foreach (var flag in flags)
    {
      activeFlags[flag.type] = null;
    }
  }

  public void ClearActions()
  {
    actionDic.Clear();
  }
}
