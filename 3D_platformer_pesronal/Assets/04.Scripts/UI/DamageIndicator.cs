using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;

    private Coroutine coroutine;

    private void Start()
    {
        CharacterManager.Instance.Player.condition.OnTakeDamge += Flash;
    }

    public void Flash()
    {
        image.enabled = true;
        image.color = new Color(1f, 105f / 255f, 105f / 255f);

        //����ڵ�
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(Fadeaway());
    }


    public IEnumerator Fadeaway()
    {
        float startAlpha = 0.3f;
        float a = startAlpha; // ���İ�

        while (a>0.0f)
        {
            a -= (startAlpha/flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 100f / 255f, 100f / 255f, a);
            yield return null;
        }
        image.enabled = false; // ���İ��� 0�̸� �̹��� ��
    }
}
