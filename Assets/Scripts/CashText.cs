using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashText : MonoBehaviour
{
    Text t;
    public ChangeText change;
    float currAmt;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();
        currAmt = Resources.money;
        t.text = "$" + Resources.money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refresh()
    {
        if (t == null)
        {
            t = GetComponent<Text>();
            currAmt = Resources.money;
        }
        float diff = Resources.money - currAmt;
        if (diff != 0)
        {
            print(currAmt);
            t.text = "$" + Resources.money.ToString();
            currAmt = Resources.money;
            ChangeText ct = Instantiate(change, this.transform);
            ct.Initialize(diff);
        }
    }
}
