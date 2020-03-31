using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApp.Models {
 public class TimeModels {
    public static IEnumerable<TimeModel> Get() {
      return new List<TimeModel> {
        new TimeModel() {
          TimeName="Завтрак",
          TimeImageUrl="https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",
          
        },
        new TimeModel() {
          TimeName="Обед",
          TimeImageUrl="https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",


        },
        new TimeModel() {
          TimeName="Ужин",
          TimeImageUrl="https://sun9-61.userapi.com/c855728/v855728173/20c14f/2g5lxf6E5bw.jpg",


        }
      };
    }
  }
}

