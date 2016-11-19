using System;

namespace _3DGame
{
    public class MenuItem
    {
        public string Name { get; set; }
        public bool Active { get; set; }

        public event EventHandler Click;

        public MenuItem(string name)
        {
            Name = name;
            Active = true;
        }

        public virtual void OnClick()
        {
            Click?.Invoke(this, EventArgs.Empty);
        }
    }

}