using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DoranApp.Components
{
    public partial class IsManagerComboBox : ComboBox
    {
        public event EventHandler Leave;
        public IsManagerComboBox()
        {
            InitializeComponent();

            InitializeDefaultItems();
        }

        public IsManagerComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            InitializeDefaultItems();
        }

        private void InitializeDefaultItems()
        {
            this.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.ValueMember = "Value";
            // Add default items with value and text
            this.Items.Add(new ListItem("", "Semua"));
            this.Items.Add(new ListItem("true", "Manager"));
            this.Items.Add(new ListItem("false", "Bukan Manager"));
            this.SelectedIndex = 0;
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            //do the initial custom logic here
            base.OnSelectedIndexChanged(e);//raise the event for the page handler
            MessageBox.Show(this.SelectedText);
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            OnLeaveEvent(e);
        }

        protected virtual void OnLeaveEvent(EventArgs e)
        {
            Leave?.Invoke(this, e);
        }
    }


    public class ListItem
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public ListItem(string value, string text)
        {
            Value = value;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }

}
