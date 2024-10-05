using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounters baseCounter;
    [SerializeField] private GameObject[] VisualGameObjectArray;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterEventChangedArgs e)
    {
        if(e.SelectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
        
    }
    private void Show()
    {
        foreach(GameObject VisualGameObject in VisualGameObjectArray)
        {
            VisualGameObject.SetActive(true);

        }
    }
    private void Hide()
    {
        foreach (GameObject VisualGameObject in VisualGameObjectArray)
        {
            VisualGameObject.SetActive(false);

        }
    }
}
