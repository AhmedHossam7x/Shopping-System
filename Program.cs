namespace Shopping_System
{
    internal class Program
    {
        static public List<string> cartList = new List<string>();
        static public Dictionary<string, double> itemPrice = new Dictionary<string, double>()
        {
            { "Camera", 1500 },
            { "Laptop", 3500 },
            { "Tv", 2500 },
            { "Pc", 5200 },
            { "Refrigerator", 2000 },
            { "Aircondiation", 3000 },
            { "Hard Disk", 500 },
        };
        static public Stack<string> actions = new Stack<string>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Welcome to the shopping system");
                Console.WriteLine("======================================");
                Console.Write("1)-Add Item to cart  2)-View Item in cart ");
                Console.Write("3)-Remove Item from cart 4)-Checkout 5)-Undo list 0)-Exit: ");
                Console.ForegroundColor = ConsoleColor.White;
                byte chooseMainProsses = byte.Parse(Console.ReadLine());

                switch(chooseMainProsses)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        AddItemToCart();
                        break;
                    case 2:
                        ViewItemInCart();
                        break;
                    case 3:
                        RemoveItemFromCart();
                        break;
                    case 4:
                        Checkout();
                        break;
                    case 5:
                        UndoList();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("-------->Wrong Input<--------");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
        }

        private static void UndoList()
        {
            if(actions.Count > 0)
            {
                string lastAction = actions.Pop();
                Console.WriteLine($"Your last action is {lastAction}");
                var actionArray = lastAction.Split();
                if (lastAction.Contains("added"))
                {
                    cartList.Remove(actionArray[1]);
                }
                else if (lastAction.Contains("deleted"))
                {
                    cartList.Add(actionArray[1]);
                }
                else
                {
                    Console.WriteLine("Check out cannot be undo, Pls ask for refund");
                }
            }
        }
        private static void Checkout()
        {
            if (cartList.Any())
            {
                double totalPrice = 0;
                Console.WriteLine("Your cart items are: ");
                var itemsCart = GetCartPrice();
                foreach (var item in itemsCart)
                    totalPrice += item.Item2;
                Console.WriteLine(totalPrice);
                cartList.Clear();
                actions.Push("CHECKOUT");
            }
        }
        private static void RemoveItemFromCart()
        {
            ViewItemInCart();
            if (cartList.Any())
            {
                Console.Write("Enter item name you want removed: ");
                string? removeName = Console.ReadLine();
                if (cartList.Contains(removeName))
                {
                    cartList.Remove(removeName);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    actions.Push($"Item {removeName} is deleted to your cart");
                    Console.WriteLine($"Item {removeName} is deleted to your cart");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        private static void ViewItemInCart()
        {
            var itemPriceCollections = GetCartPrice();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Avalibole Item in cart");
            Console.WriteLine("======================");
            Console.ForegroundColor = ConsoleColor.White;
            if(cartList.Any())
            {
                //foreach (var cart in cartList)
                //    Console.WriteLine($"{cart}");
                foreach (var cart in itemPriceCollections)
                    Console.WriteLine($"Name of Items: {cart.Item1} and Price: {cart.Item2}");
            }
            else
                Console.WriteLine("=====Cart is Empty=====");
        }
        private static void AddItemToCart()
        {
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("Avalibole Item");
            Console.WriteLine("===================");
            Console.ForegroundColor = ConsoleColor.White;
            foreach(var item in itemPrice)
                Console.WriteLine($"{item.Key} = {item.Value}");

            //Console.Write("You want add item (y/n): ");
            //char inputAddItem = char.Parse(Console.ReadLine());
            //if(inputAddItem == 'y' || inputAddItem == 'Y')
            //{
            //    Console.Write("Enter Item name: ");
            //    string? itemName = Console.ReadLine();
            //    Console.Write("Enter Item Price: ");
            //    double priceItem = double.Parse(Console.ReadLine());

            //    if (itemPrice.ContainsKey(itemName))
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.WriteLine("The item already exit");
            //        Console.ForegroundColor = ConsoleColor.White;
            //    }
            //    else
            //        itemPrice.Add(itemName, priceItem);
            //}

            Console.Write("Pls, Enter prodect name: ");
            string? selectedItem = Console.ReadLine();

            if(itemPrice.ContainsKey(selectedItem))
            {
                cartList.Add(selectedItem);
                Console.ForegroundColor = ConsoleColor.Green;
                actions.Push($"Item {selectedItem} is added to your cart");
                Console.WriteLine($"Item {selectedItem} is added to your cart");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
                Console.WriteLine("Out of stock");
        }
        private static IEnumerable<Tuple<string, double>>GetCartPrice()
        {
            var cartPrice = new List<Tuple<string, double>>();
            foreach(var item in cartList)
            {
                bool foundItem = itemPrice.TryGetValue(item, out double price);
                if(foundItem)
                {
                    Tuple<string, double> itemsPrices = new Tuple<string, double>(item, price);
                    cartPrice.Add(itemsPrices);
                }
            }
            return cartPrice;
        }
    }
}
