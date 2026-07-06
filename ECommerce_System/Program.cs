using ECommerceSystem.Models;

namespace ECommerce_System
{
    internal class Program
    {
        static ECommerceContext context = new ECommerceContext();

        // case 1: register New User
        // --------------------------------------------------
        static void RegisterUser()
        {
            Console.Clear();
            Console.WriteLine("----- Register New User -----");

            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            Console.Write("Enter full name: ");
            string fullName = Console.ReadLine();

            Console.Write("Enter phone number, or leave empty: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter address, or leave empty: ");
            string address = Console.ReadLine();

            User user = new User
            {
                username = username,
                email = email,
                passwordHash = password,
                fullName = fullName,

                // if user leaves it empty and  save null because it is optional
                phoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? null : phoneNumber,
                address = string.IsNullOrWhiteSpace(address) ? null : address,

                // system generated 
                registrationDate = DateTime.Now,
                isActive = true
            };

            context.Users.Add(user);
            context.SaveChanges();

            Console.WriteLine("User registered successfully.");
            Console.WriteLine("New User ID: " + user.userId);
        }
        static void Main(string[] args)
        {
            int choice;

            do
            {
                Console.WriteLine("\n----- E-Commerce EF Core System ----");
                Console.WriteLine("1. Register New User");
                Console.WriteLine("2. Add New Product");
                Console.WriteLine("3. Place Order");
                Console.WriteLine("4. Write Product Review");
                Console.WriteLine("5. Update Product Price and Availability");
                Console.WriteLine("6. Cancel Order");
                Console.WriteLine("7. Delete Review");
                Console.WriteLine("8. View All Products");
                Console.WriteLine("9. Filter Products by Category and Price Range");
                Console.WriteLine("10. Get Category With Products");
                Console.WriteLine("11. View User Order History");
                Console.WriteLine("12. Product Summary Report");
                Console.WriteLine("0. Exit");

                Console.Write("Enter choice: ");
                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        RegisterUser();
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        break;

                    case 5:
                        break;

                    case 6:
                        break;

                    case 7:
                        break;

                    case 8:
                        break;

                    case 9:
                        break;

                    case 10:
                        break;

                    case 11:
                      
                        break;

                    case 12:
                        
                        break;

                    case 0:
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                if (choice != 0)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (choice != 0);
        }
    }
}