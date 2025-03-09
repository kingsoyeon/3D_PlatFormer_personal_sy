using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour,IInteractable
{
    public ItemData itemData;

    public string GetInteractPrompt()
    {
        string str = $"{itemData.displayName}\n{itemData.description}";
        return str;
    }

    public void OnInteract()
    {
        // 아이템데이터에 넣어줌
        CharacterManager.Instance.Player.ItemData = itemData;
        // 액션
        CharacterManager.Instance.Player.AddItem.Invoke();
        // E누르면 아이템 먹은거니까 없애줌
        Destroy(gameObject);
    }
}
