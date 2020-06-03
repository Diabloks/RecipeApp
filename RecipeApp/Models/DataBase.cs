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
            //ip = "http://f0423389.xsph.ru/"; //Прошлый хостинг (все еще работает, запасной вариант)
            ip = "http://recipeapp.s91548kn.beget.tech/";
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
            if (Settings.Products == string.Empty)
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

        public bool LogIn(string log, string pass)
        {
            Uri uri = new Uri(this.ip + "LoginUser.php");

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_login", log),
                new KeyValuePair<string, string>("user_password", pass)
            });

            var post = client.PostAsync(uri, formContent);
            HttpResponseMessage response = post.Result;
            if (response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                var answer = content.ReadAsStringAsync();
                try
                {
                    User me = JsonConvert.DeserializeObject<User>(answer.Result);
                    if (me.img_url != null)
                    {
                        try
                        {
                            Uri imgUri = new Uri(me.img_url);
                            me.image = ImageSource.FromUri(imgUri);
                        }
                        catch (Exception)
                        {
                            me.image = ImageSource.FromFile("iconsUser.png");
                        }
                    }
                    else
                        me.image = ImageSource.FromFile("iconsUser.png");
                    Settings.myProfile = answer.Result;
                    Settings.myPass = pass;
                    Application.Current.Properties["MyProfile"] = me;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
                return false;
        }

        public bool CreateUser(string log, string pass, string email)
        {
            Uri uri = new Uri(this.ip + "CreateUser.php");

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_login", log),
                new KeyValuePair<string, string>("user_password", pass),
                new KeyValuePair<string, string>("email", email)
            });

            var post = client.PostAsync(uri, formContent);
            HttpResponseMessage response = post.Result;
            if (response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                var answer = content.ReadAsStringAsync();
                try
                {
                    User me = JsonConvert.DeserializeObject<User>(answer.Result);
                    me.image = ImageSource.FromFile("iconsUser.png");
                    Settings.myProfile = answer.Result;
                    Settings.myPass = pass;
                    Application.Current.Properties["MyProfile"] = me;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
                return false;
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

        public bool UpdateUsersSettings(User me, string pass)
        {
            if (Application.Current.Properties.ContainsKey("MyProfile") && Application.Current.Properties["MyProfile"] != null)
            {
                try
                {
                    if (pass == null)
                        pass = Settings.myPass;
                    me.image = null;
                    string json = JsonConvert.SerializeObject(me);

                    Uri uri = new Uri(this.ip + "UpdateUsersSettingsData.php");

                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("json", json),
                        new KeyValuePair<string, string>("password", pass),
                    });

                    var post = client.PostAsync(uri, formContent);
                    HttpResponseMessage response = post.Result;
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
                
            }
            else
                return false;
        }

        public IList<Recipe> SearchRecipeName(string name)
        {
            Uri uri = new Uri(this.ip + "GetRecipesFilter.php");

            IList<Recipe> RecipeList = new List<Recipe>();

            var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("pattern", name)
                });

            var post = client.PostAsync(uri, formContent);

            HttpResponseMessage response = post.Result;

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
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            ImageSource img = ImageSource.FromFile("defaultRecipe.jpg");
                            recipe.image = img;
                        }
                    }

                    RecipeList.Add(recipe);
                }

                return RecipeList;
            }
            return null;
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
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool AddToFavourite(int id)
        {
            if (Application.Current.Properties.ContainsKey("MyProfile") && Application.Current.Properties["MyProfile"] != null)
            {
                User me = Application.Current.Properties["MyProfile"] as User;
                Uri uri = new Uri(this.ip + "CreateFavorite.php");

                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("recipe_id", id.ToString()),
                    new KeyValuePair<string, string>("login", me.login),
                });

                var post = client.PostAsync(uri, formContent);
                HttpResponseMessage response = post.Result;
                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public bool ChangeRateRecipe(int recipeId, int value)
        {
            Uri uri = new Uri(this.ip + "AddRatingRecipe.php");

            if (value > 1)
                value = 1;
            if (value < 0)
                value = 0;
            var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("recipe_id", recipeId.ToString()),
                    new KeyValuePair<string, string>("value", value.ToString()),
                });

            var post = client.PostAsync(uri, formContent);
            HttpResponseMessage response = post.Result;
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public string CreateComment(int recipeId, Comment comment)
        {
            Uri uri = new Uri(this.ip + "CreateComment.php");

            try
            {
                string json = JsonConvert.SerializeObject(comment);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("id", recipeId.ToString()),
                    new KeyValuePair<string, string>("json", json),
                });

                var post = client.PostAsync(uri, formContent);
                HttpResponseMessage response = post.Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync();
                    string result = content.Result;
                    return result;
                }
                else
                    return null;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }

        public IList<Recipe> GetUserRecipes(string login)
        {
            Uri uri = new Uri(this.ip + "GetUserRecipes.php");

            IList<Recipe> RecipeList = new List<Recipe>();

            var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("login", login)
                });

            var post = client.PostAsync(uri, formContent);

            HttpResponseMessage response = post.Result;

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
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            ImageSource img = ImageSource.FromFile("defaultRecipe.jpg");
                            recipe.image = img;
                        }
                    }

                    RecipeList.Add(recipe);
                }

                return RecipeList;
            }
            return null;
        }

        public IList<Recipe> GetFavourite(string login)
        {
            Uri uri = new Uri(this.ip + "GetFavorite.php");

            IList<Recipe> RecipeList = new List<Recipe>();

            var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("user_login", login)
                });

            var post = client.PostAsync(uri, formContent);

            HttpResponseMessage response = post.Result;

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
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            ImageSource img = ImageSource.FromFile("defaultRecipe.jpg");
                            recipe.image = img;
                        }
                    }

                    RecipeList.Add(recipe);
                }

                return RecipeList;
            }
            return null;
        }

        public bool ChangeRateComment(int commentId, int value)
        {
            Uri uri = new Uri(this.ip + "ChangeRatingComment.php");

            if (value > 1)
                value = 1;
            if (value < 0)
                value = 0;
            var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("comment_id", commentId.ToString()),
                    new KeyValuePair<string, string>("value", value.ToString()),
                });

            var post = client.PostAsync(uri, formContent);
            HttpResponseMessage response = post.Result;
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
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
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
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
