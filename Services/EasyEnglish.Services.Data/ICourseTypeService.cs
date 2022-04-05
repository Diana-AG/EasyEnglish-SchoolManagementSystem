namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;

    public interface ICourseTypeService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();
    }
}
