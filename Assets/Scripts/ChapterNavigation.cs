using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterNavigation : MonoBehaviour
{

  public Chapter currentChapter;

  private GameController controller;
  private Dictionary<string, Chapter> actionDic = new Dictionary<string, Chapter>();

  private void Awake()
  {
    controller = GetComponent<GameController>();
  }


  public void UnpackActionsInChapter()
  {
    for (int i = 0; i < currentChapter.actions.Length; i++)
    {
      actionDic.Add(currentChapter.actions[i].keyString, currentChapter.actions[i].valueChapter);
      controller.actionsInChapter.Add(currentChapter.actions[i].actionDescription); 
    }
  }

  private void AttemptToChangeChapter(string action)
  {
    if (actionDic.ContainsKey(action))
    {
      currentChapter = actionDic[action];
      controller.LogStringWithReturn("Nächstes Kapitel: " + action);
      controller.DisplayChapterText();
    }
    else
    {
      controller.LogStringWithReturn("Das kannst du nicht tun.");
    }
  }

  public void ClearActions()
  {
    actionDic.Clear();
  }
}
