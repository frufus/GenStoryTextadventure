using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

  public Text displayText;
  [HideInInspector] public ChapterNavigation chapterNavigation;
  [HideInInspector] public List<string> actionsInChapter = new List<string>();
  
  List<string> actionLog = new List<string>();
	
  // Use this for initialization
  private void Awake()
  {
    chapterNavigation = GetComponent<ChapterNavigation>();
  }

  private void Start()
  {
    DisplayChapterText();
    DisplayLoggedText();
  }


  public void DisplayChapterText()
  {
    ClearCollectionsForNewChapter();
    UnpackChapter();

    string joindActionDescriptions = string.Join("\n", actionsInChapter.ToArray());
    string combinedText = chapterNavigation.currentChapter.descrition + "\n" + joindActionDescriptions;
    
    LogStringWithReturn(combinedText);
  }

  public void LogStringWithReturn(string stringToAdd)
  {
    actionLog.Add(stringToAdd + "\n");
  }

  public void DisplayLoggedText()
  {
    string logsAsText = string.Join("\n", actionLog.ToArray());

    displayText.text = logsAsText;
  }

  private void UnpackChapter()
  {
    chapterNavigation.UnpackActionsInChapter();
  }

  private void ClearCollectionsForNewChapter()
  {
    actionsInChapter.Clear();
    chapterNavigation.ClearActions();
  }

}
