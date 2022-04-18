namespace EasyEnglish.Web.ViewModels.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MessageListViewModel
    {
        public IEnumerable<MessageViewModel> Messages { get; set; }
    }
}
