using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApp.Models {
  class Ingridients {
    public static IEnumerable<Ingridient> Get() {
      return new List<Ingridient> {
        new Ingridient() {
          IngridientName="Яблоко",
          IngridientFormat="Штук",
          IngridientValue="3"


        }
      };
    }
  }
}
