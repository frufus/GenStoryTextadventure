using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputAction/NextChapter")]
public class NextChapter : InputAction
{
  
  public override void RespondToInput(GameController controller, string[] separatedInputWords)
  {
    if (separatedInputWords.Length > 0)
    {
      controller.chapterNavigation.ChangeToNextChapter();
    }
  }
}
