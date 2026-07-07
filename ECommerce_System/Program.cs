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
        // Case 2: Add  Categories
        // --------------------------------------------------
        static void AddCategory()
        {
            Console.Clear();
            Console.WriteLine("----- Add Category -----");

            Console.Write("Enter category name: ");
            string categoryName = Console.ReadLine();

            Console.Write("Enter description, or leave empty: ");
            string description = Console.ReadLine();

            Console.Write("Enter image URL, or leave empty: ");
            string imageUrl = Console.ReadLine();

            Category category = new Category
            {
                categoryName = categoryName,

                // optional 
                description = string.IsNullOrWhiteSpace(description) ? null : description,
                imageUrl = string.IsNullOrWhiteSpace(imageUrl) ? null : imageUrl
            };

            context.Categories.Add(category);
            context.SaveChanges();

            Console.WriteLine("Category added successfully.");
            Console.WriteLine("New Category ID: " + category.categoryId);
        }
      
 
        static void Main(string[] args)
        {
            int choice;

            do
            {
                Console.WriteLine("\n----- E-Commerce EF Core System ----");
                Console.WriteLine("1. Register New User");
                Console.WriteLine("2. Add Category");
                Console.WriteLine("3. Add New Product");
                Console.WriteLine("4. Place Order");
                Console.WriteLine("5. Write Product Review");
                Console.WriteLine("6. Update Product Price and Availability");
                Console.WriteLine("7. Cancel Order");
                Console.WriteLine("8. Delete Review");
                Console.WriteLine("9. View All Products");
                Console.WriteLine("10. Filter Products by Category and Price Range");
                Console.WriteLine("11. Get Category With Products");
                Console.WriteLine("12. View User Order History");
                Console.WriteLine("13. Product Summary Report");
                Console.WriteLine("0. Exit");

                Console.Write("Enter choice: ");
                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        RegisterUser();
                        break;

                    case 2:
                        AddCategory();
                      
                        break;

                    case 3:
                        AddCategory();
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