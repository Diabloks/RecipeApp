using System;
using System.Collections.Generic;
using System.Text;
using RecipeApp.Models;
namespace RecipeApp.Models {
 public class Recipies {
    public static IEnumerable<Recipie> Get() {
      return new List<Recipie> {
        new Recipie() {
          RecipeName="КАША1",
          ImageUrl="https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",
          Time="5 минут",
          Rate="5",
          AuthorName="k.gavrilove"
        },
        new Recipie() {
          RecipeName="КАША2",
          ImageUrl="https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",
          Time="5 минут",
          Rate="5",
          AuthorName="k.gavrilove"
        },
        new Recipie() {
          RecipeName="КАША3",
          ImageUrl="https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",
          Time="5 минут",
          Rate="5",
          AuthorName="k.gavrilove"
        },
        new Recipie() {
          RecipeName="КАША4",
          ImageUrl="https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",
          Time="5 минут",
          Rate="5",
          AuthorName="k.gavrilove"
        },
        new Recipie() {
          RecipeName="КАША5",
          ImageUrl="https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",
          Time="5 минут",
          Rate="5",
          AuthorName="k.gavrilove"
        }
      };
      }
    }
  }


