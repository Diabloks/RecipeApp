using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Specialized;

namespace RecipeApp.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SingUpPage : ContentPage {
    public SingUpPage() {
            InitializeComponent();
    }

    public void SignUp_Clicked(object sender, EventArgs e){ //Временно, тест работы с бд
            if(login.Text != "" && password.Text != "" && email.Text != "" && password.Text == confirmPass.Text)
            {
                WebClient client = new WebClient();
                Uri uri = new Uri("http://10.0.2.2/CreateUser.php");
                NameValueCollection parameters = new NameValueCollection();

                parameters.Add("user_login", login.Text);
                parameters.Add("user_password", password.Text);
                parameters.Add("email", email.Text);

                var response = client.UploadValues(uri, "POST" ,parameters);
                client.UploadValuesCompleted += client_UploadValuesComplited;
                string json = Encoding.UTF8.GetString(response);
                //login.Text = json;
            }
    }

    void client_UploadValuesComplited(object sender, UploadValuesCompletedEventArgs e){ //Пока временно, тестирую работу бд
            //string id = Encoding.UTF8.GetString(e.Result);
            //int newID = 0;

            //int.TryParse(id, out newID);
    }

    public void LogIn_Clicked(object sender, EventArgs e)
        {
            
        }
  }
}