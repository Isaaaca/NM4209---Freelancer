using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PercentDisplay : MonoBehaviour
{

    private Image bar;
    [SerializeField]
    private float fastSpeed;

    private float currAmt;
    private float displayedPercent;
    private float totalAmt;

    // Start is called before the first frame update
    void Start()
    {
        bar = this.GetComponent<Image>();
        displayedPercent = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float currPercent = currAmt / totalAmt;
        //print(currAmt.ToString() + "/"+ totalAmt.ToString() + "=" +currPercent.ToString());
        float percentDiff = currPercent - bar.fillAmount;
        if (Mathf.Abs(percentDiff)> 0.05f)
        {
            bar.fillAmount = Mathf.Clamp(bar.fillAmount + fastSpeed*Mathf.Sign(percentDiff), 0f, currPercent);
        }
        else
        {
            bar.fillAmount = currPercent;
        }
    }

    public void Initialize(float curr, float total)
    {
        currAmt = curr;
        totalAmt = total;
        print("total amt: " + totalAmt.ToString());
    }

    public void Set(float newAmt)
    {
        currAmt = newAmt;
    }

}
