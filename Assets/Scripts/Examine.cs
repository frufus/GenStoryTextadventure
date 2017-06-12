using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputAction/Examine")]
public class Examine : InputAction
{
  
  public override void RespondToInput(GameController controller, string[] separatedInputWords)
  {
    
    if (separatedInputWords.Length > 0 && controller.chapterNavigation.HasAction(separatedInputWords[0]))
    {
      foreach (var flag in controller.chapterNavigation.activeFlags)
      {
        if (flag.Value.id == separatedInputWords[1] && flag.Value.type == "item")
        {
            controller.LogStringWithReturn(flag.Value.description);
        }
        else
        {
          controller.LogStringWithReturn("Das kannst du nicht tun.");
        } 
      } 
    }
    else
    {
      controller.LogStringWithReturn("Das kannst du nicht tun.");
    }
  }
}
