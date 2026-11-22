using System.Windows.Forms;

namespace Second_Project_Test__Original_
{
    public partial class memoryButton : Button
    {
        private int buttonValue;

        public int Value
        {
            get { return buttonValue; }
            set { buttonValue = value; this.Tag = value; }
        }

        public virtual void Reveal()
        {
            this.ForeColor = System.Drawing.Color.Yellow;
        }

        public virtual void HideValue()
        {
            this.ForeColor = System.Drawing.Color.Black;
        }
    }
}