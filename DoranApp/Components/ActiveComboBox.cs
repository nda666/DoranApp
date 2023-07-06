using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.Components
{
    [ToolboxItem(true)]
    public partial class ActiveComboBox : ComboBox
    {

        public event EventHandler Leave;
        public ActiveComboBox()
        {
            InitializeComponent();

            InitializeDefaultItems();
        }

        public ActiveComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            InitializeDefaultItems();
        }

        private void InitializeDefaultItems()
        {
            this.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            // Add default items with value and text
            this.DropDownStyle = ComboBoxStyle.DropDownList;

        }

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
