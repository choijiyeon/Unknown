using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JYUIManager : MonoBehaviour
{
    private GameObject GameUIRoot;
    List<JYDefines.UISection> _uiSectionList = new List<JYDefines.UISection>();

    //싱글턴
    static JYUIManager _instance = null;
    public static JYUIManager Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = FindObjectOfType<JYUIManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        //GameUIRoot = this.gameObject.GetComponentInChildren<UIRoot>().gameObject;
        //if (null == GameUIRoot)
        //{
        //    Debug.LogError("GameUIRoot is null!!");
        //    return;
        //}

        //DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        JYUIManager.Instance.AttatchUI(JYDefines.UISection.UIStart);
    }

    private void Update()
    {
       
    }

    public void Initialize()
    {
        PreLoadUIObjectList();
    }

    private void PreLoadUIObjectList()
    {
        LoadUIObject(JYDefines.UISection.UIPad);
        LoadUIObject(JYDefines.UISection.UIPlayerHP);
    }

    string GetPathNameByUISection(JYDefines.UISection aUISection)
    {
        return "UIPrefab/" + aUISection.ToString();
    }

    private void LoadUIObject(JYDefines.UISection section)
    {
        //생성
        string path = GetPathNameByUISection(section);
        GameObject uiObj = Resources.Load(path) as GameObject;

        if (uiObj != null)
        {
            GameObject go = Instantiate(uiObj) as GameObject;
            go.transform.parent = GameUIRoot.transform;
            go.transform.localScale = Vector3.one;
            go.name = uiObj.name;
        }
    }

    public void AttatchUI(JYDefines.UISection aUISection)
    {
        _uiSectionList.Add(aUISection);
        LoadUIObject(aUISection);
    }
}
