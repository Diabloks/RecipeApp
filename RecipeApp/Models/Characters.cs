using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApp.Models {
  class Characters {
    public static IEnumerable<Character> Get() {
      return new List<Character> {
        new Character() {
          CharacterName = "Веган",
          CharacterImageUrl = "https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",

        },
        new Character() {
          CharacterName = "Халяль",
          CharacterImageUrl = "https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",

        },
        new Character() {
          CharacterName = "Без глютена",
          CharacterImageUrl = "https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",

        },
        new Character() {
          CharacterName = "Без Сахара",
          CharacterImageUrl = "https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",

        },
        new Character() {
          CharacterName = "Без молока",
          CharacterImageUrl = "https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",

        },
        new Character() {
          CharacterName = "Без рыбы",
          CharacterImageUrl = "https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",

        },
        new Character() {
          CharacterName = "Без яиц",
          CharacterImageUrl = "https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",

        },
        new Character() {
          CharacterName = "Без сои",
          CharacterImageUrl = "https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",

        },
      };
    }
  }
}
