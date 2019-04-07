using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hoverable : MonoBehaviour, IPointerEnterHandler
{
    public static Hoverable current;

    public GameObject hoverObject;

    // Start is called before the first frame update
    void Start()
    {
        hoverObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        current = this;
        ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
    }
}
