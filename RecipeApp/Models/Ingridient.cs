using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApp.Models {
 public class Ingridient {
    public string IngridientName {
      get {
        return this.IngridientName;
      }
      set {
        this.IngridientName = value;
      }
    }
    public string IngridientValue { get; set; }
    public string IngridientFormat { get; set; }
  }
}
