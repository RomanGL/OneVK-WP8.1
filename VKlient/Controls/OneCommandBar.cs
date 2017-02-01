using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

namespace OneVK.Controls
{
    [ContentProperty(Name = "PrimaryCommands")]
    public class OneCommandBar : BaseOneCommandBar
    {
        public OneCommandBar()
        {
            this.DefaultStyleKey = typeof(OneCommandBar);            
        }        
    }
}
