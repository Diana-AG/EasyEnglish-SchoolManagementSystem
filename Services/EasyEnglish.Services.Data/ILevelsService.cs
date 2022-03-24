namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;

    public interface ILevelsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();
    }
}
