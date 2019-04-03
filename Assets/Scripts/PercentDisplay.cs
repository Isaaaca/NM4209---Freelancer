using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PercentDisplay : MonoBehaviour
{

    private Image bar;
    [SerializeField]
    private float fastSpeed = 0.05f;
    [SerializeField]
    private float slowSpeed = 0.001f;
    [SerializeField]
    private Color fullColor;
    [SerializeField]
    private Color emptyColor;

    private float currAmt;
    private float displayedPercent;
    private float totalAmt;
    private bool fast;

    // Start is called before the first frame update
    void Start()
    {
        bar = this.GetComponent<Image>();
        displayedPercent = 1;
        bar.color = fullColor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currPercent = currAmt / totalAmt;
        //print(currAmt.ToString() + "/"+ totalAmt.ToString() + "=" +currPercent.ToString());
        float percentDiff = currPercent - bar.fillAmount;
        if (Mathf.Abs(percentDiff) > 0.001)
        {
            if (fast)
            {
                bar.fillAmount = Mathf.Clamp(bar.fillAmount + fastSpeed * Mathf.Abs(percentDiff) * Mathf.Sign(percentDiff), 0f, 100f);
            }
            else
            {
                bar.fillAmount = Mathf.Clamp(bar.fillAmount +  slowSpeed * Mathf.Abs(percentDiff) * Mathf.Sign(percentDiff), 0f, 100f); 
            }
            bar.color = fullColor * bar.fillAmount + emptyColor * (1 - bar.fillAmount);
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
        float currPercent = currAmt / totalAmt;
        float percentDiff = currPercent - bar.fillAmount;
        if (Mathf.Abs(percentDiff) > 0.05)
        {
            fast = true;
        }
        else
        {
            fast = false;  
        }

    }

}
