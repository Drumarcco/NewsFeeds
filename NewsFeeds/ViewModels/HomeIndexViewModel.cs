using NewsFeeds.Models;
using System.Collections.Generic;

namespace NewsFeeds.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Topic> Topics { get; set; }
    }
}