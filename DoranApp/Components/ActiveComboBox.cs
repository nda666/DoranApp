using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace DoranApp.Components
{
    [ToolboxItem(true)]
    public partial class ActiveComboBox : ComboBox
    {
        private bool isItemsAdded = false;

        public event EventHandler Leave;

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

        public ActiveComboBox()
        {
            InitializeComponent();

            InitializeDefaultComponent();
        }

        public ActiveComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            InitializeDefaultComponent();
        }

        private void InitializeDefaultComponent()
        {
            this.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DisplayMember = "Value";
            this.ValueMember = "Key";
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // Populate the ComboBox with items and corresponding display text (only once)
            if (!isItemsAdded)
            {
                Items.Add(new KeyValuePair<bool?, string>(true, "Aktif"));
                Items.Add(new KeyValuePair<bool?, string>(false, "Tidak Aktif"));
                Items.Add(new KeyValuePair<bool?, string>(null, "Semua"));

                // Set initial selection to "Semua"
                SelectedIndex = 0;

                isItemsAdded = true;
            }
        }

        // Get the selected value (bool?) from the ComboBox
        public bool? SelectedValue => (SelectedIndex >= 0) ? (bool?)((KeyValuePair<bool?, string>)SelectedItem).Key : null;

        // Get the selected display text from the ComboBox
        public string SelectedDisplayText => (SelectedIndex >= 0) ? ((KeyValuePair<bool?, string>)SelectedItem).Value : "";

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            OnLeaveEvent(e);
        }

        protected virtual void OnLeaveEvent(EventArgs e)
        {
            Leave?.Invoke(this, e);
            if (this.SelectedIndex == -1)
            {
                this.Text = "";
            }
        }
    }
}