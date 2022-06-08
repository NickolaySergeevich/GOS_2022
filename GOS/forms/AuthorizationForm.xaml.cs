
using GOS.WorkWithDB;

namespace GOS.forms
{
    public partial class AuthorizationForm
    {
        public AuthorizationForm()
        {
            InitializeComponent();

            var test = WorkWithDb.Instance;
        }
    }
}
