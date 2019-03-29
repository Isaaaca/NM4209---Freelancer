using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Word : MonoBehaviour
{
    [SerializeField]
    private Text wordText;

    private string value;
    private float startY;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > startY)
        {
            wordText.canvasRenderer.SetAlpha(0);
        }
        else wordText.canvasRenderer.SetAlpha(1);
    }

    public void SetWord(string word)
    {
        value = word;
        wordText.text = word;
        if (Upgrades.hasUpgrade("wordGrid"))
        {
            GetComponent<BoxCollider2D>().size = new Vector2(wordText.preferredWidth + 10, wordText.preferredHeight+ 10) * wordText.transform.lossyScale;
        }
        else
        GetComponent<BoxCollider2D>().size = new Vector2(wordText.preferredWidth+5, wordText.preferredHeight) * wordText.transform.lossyScale;
    }
}
