using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApp.Models {
  class Categories {
    public static IEnumerable<Category> Get() {
      return new List<Category> {
        new Category() {
          CategoryName="Мясо",
          CategoryImageUrl="https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",

        },
        new Category() {
          CategoryName="Салат",
          CategoryImageUrl="https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",

        },
        new Category() {
          CategoryName="Основное",
          CategoryImageUrl="https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",

        },
        new Category() {
          CategoryName="Дессерт",
          CategoryImageUrl="https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",

        },
        new Category() {
          CategoryName="Напитки",
          CategoryImageUrl="https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",

        },
        new Category() {
          CategoryName="Суп",
          CategoryImageUrl="https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",

        },
        new Category() {
          CategoryName="Азиатская",
          CategoryImageUrl="https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",

        },
        new Category() {
          CategoryName="Соусы",
          CategoryImageUrl="https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",

        }

      };
    }
    }
}
