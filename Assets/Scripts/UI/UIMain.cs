using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.parentsUI = transform;
        UIManager.Instance.ShowPopup<BattleUI>(transform);
    }
}
