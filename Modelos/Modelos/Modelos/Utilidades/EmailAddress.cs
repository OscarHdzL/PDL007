

namespace Modelos.Modelos.Utilidades
{
    public class EmailAddress
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public EmailAddress()
        {

        }

        public EmailAddress(string name, string address)
        {
            this.Name = name;
            this.Address = address;
        }
    }
}
