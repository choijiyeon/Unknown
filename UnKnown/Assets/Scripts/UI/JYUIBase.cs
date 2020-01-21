using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIContents
{
    public abstract class JYUIBase : MonoBehaviour
    {

        //public static readonly JYUIBaseNotifySender _uiNotyfySender = new JYUIBaseNotifySender();

        protected abstract void InitUIData();
        public abstract void UpdateView();
        public virtual void UpdateView(JYDefines.UISectionFun section, params object[] values)
        {
            switch (section)
            {
                case JYDefines.UISectionFun.InitUIData:
                    {

                    }
                    break;
            }
        }

        public virtual void Awake()
        {
            InitUIData();
        }
    }
}
