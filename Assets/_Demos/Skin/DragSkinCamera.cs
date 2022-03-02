using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSkinCamera : MonoBehaviour,IDragHandler,IEndDragHandler,IBeginDragHandler
{
    public Transform camTrans;
    // Start is called before the first frame update
    private Vector3 bigenPos;
    private bool draging = false;
    void Start()
    {
        bigenPos = gameObject.transform.position;
        Debug.Log((gameObject.transform as RectTransform).position);
    }

    // Update is called once per frame
    void Update()
    {
        if (draging)
        {
            float deltaX = gameObject.transform.position.x - bigenPos.x;
            float deltaY = gameObject.transform.position.y - bigenPos.y;
            if (Mathf.Abs(deltaX)>Mathf.Abs(deltaY))
            {
                camTrans.position = camTrans.position + new Vector3(Mathf.Sign(deltaX)*0.01f, 0, 0);
            }
            else
            {
                camTrans.position = camTrans.position + new Vector3(0,0,Mathf.Sign(deltaY)*0.01f);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position += new Vector3(eventData.delta.x,eventData.delta.y,0);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.transform.position = bigenPos;
        draging = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        draging = true;
    }
}
