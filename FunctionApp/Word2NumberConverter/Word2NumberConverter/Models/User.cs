using System;
using System.Collections.Generic;
using System.Text;

namespace Word2NumberConverter.Models
{
    /// <summary>
    /// User class containing basic details and list of translations.
    /// </summary>
    public class User
    {
        Guid UserId { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        DateTime CreatedDateTime { get; set;}
        List<TranslationHistory> TranslationList { get; set; }

    }
}
