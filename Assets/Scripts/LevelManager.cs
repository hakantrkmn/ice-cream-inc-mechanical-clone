using PathCreation;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region singleton

    private static LevelManager _instance;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("LevelManager");
                go.AddComponent<LevelManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    #endregion singleton

    public List<GameObject> ices;

    public PathCreator pathCreator;
    public List<Color> pattern;

    public Transform targetParent;

    private void Start()
    {
        createTarget();
    }

    // levelde yapmayı amaçladığımız dondurmanın yapıldığı fonksiyon. basit bir level şeklinde tasarladım. daha karışık veya basit levellerde yapılabilir.
    private void createTarget()
    {
        for (int i = 0; i < GameManager.Instance.pathCreator.path.NumPoints; i++)
        {
            if (i < GameManager.Instance.pathCreator.path.NumPoints / 2)
            {
                var ice = Instantiate(ices[0], transform.position, Quaternion.identity, targetParent);
                ice.transform.position = pathCreator.path.GetPoint(i);
                ice.transform.LookAt(pathCreator.path.GetPoint(i + 1));
                pattern.Add(ice.GetComponentInChildren<Renderer>().material.color);
            }
            else
            {
                var ice = Instantiate(ices[1], transform.position, Quaternion.identity, targetParent);
                ice.transform.position = pathCreator.path.GetPoint(i);
                ice.transform.LookAt(pathCreator.path.GetPoint(i + 1));
                pattern.Add(ice.GetComponentInChildren<Renderer>().material.color);
            }
        }
    }

}