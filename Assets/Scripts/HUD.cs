using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEngine;

public class HUD : BaseManager<HUD>
{
    [SerializeField] private List<CanvasGroup> keyIcons;

    public void FillIcon(int index)
    {
        if (keyIcons[index] != null)
            keyIcons[index].DOFade(1, 0.5f);
    }
}
