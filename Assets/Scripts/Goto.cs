using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputAction/Goto")]
public class Goto : InputAction
{
  
  public override void RespondToInput(GameController controller, string[] separatedInputWords)
  {
    foreach (var chapter in controller.allChapters)
    {
      
      if (chapter.id == separatedInputWords[1])
      {
        controller.chapterNavigation.currentChapter = chapter;
        controller.chapterNavigation.UnpackChapter();
      }
    }
  }
}
