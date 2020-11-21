using System.Windows.Forms;

namespace CS
{
    class Data_Structure
    {
        public static void Main()
        { 
            Graph G = new Graph();
            UI userInterface = new UI();
            Application.Run(userInterface);
        }
    }
}
