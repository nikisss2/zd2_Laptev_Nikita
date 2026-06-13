using Xamarin.Forms;
using zd5_2_Laptev.Views;

namespace zd5_2_Laptev
{
    public partial class MainCarousel : CarouselPage
    {
        public MainCarousel()
        {
            InitializeComponent();
            // связываем экраны
            welcomePage.Init(this, secondPage);
        }

        // метод перелистывания на нужную страницу
        public void GoTo(ContentPage page)
        {
            CurrentPage = page;
        }
    }
}