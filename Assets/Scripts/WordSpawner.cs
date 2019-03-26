using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    [SerializeField]
    private float minX;
    [SerializeField]
    private float maxX;

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
        float x = Random.Range(minX, maxX);
        this.transform.position = new Vector3(x, transform.position.y);
        GameObject w;
        w = Instantiate(wordObject,this.transform.position, Quaternion.identity, transform.parent);
        w.GetComponent<Word>().SetWord(word);

        return w;
    }
}
