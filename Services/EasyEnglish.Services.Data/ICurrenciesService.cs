namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;

    public interface ICurrenciesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();
    }
}
