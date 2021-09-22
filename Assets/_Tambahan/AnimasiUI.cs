using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimasiUI : MonoBehaviour
{
    public GameObject[] Obj_TombolNya;
    // Start is called before the first frame update
    void OnEnable()
    {
        for (int i = 0; i < Obj_TombolNya.Length; i++)
        {
            Obj_TombolNya[i].transform.localScale = new Vector2(0, 0);
        }
        StopAllCoroutines();
        StartCoroutine(AnimasiTombol());
    }

    IEnumerator AnimasiTombol()
    {
        yield return new WaitForSeconds(0);
        for (int i = 0; i < Obj_TombolNya.Length; i++)
        {
            Obj_TombolNya[i].transform.localScale = new Vector2(0, 0);
        }
        yield return new WaitForSeconds(0);
        for (int i = 0; i < Obj_TombolNya.Length; i++)
        {
            Obj_TombolNya[i].transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBack);
            
            yield return new WaitForSeconds(0.1f);
        }
    }
}
