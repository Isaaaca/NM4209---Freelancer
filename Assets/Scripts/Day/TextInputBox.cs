using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInputBox : MonoBehaviour
{
    [SerializeField]
    InputField inputField;
    [SerializeField]
    private WordManager wordManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inputField.selectionAnchorPosition != inputField.selectionFocusPosition)inputField.MoveTextEnd(false);
    }

    public void Submit(string word)
    {
        if(Input.GetKey(KeyCode.Return))
        {
            wordManager.SubmitWord(word);
            inputField.text = "";
        }
        inputField.ActivateInputField();
    }
}
