using System;
using System.Collections.Generic;
using System.Linq;

namespace ManiaNet.Manialink.ManiaScript
{
    internal abstract class ControlEvent : Event
    {
        public string[] ControlIDs { get; private set; }

        public ControlEvent(string code, string type, string[] controlIDs, int priority = 5, bool inline = true)
            : base(code, type, priority, inline)
        {
            if (controlIDs.Length < 1)
                throw new ArgumentException("ControlEvent expects at least one controlId");
            this.ControlIDs = controlIDs;
        }
    }

    internal class EntrySubmit : ControlEvent
    {
        public EntrySubmit(string code, string[] controlIDs, int priority = 5, bool inline = true)
            : base(code, "EntrySubmit", controlIDs, priority, inline)
        { }
    }

    internal abstract class Event : ManiascriptCode
    {
        public string Type { get; private set; }

        public Event(string code, string type, int priority = 5, bool inline = true)
            : base(code, priority, inline)
        {
            this.Type = type;
        }
    }

    internal class KeyPress : Event
    {
        public int[] Keycodes { get; set; }

        public KeyPress(string code, int[] keycodes, int priority = 5, bool inline = true)
            : base(code, "KeyPress", priority, inline)
        {
            this.Keycodes = keycodes;
        }
    }

    internal class MouseClick : ControlEvent
    {
        public MouseClick(string code, string[] controlIDs, int priority = 5, bool inline = true)
            : base(code, "MouseClick", controlIDs, priority, inline)
        { }
    }

    internal class MouseOut : ControlEvent
    {
        public MouseOut(string code, string[] controlIDs, int priority = 5, bool inline = true)
            : base(code, "MouseOut", controlIDs, priority, inline)
        { }
    }

    internal class MouseOver : ControlEvent
    {
        public MouseOver(string code, string[] controlIDs, int priority = 5, bool inline = true)
            : base(code, "MouseOver", controlIDs, priority, inline)
        { }
    }
}