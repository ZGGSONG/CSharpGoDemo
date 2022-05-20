using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScadaTool.Helper
{
    public class ViewModelHelper : SingletonMode<ViewModelHelper>
    {
        public SynchronizationContext Context;

        public void UpdateUI(Action action)
        {
            Context?.Post((state) => action(), null);
        }
    }
}
