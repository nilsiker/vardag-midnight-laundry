namespace Vardag;
public partial class PlayerLogic {
  public partial record State {
    public partial record Alive {
      public partial record Grounded {
        public partial record Moving : Grounded {

        }
      }
    }
  }
}
