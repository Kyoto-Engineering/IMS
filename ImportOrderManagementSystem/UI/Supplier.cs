using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportOrderManagementSystem.UI
{
    class Supplier
    {
        private string name;
        private string phone;
        private string fax;
        private string email;
        private string weburl;
        private string code;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string WebSiteUrl
        {
            get { return weburl; }
            set { weburl = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

    }
}
