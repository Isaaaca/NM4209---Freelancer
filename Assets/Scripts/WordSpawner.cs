using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WordSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform leftAnchor;
    [SerializeField]
    private Transform rightAnchor;

    private bool gridPosLeft = true;
    public GameObject wordObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnWord(string word)
    {
        float x;
        if (Upgrades.hasUpgrade("wordGrid"))
        {
            if (gridPosLeft)
                x = leftAnchor.position.x;
            else
                x = rightAnchor.position.x;
            gridPosLeft = !gridPosLeft;
        }
        else
        {
            x = Random.Range(leftAnchor.position.x, rightAnchor.position.x);
        }
        this.transform.position = new Vector3(x, transform.position.y);
        GameObject w;
        w = Instantiate(wordObject,this.transform.position, Quaternion.identity, transform.parent);
        if (Upgrades.hasUpgrade("rigidWords"))
        {
            w.GetComponent<Rigidbody2D>().freezeRotation = true;
        }
        w.GetComponent<Word>().SetWord(word);

        return w;
    }
}
