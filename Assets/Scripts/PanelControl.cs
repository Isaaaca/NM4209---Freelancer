using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControl : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPanel()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("open", true);
        foreach (UpgradeButton b in GetComponentsInChildren<UpgradeButton>())
        {
            b.enabled = true;
        }

    }

    public void ClosePanel()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("open", false);
        foreach (UpgradeButton b in GetComponentsInChildren<UpgradeButton>())
        {
            b.enabled = false;
        }

    }
}
