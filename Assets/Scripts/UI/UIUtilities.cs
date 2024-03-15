using UnityEngine;

public static class UIUtilities
{
    // ���� ������Ʈ�� Ȱ��ȭ ���¸� �����ϴ� ��ƿ��Ƽ �Լ�
    public static void SetUIActive(GameObject uiObject, bool isActive)
    {
        if (uiObject != null)
        {
            uiObject.SetActive(isActive);
        }
    }
}
