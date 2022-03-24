namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;

    public interface ILanguagesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();
    }
}
