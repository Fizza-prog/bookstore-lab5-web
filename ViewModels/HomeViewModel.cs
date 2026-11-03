using bookstoreLab5.Models;

namespace bookstoreLab5.ViewModels
{
    public class HomeViewModel
    {
        public List<BookCardViewModel> Books { get; set; }
        public List<Author> Authors { get; set; }
    }
}
