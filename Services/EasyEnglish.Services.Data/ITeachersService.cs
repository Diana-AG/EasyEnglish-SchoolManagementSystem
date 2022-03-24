namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;

    public interface ITeachersService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();
    }
}
