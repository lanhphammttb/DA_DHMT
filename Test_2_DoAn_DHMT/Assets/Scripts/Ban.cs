using UnityEngine;

public class Ban : MonoBehaviour
{
    void Start()
    {
        // Ðãng kí event
        EventManager.Instance.ShowObject += Show;
        EventManager.Instance.HideObject += Hide;
    }


    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}