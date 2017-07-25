using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystam
{
    public delegate void CloseTab(ITabbedWindow sender,EventArgs e);
    public interface ITabbedWindow
    {
          event CloseTab CloseInitiated;
          string TabName { get; set; }
           string TabTitle { get; set; }
    }
}
