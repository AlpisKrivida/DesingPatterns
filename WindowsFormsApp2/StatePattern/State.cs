using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2.StatePattern
{
    abstract class State
    {
        protected bool finishedIteration;
        protected Label l1;
        protected Label l2;
        protected Button bt1;
        protected TextBox tb1;
        protected TextBox tb2;

        public State()
        {
            l1 = Application.OpenForms["Form1"].Controls["label1"] as Label;
            l2 = Application.OpenForms["Form1"].Controls["label2"] as Label;
            tb1 = Application.OpenForms["Form1"].Controls["textbox1"] as TextBox;
            tb2 = Application.OpenForms["Form1"].Controls["textbox2"] as TextBox;
            bt1 = Application.OpenForms["Form1"].Controls["button1"] as Button;
        }

        public bool IsIterationFinished()
        {
            return finishedIteration;
        }
        public abstract void RenderElements();
    }
}
