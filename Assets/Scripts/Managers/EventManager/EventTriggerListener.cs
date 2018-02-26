using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using XLua;

public class EventTriggerListener : MonoBehaviour, 
    IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, 
    IMoveHandler
{
    [CSharpCallLua]
    public delegate void VoidDelegate(GameObject go);
    
    private VoidDelegate m_onClick;
    public VoidDelegate onClick
    {
        get { return m_onClick; }
        set
        {
            if (m_onClick != null)
            {
                m_onClick = null;
            }
            m_onClick = value;
        }
    }

    private VoidDelegate m_onDown;
    public VoidDelegate onDown
    {
        get { return m_onDown; }
        set
        {
            if (m_onDown != null)
            {
                m_onDown = null;
            }
            m_onDown = value;
        }
    }

    private VoidDelegate m_onEnter;
    public VoidDelegate onEnter
    {
        get { return m_onEnter; }
        set
        {
            if (m_onEnter != null)
            {
                m_onEnter = null;
            }
            m_onEnter = value;
        }
    }

    private VoidDelegate m_onExit;
    public VoidDelegate onExit
    {
        get { return m_onExit; }
        set
        {
            if (m_onExit != null)
            {
                m_onExit = null;
            }
            m_onExit = value;
        }
    }

    private VoidDelegate m_onUp;
    public VoidDelegate onUp
    {
        get { return m_onUp; }
        set
        {
            if (m_onUp != null)
            {
                m_onUp = null;
            }
            m_onUp = value;
        }    
    }

    private VoidDelegate m_onMove;
    public VoidDelegate onMove
    {
        get { return m_onMove; }
        set
        {
            if (m_onMove != null)
            {
                m_onMove = null;
            }
            m_onMove = value;
        }
    }

    static public EventTriggerListener Get(GameObject go)
    {
        EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
        if (listener == null) listener = go.AddComponent<EventTriggerListener>();
        return listener;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(gameObject);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null)
        {
            onDown(gameObject);
        };
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
    }

    public void OnMove(AxisEventData eventData)
    {
        if (onMove != null) onMove(gameObject);
    }

    protected void OnDestroy()
    {
        onClick = null;
        onDown = null;
        onUp = null;
        onEnter = null;
        onExit = null;
        onMove = null;
    }

}