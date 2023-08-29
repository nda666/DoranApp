using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace DoranApp.Components
{
    public partial class IsManagerComboBox : ComboBox
    {
        [Browsable(false)]
        public new ObjectCollection Items => base.Items;

        [Browsable(false)]
        public new string DisplayMember
        {
            get => base.DisplayMember;
            set => base.DisplayMember = value;
        }

        // Hide the ValueMember property in the Property Grid
        [Browsable(false)]
        public new string ValueMember
        {
            get => base.ValueMember;
            set => base.ValueMember = value;
        }

        public event EventHandler Leave;

        public IsManagerComboBox()
        {
            InitializeComponent();

            InitializeDefaultItems();
        }

        public IsManagerComboBox(IContainer container)
        {
            container.Add(this);
            InitializeDefaultItems();
            InitializeComponent();
        }

        private void InitializeDefaultItems()
        {
            this.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            Items.Add(new KeyValuePair<bool?, string>(null, "Semua"));
            Items.Add(new KeyValuePair<bool?, string>(true, "Manager"));
            Items.Add(new KeyValuePair<bool?, string>(false, "Bukan Manager"));

            this.DisplayMember = "Value";
            this.ValueMember = "Key";

            this.SelectedIndex = 0;
        }

        // Get the selected value (bool?) from the ComboBox
        public bool? SelectedValue => (SelectedIndex >= 0) ? (bool?)((KeyValuePair<bool?, string>)SelectedItem).Key : null;

        // Get the selected display text from the ComboBox
        public string SelectedDisplayText => (SelectedIndex >= 0) ? ((KeyValuePair<bool?, string>)SelectedItem).Value : "";

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            //do the initial custom logic here
            base.OnSelectedIndexChanged(e);
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
}