using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Obvious.Soap.Editor
{
    public class SoapSettings : ScriptableObject
    {
        public EVariableDisplayMode VariableDisplayMode = EVariableDisplayMode.Default;
        public ENamingCreationMode NamingOnCreationMode = ENamingCreationMode.Auto;
        public ECreatePathMode CreatePathMode = ECreatePathMode.Auto;
        public ERaiseEventInEditorMode RaiseEventsInEditor;
        public bool CanEventsBeRaisedInEditor => RaiseEventsInEditor == ERaiseEventInEditorMode.Enabled;
        [FormerlySerializedAs("Categories")] public List<string> Tags = new List<string> { "None" };

        public int GetTagIndex(string tagName)
        {
            if (!Tags.Contains(tagName))
            {
                Debug.LogWarning($"Tag {tagName} does not exist. Returning 0.");
                return 0;
            }

            return Tags.IndexOf(tagName);
        }

        public int GetTagIndex(int fromIndex)
        {
            if (fromIndex < 0 || fromIndex >= Tags.Count)
            {
                Debug.LogWarning($"Tag index {fromIndex} out of range. Returning 0.");
                return 0;
            }

            return fromIndex;
        }
        
        public int GetTagIndex(ScriptableBase scriptableBase)
        {
            var fromIndex = scriptableBase.TagIndex;
            if (fromIndex < 0 || fromIndex >= Tags.Count)
            {
                Debug.Log($"Tag for {scriptableBase.name} not found. Reassigning to None.");
                SoapEditorUtils.AssignTag(scriptableBase,0);
                return 0;
            }

            return fromIndex;
        }
        
        public string GetTagName(ScriptableBase scriptableBase)
        {
            var tagIndex = GetTagIndex(scriptableBase);
            return Tags[tagIndex];
        }
    }

    public enum EVariableDisplayMode
    {
        Default,
        Minimal
    }

    public enum ENamingCreationMode
    {
        Auto,
        Manual
    }


    public enum ECreatePathMode
    {
        Auto,
        Manual
    }

    public enum ERaiseEventInEditorMode
    {
        Disabled,
        Enabled
    }
}