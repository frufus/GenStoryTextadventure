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
    userInput = userInput.ToLower();
    
    controller.LogStringWithReturn(userInput);
    
    InputComplete();
      
  }

  private void InputComplete()
  {
    controller.DisplayLoggedText();
    inputField.ActivateInputField();
    inputField.text = null;
  }
}
