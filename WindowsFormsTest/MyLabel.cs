using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace WindowsFormsTest
{
    class MyLabel:Label
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            if (GetService(typeof(ISelectionService)) is ISelectionService iss)
            {
                iss.SelectionChanged += Iss_SelectionChanged;
            }
            base.OnHandleCreated(e);
        }

        private void Iss_SelectionChanged(object sender, EventArgs e)
        {
            if (GetService(typeof(ISelectionService)) is ISelectionService iss)
            {
                Text = $@"窗体设计器当前选中了{iss.SelectionCount}个组件,第一个组件是{((Component) iss.PrimarySelection).Site.Name}";
            }
        }
    }
}
