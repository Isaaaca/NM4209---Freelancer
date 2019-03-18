using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Word : MonoBehaviour
{
    [SerializeField]
    private Text wordText;

    private string value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWord(string word)
    {
        value = word;
        wordText.text = word;
        print(wordText.preferredWidth);
        print(wordText.preferredHeight);
        GetComponent<BoxCollider2D>().size = new Vector2(wordText.preferredWidth, wordText.preferredHeight) * wordText.transform.lossyScale;
    }
}
