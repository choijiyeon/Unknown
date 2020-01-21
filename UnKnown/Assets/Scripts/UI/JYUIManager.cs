using System.Collections;
using System.Collections.Generic;
using UIContents;
using UnityEngine;
using UIList = System.Collections.Generic.List<UIContents.JYUIBase>;

public class JYUIManager : MonoBehaviour
{
    private GameObject GameUIRoot = null;
    List<JYDefines.UISection> _uiSectionList = new List<JYDefines.UISection>(); 
    Dictionary<JYDefines.UISectionFun, UIList> _uiSectionMap = new Dictionary<JYDefines.UISectionFun, UIList>();

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
    //public void Notify(JYDefines.UISectionFun section, params object[] values) { JYUIBase._uiNotyfySender.Notify(section, values); }

    private void Awake()
    {
    }

    private void Start()
    {
        JYUIManager.Instance.AttatchUI(JYDefines.UISection.UIStart);
    }

    private void Update()
    {

    }

    public void CallFunc(JYDefines.UISection uISection)
    {

    }

    public void Initialize()
    {
        PreLoadUIObjectList();
    }

    private void PreLoadUIObjectList()
    {
        //  LoadUIObject(JYDefines.UISection.UIPad);
        // LoadUIObject(JYDefines.UISection.UIPlayerHP);
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
    public void AttachUISection(JYDefines.UISectionFun section, JYUIBase uiBase)
    {
        UIList uiList;

        if (!_uiSectionMap.ContainsKey(section))
        {
            uiList = new UIList();
            _uiSectionMap.Add(section, uiList);
        }
        else
            uiList = _uiSectionMap[section];

        if (!uiList.Contains(uiBase))
            uiList.Add(uiBase);
    }
    public void DetachUISection(JYDefines.UISectionFun section, JYUIBase uiBase)
    {
        if (_uiSectionMap.ContainsKey(section))
        {
            UIList uiList = _uiSectionMap[section];
            if (uiList.Contains(uiBase))
                uiList.Remove(uiBase);
        }
    }
    public void Notify(JYDefines.UISectionFun section, params object[] values)
    {
        if (_uiSectionMap.ContainsKey(section))
        {
            UIList uiList = _uiSectionMap[section];
            for (int i = 0; i < uiList.Count; ++i)
            {
                if (uiList[i] == null)
                    Debug.LogWarning("Just UI Destory after Detach eUISection. Call eUISection : " + section);
                else
                    uiList[i].UpdateView(section, values);
            }
        }
    }
}