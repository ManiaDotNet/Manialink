using System;
using System.Collections.Generic;
using System.Linq;

namespace ManiaNet.Manialink.ManiaScript
{
    internal class ManiaScript
    {
        private Dictionary<string, List<Event>> events = new Dictionary<string, List<Event>>();
        private List<ManiascriptFunction> functions = new List<ManiascriptFunction>();
        private List<ManiascriptGlobal> globalCode = new List<ManiascriptGlobal>();
        private List<ManiascriptMain> mainCode = new List<ManiascriptMain>();
        private int mainFnCounter = 0;
        private List<string> nativeEvents = new List<string> { "MouseClick", "MouseOut", "MouseOver", "KeyPress", "EntrySubmit" };

        public ManiaScript()
        {
        }

        /// <summary>
        /// Adds an event to be checked on runtime
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public ManiaScript addEvent(Event e)
        {
            if (!this.events.ContainsKey(e.Type))
            {
                this.events.Add(e.Type, new List<Event>());
            }
            this.events[e.Type].Add(e);
            return this;
        }

        /// <summary>
        /// Adds a function to the global code. Functions will be rendered after the global Code!
        /// </summary>
        /// <param name="fn">The function to be added</param>
        /// <returns>This</returns>
        public ManiaScript AddFunction(ManiascriptFunction fn)
        {
            this.functions.Add(fn);
            return this;
        }

        /// <summary>
        /// Adds a function to the global code. Functions will be rendered after the global Code!
        /// </summary>
        /// <param name="code">The code of the function</param>
        /// <param name="priority">Functions will be sorted by priority</param>
        /// <returns>This</returns>
        public ManiaScript AddFunction(string code, int priority = 5)
        {
            this.functions.Add(new ManiascriptFunction(code, priority));
            return this;
        }

        /// <summary>
        /// Adds a function to the global code. Functions will be rendered after the global Code!
        /// </summary>
        /// <param name="type">Returntype of the function</param>
        /// <param name="name">The name of the function</param>
        /// <param name="args">The arguments of the function in parantheses like () or (Text arg1, Integer arg2)</param>
        /// <param name="code">The function's body</param>
        /// <param name="priority">Functions will be sorted by priority</param>
        /// <returns>This</returns>
        public ManiaScript AddFunction(string type, string name, string args, string code, int priority = 5)
        {
            this.AddFunction(type + " " + name + args + "{\n" + code + "\n}", priority);
            return this;
        }

        /// <summary>
        /// Adds code before the main() function
        /// </summary>
        /// <param name="code">The code to be added</param>
        /// <returns>This</returns>
        public ManiaScript AddToGlobal(ManiascriptGlobal code)
        {
            this.AddToGlobal(code.Code, code.Priority);
            return this;
        }

        /// <summary>
        /// Adds code before the main() function
        /// </summary>
        /// <param name="code">The code to be added</param>
        /// <param name="priority">Code will be sorted by priority</param>
        /// <returns>This</returns>
        public ManiaScript AddToGlobal(string code, int priority = 5)
        {
            this.globalCode.Add(new ManiascriptGlobal(code, priority));
            return this;
        }

        /// <summary>
        /// Adds code to the main() function before the while-loop
        /// </summary>
        /// <param name="code">The code to be added</param>
        /// <returns>This</returns>
        public ManiaScript AddToMain(ManiascriptMain code)
        {
            this.AddToMain(code.Code, code.Priority, code.Inline);
            return this;
        }

        /// <summary>
        /// Adds code to the main() function before the while-loop
        /// </summary>
        /// <param name="code">The code to be added</param>
        /// <param name="priority">Code will be sorted by priority</param>
        /// <param name="inline">Execute code inline or wrap into a function</param>
        /// <returns>This</returns>
        public ManiaScript AddToMain(string code, int priority = 5, bool inline = true)
        {
            if (!inline)
            {
                string fnName = "mainFn" + (++mainFnCounter).ToString();
                this.AddFunction("Void", fnName, "()", code);
                code = fnName + "();";
            }
            this.mainCode.Add(new ManiascriptMain(code, priority, inline));
            return this;
        }

        /// <summary>
        /// Creates the whole ManiaScript part with all given functions and events
        /// </summary>
        /// <param name="compress">Compressed code does not contain comments or unnecessary whitespaces or newlines. TODO</param>
        /// <returns>The builded ManiaScript</returns>
        public string Build(bool compress = false)
        {
            string result = string.Empty;
            // global code
            result += String.Join("\n", this.globalCode.OrderBy(x => x.Priority).ToList().Select(x => x.Code).ToList());
            // functions
            List<ManiascriptFunction> fn = this.functions.OrderBy(x => x.Priority).ToList();
            result += String.Join("\n", fn.Select(x => x.Code).ToList());
            result += buildMain();
            return result;
        }

        private string buildEvents()
        {
            if (events.Count == 0)
                return string.Empty;
            string result = "foreach (Event in PendingEvents) {switch(Event.Type) {";
            int n = 0;
            foreach (KeyValuePair<string, List<Event>> entry in events)
            {
                if (this.nativeEvents.Contains(entry.Key))
                {
                    if (entry.Value.Count > 0)
                    {
                        result += "case CMlEvent::Type::" + entry.Key + ": {";
                        List<Event> eventList = entry.Value.OrderBy(x => x.Priority).ToList();
                        foreach (Event e in eventList)
                        {
                            if (e.GetType() == typeof(ControlEvent))
                            {
                                ControlEvent x = (ControlEvent)e;
                                result += "if (" + (string.Join(" || ", x.ControlIDs.Select(y => "Event.ControlID == \"" + y + "\"").ToList())) + ") {";
                            }
                            else if (e.GetType() == typeof(KeyPress))
                            {
                                KeyPress x = (KeyPress)e;
                                result += "if (" + (string.Join(" || ", x.Keycodes.Select(y => "Event.KeyCode == " + y).ToList())) + ") {";
                            }
                            else
                                continue;
                            if (e.Inline)
                            {
                                result += e.Code;
                            }
                            else
                            {
                                string fnName = "EventFn" + (++n).ToString();
                                this.AddFunction("Void", fnName, "()", e.Code);
                                result += fnName + "();";
                            }
                            result += "}";
                        }
                        result += "}";
                    }
                }
            }
            result += "}}";
            return result;
        }

        private string buildLoop()
        {
            string result = "while(True) {yield;";
            result += buildEvents();
            result += "}";
            return result;
        }

        private string buildMain()
        {
            string result = "main(){";
            result += String.Join("\n", this.mainCode.OrderBy(x => x.Priority).ToList().Select(x => x.Code).ToList());
            result += buildLoop();
            return result + "}";
        }
    }
}