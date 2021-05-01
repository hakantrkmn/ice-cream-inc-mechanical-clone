using DG.Tweening;
using PathCreation;
using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    #endregion singleton

    public PathCreator pathCreator;

    public bool startCreating = false;

    public GameObject yellowIce;
    public GameObject blueIce;
    public GameObject iceParent;

    public float similarityPercent;
    public float correctIceCount = 0;
    public int i = 0;

    public static event Action<float> Percent;


    //butona bastığımızda dondurma düşmesini sağlayan fonksiyon
    public IEnumerator createIce(string color)
    {
        if (i <= pathCreator.path.NumPoints - 2)
        {
            //butona basılı tutulduğunda belirli süre aralığıyla düşmesi için bazı kontroller yapılıyor
            if (startCreating)
            {
                if (color == "yellow")
                {
                    var ice = Instantiate(yellowIce, transform.position, Quaternion.identity, iceParent.transform);
                    ice.transform.forward = (pathCreator.path.GetPoint(i) - pathCreator.path.GetPoint(i + 1)).normalized;
                    ice.transform.DOMove(pathCreator.path.GetPoint(i), 2);
                    //oluşturduğumuz her dondurmadan sonra amaçladığımız dondurma ile karşılaştırıyoruz.benzerse kaydedip yüzdesini alıyoruz.
                    checkSimilarity(ice.GetComponentInChildren<Renderer>().material.color);
                    yield return new WaitForSeconds(.5f);
                    StartCoroutine(createIce(color));
                }
                else if (color == "blue")
                {
                    var ice = Instantiate(blueIce, transform.position, Quaternion.identity, iceParent.transform);
                    ice.transform.forward = (pathCreator.path.GetPoint(i) - pathCreator.path.GetPoint(i + 1)).normalized;
                    ice.transform.DOMove(pathCreator.path.GetPoint(i), 2);
                    checkSimilarity(ice.GetComponentInChildren<Renderer>().material.color);
                    yield return new WaitForSeconds(.5f);
                    StartCoroutine(createIce(color));
                }
            }
            else
            {
                StopAllCoroutines();
            }
        }
    }

    private void checkSimilarity(Color obj)
    {
        if (LevelManager.Instance.pattern[i] == obj)
        {
            correctIceCount += 1;
        }
        else
        {
        }
        similarityPercent = (correctIceCount * 100) / pathCreator.path.NumPoints;
        Percent(similarityPercent);
        i++;
    }
}