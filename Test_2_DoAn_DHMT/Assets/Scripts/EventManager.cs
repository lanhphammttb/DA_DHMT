using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EventManager : Singleton<EventManager>
{
    public UnityAction ShowObject;
    public UnityAction HideObject;


    public void Show()
    {
        if (ShowObject != null)
        {
            ShowObject();
        }
    }

    public void Hide()
    {
        if (HideObject != null)
        {
            HideObject();
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("MyMainScene"); //nhan nut play chuyen qua man hinh
    }

    public void Exit()
    {
       SceneManager.LoadScene("LevelMenu");
    }
}
