using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RecipeApp.Models
{
    public class Recipe
    {
        public int id { get; set; } //Id рецепта в БД
        public DateTime DateTake { get; set; } //Время получения рецепта с БД
        public DateTime date { get; set; } //Время публикации 
        public string name { get; set; } //Название рецепта
        public string img_url { get; set; } //Ссылка на картинку
        public ImageSource image { get; set; } //Сама картинка
        public string description { get; set; } //Комментарий создателя
        public int servings { get; set; } //Количество получаемых порций при готовке
        public int time { get; set; } //Время приготовления
        public string timeType { get; set; } //Тип времени (минут, часов)
        public int rating { get; set; } //Рейтинг рецепта
        public string login { get; set; } //Имя пользователя (создателя рецепта)
        public string category { get; set; } //Имя категории к которой относится рецепт
        public IList<RecipeStep> steps { get; set; } //Шаги приготовления
        public IList<Ingredient> ingredients { get; set; } //Список продуктов
        public IList<Comment> comments { get; set; } //Список комментариев
    }
}
