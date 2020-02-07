using System;
using System.Linq;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Interact.Contracts;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Interact.Enums;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Present.Attributes.Base;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Theme.Enums;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Present.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Enum | AttributeTargets.Field)]
    public sealed class ActionDisplayAttribute : BaseDisplayAttribute, IPresentDisplay, IPresentImageConfig, IInteractionShortcutConfig
    {
        private object _inherit;

        public bool Alt { get; set; }

        public bool Control { get; set; }

        public EActionImage Image { get; set; } = EActionImage.None;

        public string ImagePath { get; set; }

        public object Inherit
        {
            get => _inherit;
            set
            {
                if(!Equals(_inherit, value))
                {
                    _inherit = value;

                    var fieldInfo = XHelper.Enums.GetFieldInfo(_inherit);

                    var dispayConfig = XHelper.Models.GetDisplayData(fieldInfo);
                    if(dispayConfig != null)
                    {
                        Name = Name ?? dispayConfig.Value.Name;
                        Description = Description ?? dispayConfig.Value.Description;
                    }

                    var shortcut = XHelper.Reflections.Attributes.Get<IInteractionShortcutConfig>(fieldInfo);
                    if(shortcut != null)
                    {
                        Alt = shortcut.Alt;
                        Control = shortcut.Control;
                        Shift = shortcut.Shift;
                        Key = shortcut.Key;
                    }

                    var image = XHelper.Reflections.Attributes.Get<IPresentImageConfig>(fieldInfo);
                    if(image != null)
                    {
                        Image = image.Image;
                        ImagePath = ImagePath ?? image.ImagePath;
                    }
                }
            }
        }

        public EKey Key { get; set; }

        public bool Shift { get; set; }

        string IPresentDisplay.Description => GetDescription();

        string IPresentDisplay.GroupName => GetGroupName();

        string IPresentDisplay.Name => GetName();
    }
}