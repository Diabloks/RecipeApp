using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Media;
using RecipeApp.Models;
using Xamarin.Forms;
using RecipeApp.Helpers;

namespace RecipeApp.Models
{
    class DataBase
    {
        private string ip;
        private HttpClient client;

        public DataBase()
        {
            ip = "http://f0423389.xsph.ru/";
            client = new HttpClient();
        }

        public string UploadPhoto(byte[] image)
        {
            Uri uri = new Uri(this.ip + "UploadPhoto.php");

            MultipartFormDataContent content = new MultipartFormDataContent();
            ByteArrayContent baContent = new ByteArrayContent(image);
            content.Add(baContent, "img", "image.png");

            var response = client.PostAsync(uri, content);
            var responseString = response.Result.Content.ReadAsStringAsync();
            string result = responseString.Result;

            if (Uri.IsWellFormedUriString(result, UriKind.Absolute))
                return result;
            else
                return null;
        }

        public void SyncProducts()
        {
            Uri uri = new Uri(this.ip + "GetProducts.php");

            List<Product> ProductsList = new List<Product>();

            string json = "";
            if (Settings.Products == null || Settings.Products == "")
            {
                var get = client.GetAsync(uri);

                HttpResponseMessage response = get.Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent responseContent = response.Content;
                    var jsnString = responseContent.ReadAsStringAsync();

                    json = jsnString.Result;
                }
            }
            else
            {
                json = Settings.Products;
            }
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            reader.SupportMultipleContent = true;

            while (true)
            {
                if (!reader.Read())
                {
                    break;
                }

                JsonSerializer serializer = new JsonSerializer();
                Product product = serializer.Deserialize<Product>(reader);

                if (product.img_url != null)
                {
                    try
                    {
                        Uri imgUri = new Uri(product.img_url);
                        ImageSource img = ImageSource.FromUri(imgUri);
                        product.image = img;
                    }
                    catch (Exception)
                    {
                        product.image = null;
                    }
                }

                ProductsList.Add(product);
            }
            Application.Current.Properties["ProductsList"] = ProductsList;

            if (Settings.Products == null || Settings.Products == "")
                Settings.Products = json;
        }

        public void CreateUser(string log, string pass, string email)
        {

        }

        public bool CreateRecipe(Recipe created)
        {
            Uri uri = new Uri(this.ip + "CreateRecipe.php");

            string json = JsonConvert.SerializeObject(created, Formatting.Indented);

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("json", json)
            });

            var post = client.PostAsync(uri, formContent);
            HttpResponseMessage response = post.Result;
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public Recipe GetRecipeByID(int id)
        {
            Uri uri = new Uri(this.ip + "GetRecipe.php");

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("recipe_id", id.ToString())
            });

            var responseAsync = client.PostAsync(uri, formContent);
            HttpResponseMessage response = responseAsync.Result;

            try
            {
                var jsonAsync = response.Content.ReadAsStringAsync();
                string json = jsonAsync.Result;
                Recipe recipe = JsonConvert.DeserializeObject<Recipe>(json);

                try
                {
                    recipe.image = ImageSource.FromUri(new Uri(recipe.img_url));
                }
                catch (Exception)
                {
                    recipe.image = ImageSource.FromFile("defaultRecipe.jpg");
                }

                foreach (RecipeStep step in recipe.steps)
                {
                    try
                    {
                        step.image = ImageSource.FromUri(new Uri(step.img_url));
                    }
                    catch (Exception)
                    {
                        step.image = null;
                    }
                }

                return recipe;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public IList<Recipe> GetRecipes()
        {
            Uri uri = new Uri(this.ip + "GetRecipes.php");

            IList<Recipe> RecipeList = new List<Recipe>();

            var get = client.GetAsync(uri);

            HttpResponseMessage response = get.Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var jsnString = responseContent.ReadAsStringAsync();

                string json = jsnString.Result;
                JsonTextReader reader = new JsonTextReader(new StringReader(json));
                reader.SupportMultipleContent = true;

                while (true)
                {
                    if (!reader.Read())
                    {
                        break;
                    }

                    JsonSerializer serializer = new JsonSerializer();
                    Recipe recipe = serializer.Deserialize<Recipe>(reader);

                    if (recipe.img_url != null)
                    {
                        try
                        {
                            Uri imgUri = new Uri(recipe.img_url);
                            ImageSource img = ImageSource.FromUri(imgUri);
                            recipe.image = img;
                        }
                        catch (Exception)
                        {
                            ImageSource img = ImageSource.FromFile("defaultRecipe.jpg");
                            recipe.image = img;
                        }
                    }

                    RecipeList.Add(recipe);
                }
                Application.Current.Properties["RecipeList"] = RecipeList;

                return RecipeList;
            }
            return null;
        }
    }
}
