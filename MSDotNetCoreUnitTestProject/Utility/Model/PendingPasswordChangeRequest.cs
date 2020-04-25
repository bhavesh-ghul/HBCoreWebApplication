using System;
using System.Collections.Generic;
using System.Text;

namespace MSDotNetCoreUnitTestProject.Utility.Model
{
    public class PendingPasswordChangeRequest
    {
        public string Email { get; set; }

        public bool IsActive { get; set; }
    }
}
