using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bubble.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public class Post
    {
        public string title { get; set; }
        public string backdropPhotoUrl { get; set; }
        public string overview { get; set; }
    }
    public class LoginObj
    {
        public bool res { get; set; }
        public string user { get; set; }
    }
    public partial class AboutPage : ContentPage
    {
        private const string Url = "http://192.168.1.86:3001/api/management/getarticles";
        private const string loginUrl = "http://192.168.1.86:3001/api/management/login";
        private HttpClient client = new HttpClient();
        private ObservableCollection<Post> _posts;
        public AboutPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            Poster();
            base.OnAppearing();
        }
        public async Task Poster()
        {
            var pidNum = new { user = "pillarman38" };
            var pidret = JsonConvert.SerializeObject(pidNum);
            var content = await client.PostAsync(Url, new StringContent(pidret));

            content.EnsureSuccessStatusCode();

            string json = await content.Content.ReadAsStringAsync();
            var br = JsonConvert.DeserializeObject<List<Post>>(json);
        }
        public async Task Login()
        {
            var contentType = "application/json";
            var data = new { email = username.Text, password = password.Text };
            var pidret = JsonConvert.SerializeObject(data);
            var httpMethod = HttpMethod.Post;
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(loginUrl),
                Method = httpMethod,
                Content = new StringContent(pidret, System.Text.Encoding.UTF8, contentType)
            };

            var httpResponse = await client.SendAsync(request);

            var result = await httpResponse.Content.ReadAsStringAsync();
            
            var br = JsonConvert.DeserializeObject<LoginObj>(result);
            Console.WriteLine(br.res);
            if(br.res == true)
            {
                await Navigation.PushAsync(new HomePage(br));
            }
        }
        void refresher(object sender, System.EventArgs e)
        {
            Poster();
        }
        void OnAdd(object sender, System.EventArgs e)
        {
        }

        void OnUpdate(object sender, System.EventArgs e)
        {
        }
        void LoginBtn(object sender, System.EventArgs e)
        {
            Login();
        }
    }
}