using ECommerceSystem.Models;

namespace ECommerce_System
{
    internal class Program
    {
       public static ECommerceContext context = new ECommerceContext();

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
        // Case 3: Add New Product to Category
        // --------------------------------------------------
        static void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("----- Add New Product -----");

            var categories = context.Categories.ToList();

            if (!categories.Any())
            {
                Console.WriteLine("no categories found ,add categories first");
                return;
            }

            Console.WriteLine("available categories:");
            foreach (Category category in categories)
            {
                Console.WriteLine("ID: " + category.categoryId + " | Name: " + category.categoryName);

            }

            Console.Write("Enter category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            Category selectedCategory = context.Categories.FirstOrDefault(c => c.categoryId == categoryId);


            if (selectedCategory == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            Console.Write("Enter product name: ");
            string productName = Console.ReadLine();

            Console.Write("Enter description, or leave empty: ");
            string description = Console.ReadLine();

            Console.Write("Enter price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Enter stock quantity: ");
            int stockQuantity = int.Parse(Console.ReadLine());

            Console.Write("Enter image URL, or leave empty: ");
            string imageUrl = Console.ReadLine();

            Product product = new Product
            {
                productName = productName,

                // optional 
                description = string.IsNullOrWhiteSpace(description) ? null : description,
                imageUrl = string.IsNullOrWhiteSpace(imageUrl) ? null : imageUrl,

                price = price,
                stockQuantity = stockQuantity,
                categoryId = categoryId,

                // system generated 
                createdAt = DateTime.Now,
                isAvailable = true
            };

            context.Products.Add(product);
            context.SaveChanges();

            Console.WriteLine("Product added successfully.");
            Console.WriteLine("New Product ID: " + product.productId);
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
                        AddProduct();
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