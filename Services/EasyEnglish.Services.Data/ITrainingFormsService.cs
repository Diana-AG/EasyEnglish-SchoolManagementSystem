namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;

    public interface ITrainingFormsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();
    }
}
