using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkside.Editor.Data
{
    public class ElementSettings
    {
        public const string DefaultName = "Not Set";

        public string DisplayName { get; set; } = DefaultName;
        public DisplayType DisplayType { get; set; } = DisplayType.PropertyNoLabel;
        public bool HasListDrawerToggle { get; set; } = false;
    }
}