using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInputBox : MonoBehaviour
{
    [SerializeField]
    InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Submit()
    {
        inputField.text = "";
        inputField.Select();

    }
}
