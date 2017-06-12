using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextInput : MonoBehaviour
{
  private GameController controller;
  public InputField inputField;

  private void Awake()
  {
    controller = GetComponent<GameController>();
    inputField.onEndEdit.AddListener(AcceptStringInput);
  }

  private void AcceptStringInput(string userInput)
  {
    if (Input.GetButtonDown("Submit"))
    {
      controller.LogStringWithReturn(userInput);

      userInput = userInput.ToLower();

      char[] delimiterCharacters = {' '};
      string[] seperatedInputWords = userInput.Split(delimiterCharacters);

      for (int i = 0; i < controller.inputActions.Length; i++)
      {
        InputAction inputAction = controller.inputActions[i];
        if (inputAction.keyWord == seperatedInputWords[0])
        {
          inputAction.RespondToInput(controller, seperatedInputWords);
        }
      }

      InputComplete();
    }
  }

  private void InputComplete()
  {
    controller.DisplayLoggedText();
    inputField.ActivateInputField();
    inputField.text = null;
  }
}
