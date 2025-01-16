using RailwayCompanyIS.Data.Models;
using RailwayCompanyIS.Models.Carrier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwayCompanyIS.Controllers.FrontHelpers
{
    public class FrontHumanizeHelper
    {
        public static string ContactsHumanize(IEnumerable<Contact> contacts)
        {
            string formattedContacts = string.Empty;
            foreach (var contact in contacts)
            {
                string contactType = HumanizeContactType(contact.Type);
                formattedContacts += contact.ContactContent + "(" + HumanizeContactType(contact.Type) + ")";
                formattedContacts += ',';
                formattedContacts += ' ';
            }
            formattedContacts = formattedContacts.TrimEnd(' ');
            formattedContacts = formattedContacts.TrimEnd(',');
            return formattedContacts;
        }

        private static string HumanizeContactType(ContactType type)
        {
            switch (type)
            {
                case ContactType.Email:  
                    return "эл. почта";
                case ContactType.Вконтакте:
                    return "вк";
                case ContactType.Факс:
                    return "факс";
                case ContactType.Telegram:
                    return "тг";
                case ContactType.Домашний:
                    return "дом. тел.";
                case ContactType.Мобильный:
                    return "мобил. тел.";
                default:
                    return "твиттер";
            }
        }
    }
}
