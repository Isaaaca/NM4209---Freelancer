using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize(float amt)
    {
        Animator animator = GetComponent<Animator>();
        string str;
        if (amt > 0)
            GetComponent<Text>().text = "+"+amt.ToString();
        else
            GetComponent<Text>().text = amt.ToString();

        animator.Play("ChangeText");
        Destroy(this.gameObject, animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
