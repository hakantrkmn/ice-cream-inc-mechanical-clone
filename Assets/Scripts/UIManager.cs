using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Slider progressBar;
    public Text percentText;

    void Start()
    {
        GameManager.Percent += updatePercent;
    }

    //yüzdeyi progress bara ve texte yazdığımız kod
    private void updatePercent(float percent)
    {
        progressBar.value = percent;
        percentText.text =  "% "+Mathf.RoundToInt(percent).ToString();
    }


    private void OnDestroy()
    {
        GameManager.Percent -= updatePercent;
    }

    //bastığımız butona göre managerdaki fonksiyonu değişken ile çağrıyoruz. değişkeni true yapma sebebimiz butona bir kere basınca belirli aralıklarla
    //dondurma oluşturmaya başlıyor.
    public void createYellowIce()
    {
        GameManager.Instance.startCreating = true;
        StartCoroutine(GameManager.Instance.createIce("yellow"));
    }
    public void createBlueIce()
    {
        GameManager.Instance.startCreating = true;
        StartCoroutine(GameManager.Instance.createIce("blue"));
    }

    //butona tekrar basarsak değişkeni false yapıyoruz ve üretme duruyor.
    public void stopCreating()
    {
        GameManager.Instance.startCreating = false;
    }
}
