using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
  public Text displayText;
  public ScrollRect scroll;
  public Text displayActions;
  public InputAction[] inputActions;
  public Chapter[] allChapters;
  public Flag[] allFlags;
  [HideInInspector] public ChapterNavigation chapterNavigation;
  [HideInInspector] public List<string> actionsInChapter = new List<string>();


  List<string> actionLog = new List<string>();
  List<string> displayLog = new List<string>();

  // Use this for initialization
  private void Awake()
  {
    chapterNavigation = GetComponent<ChapterNavigation>();
  }

  private void Start()
  {
    LogStringWithReturn("Kapitel " + chapterNavigation.currentChapter.chapterNumber + ": " +
                        chapterNavigation.currentChapter.chapterName);
    DisplayChapterText();
  }

  public void DisplayChapterText()
  {
    ClearCollectionsForNewChapter();
    UnpackChapter();
    DisplayLoggedText();
    DisplayLoggedActions();
  }

  public void LogStringWithReturn(string stringToAdd)
  {
    displayLog.Add(stringToAdd + "\n");
  }

  public void LogActionWithReturn(string stringToAdd)
  {
    actionLog.Add(stringToAdd + "\n");
  }

  public void DisplayLoggedText()
  {
    string logsAsText = string.Join("\n", displayLog.ToArray());

    displayText.text = logsAsText;
  }

  public void DisplayLoggedActions()
  {
    string logsAsText = "Du kannst: \n" + "weiter \n" + string.Join("\n", actionLog.ToArray());

    displayActions.text = logsAsText;
  }

  private void UnpackChapter()
  {
    chapterNavigation.UnpackChapter();
  }

  private void ClearCollectionsForNewChapter()
  {
    actionsInChapter.Clear();
    actionLog.Clear();
    chapterNavigation.ClearActions();
  }

  private void Update()
  {
    scroll.verticalNormalizedPosition = 0;
  }
}
