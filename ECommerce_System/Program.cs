using ECommerceSystem.Models;
using Microsoft.EntityFrameworkCore;

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
        //CASE 4 :  PLACE ORDER 
        //-------------------------------------------------
        static void PlaceOrder()
        {
            Console.Clear();
            Console.WriteLine("----- Place Order -----");

            Console.Write("Enter user ID: ");
            int userId = int.Parse(Console.ReadLine());

            if (!context.Users.Any(u => u.userId == userId))
            {
                Console.WriteLine("User not found.");
                return;
            }

            Order order = new Order
            {
                userId = userId,
                orderDate = DateTime.Now,
                status = "Pending",
                totalAmount = 0
            };

            // create and save Order first to get orderid
            context.Orders.Add(order);
            context.SaveChanges();

            decimal totalAmount = 0;
            string addMore = "yes";

            while (addMore == "yes")
            {
                var products = context.Products.Where(p => p.isAvailable && p.stockQuantity > 0).ToList();
                    
                                    Console.WriteLine("\nAvailable Products:");
                foreach (Product p in products)
                {
                    Console.WriteLine("ID: " + p.productId +" | Name: " + p.productName +" | Price: " + p.price +" | Stock: " + p.stockQuantity);
                               
                }

                Console.Write("Enter product ID: ");
                int productId = int.Parse(Console.ReadLine());

                Product product = products.FirstOrDefault(p => p.productId == productId);

                if (product == null)
                {
                    Console.WriteLine("Product not found or not available.");
                    continue;
                }

                Console.Write("Enter quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                //quantity validation
                if (quantity <= 0 || quantity > product.stockQuantity)
                {
                    Console.WriteLine("Invalid quantity.");
                    continue;
                }

                OrderProduct orderProduct = new OrderProduct
                {
                    orderId = order.orderId,
                    productId = product.productId,
                    quantity = quantity,
                    unitPrice = product.price
                };

                
                context.OrderProducts.Add(orderProduct);

                // decrement stock
                product.stockQuantity -= quantity;

                // calculate  totalAmount
                totalAmount += orderProduct.unitPrice * quantity;

                // ask user if they want to add another product
                Console.Write("Add another product? yes/no: ");
                addMore = Console.ReadLine().ToLower();


            }

            order.totalAmount = totalAmount;

            // Save OrderProducts, stock changes, and totalAmount
            context.SaveChanges();

            Console.WriteLine("Order placed successfully.");
            Console.WriteLine("Order ID: " + order.orderId);
            Console.WriteLine("Total Amount: " + order.totalAmount);
        }
        
        // CASE 5: WRITE PRODUCT REVIEW
        //-------------------------------------------------
        static void WriteReview()
        {
            Console.Clear();
            Console.WriteLine("----- Write Product Review -----");

            //list of users
            Console.WriteLine("available Users:");
            foreach (User user in context.Users.ToList())
            {
                Console.WriteLine("iD: " + user.userId + " | username: " + user.username);
            }

            Console.Write("enter user ID: ");
            int userId = int.Parse(Console.ReadLine());

            //list of products
            Console.WriteLine("\nAvailable Products:");
            foreach (Product product in context.Products.ToList())
            {
                Console.WriteLine("iD: " + product.productId + " | name: " + product.productName);
            }

            Console.Write("enter product ID: ");
            int productId = int.Parse(Console.ReadLine());


            Console.Write("enter rating from 1 to 5: ");
            int rating = int.Parse(Console.ReadLine());

            //rating validation
            if (rating < 1 || rating > 5)
            {
                Console.WriteLine("rating must be between 1 and 5.");
                return;
            }

            Console.Write("enter comment, or leave empty: ");
            string comment = Console.ReadLine();

            Review review = new Review
            {
                userId = userId,
                productId = productId,
                rating = rating,
                comment = string.IsNullOrWhiteSpace(comment) ? null : comment,
                reviewDate = DateTime.Now
            };

            context.Reviews.Add(review);
            context.SaveChanges();

            Console.WriteLine("review added successfully.");
            Console.WriteLine("review ID: " + review.reviewId);
        }
        
        // Case 6: Update Product Price and Availability
        // --------------------------------------------------
        static void UpdateProduct()
        {
            Console.Clear();
            Console.WriteLine("----- Update Product -----");

            var products = context.Products.ToList();

            if (!products.Any())
            {
                Console.WriteLine("No products found.");
                return;
            }

            Console.WriteLine("Products:");
            foreach (Product productItem in products)
            {
                Console.WriteLine("ID: " + productItem.productId + " | Name: " + productItem.productName +" | Price: " + productItem.price +" | Available: " + productItem.isAvailable);

            }

            Console.Write("Enter product ID: ");
            int productId = int.Parse(Console.ReadLine());

            Product product = context.Products.FirstOrDefault(p => p.productId == productId);
                

            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.Write("Enter new price: ");
            decimal newPrice = decimal.Parse(Console.ReadLine());

            Console.Write("Is product available? true/false: ");
            bool isAvailable = bool.Parse(Console.ReadLine());

            product.price = newPrice;
            product.isAvailable = isAvailable;

            context.SaveChanges();

            Console.WriteLine("Product updated successfully.");
        }
        // Case 7: Cancel Order
        // --------------------------------------------------
        static void CancelOrder()
        {
            Console.Clear();
            Console.WriteLine("----- Cancel Order -----");

            Console.Write("Enter order ID: ");
            int orderId = int.Parse(Console.ReadLine());

            Order order = context.Orders.FirstOrDefault(o => o.orderId == orderId);


            if (order == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            if (order.status == "Cancelled")
            {
                Console.WriteLine("Order is already cancelled");
                return;
            }

            //get all products that belong to this order from the OrderProducts table
            var orderProducts = context.OrderProducts.Where(op => op.orderId == orderId).ToList();



            foreach (OrderProduct prod in orderProducts)
            {
                //search for product 
                Product product = context.Products.FirstOrDefault(p => p.productId == prod.productId);


                if (product != null)
                {
                    product.stockQuantity += prod.quantity;
                }
            }

            order.status = "Cancelled";

            context.SaveChanges();

            Console.WriteLine("Order cancelled successfully");
            Console.WriteLine("Product stock restored");
        }
        // Case 8: Delete Review
        // --------------------------------------------------
        static void DeleteReview()
        {
            Console.Clear();
            Console.WriteLine("----- Delete Review -----");

            var reviews = context.Reviews.ToList();

            if (!reviews.Any())
            {
                Console.WriteLine("No reviews found");
                return;
            }

            Console.WriteLine("Reviews:");
            foreach (Review reviewProduct in reviews)
            {
                Console.WriteLine("ID: " + reviewProduct.reviewId +
                                  " | Rating: " + reviewProduct.rating +
                                  " | Comment: " + reviewProduct.comment);
            }

            Console.Write("Enter review ID: ");
            int reviewId = int.Parse(Console.ReadLine());

            Review review = context.Reviews.FirstOrDefault(r => r.reviewId == reviewId);
                

            if (review == null)
            {
                Console.WriteLine("Review not found");
                return;
            }

            context.Reviews.Remove(review);
            context.SaveChanges();

            Console.WriteLine("Review deleted successfully");
        }
        // Case 9: View All Products
        // --------------------------------------------------
        static void ViewAllProducts()
        {
            Console.Clear();
            Console.WriteLine("----- View All Products -----");

            var products = context.Products.ToList();

            if (!products.Any())
            {
                Console.WriteLine("No products found.");
                return;
            }

            foreach (Product product in products)
            {
                Console.WriteLine("Product ID: " + product.productId);
                Console.WriteLine("Name: " + product.productName);
                Console.WriteLine("Price: " + product.price);
                Console.WriteLine("Stock Quantity: " + product.stockQuantity);
                Console.WriteLine("Available: " + product.isAvailable);
            }
        }
        // Case 10 Filter Products by Category and Price Range
        // --------------------------------------------------
        static void FilterProducts()
        {
            Console.Clear();
            Console.WriteLine("----- Filter Products -----");

            var categories = context.Categories.ToList();

            if (!categories.Any())
            {
                Console.WriteLine("No categories found.");
                return;
            }

            Console.WriteLine("Categories:");
            foreach (Category category in categories)
            {
                Console.WriteLine("ID: " + category.categoryId +" | Name: " + category.categoryName);
                                  
            }

            Console.Write("Enter category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            Console.Write("Enter minimum price: ");
            decimal minPrice = decimal.Parse(Console.ReadLine());

            Console.Write("Enter maximum price: ");
            decimal maxPrice = decimal.Parse(Console.ReadLine());

            var products = context.Products .Where(p => p.categoryId == categoryId &&

            // price must be greater than or equal to minimum price
            // price must be less than or equal to maximum price
            p.price >= minPrice &&p.price <= maxPrice).OrderBy(p => p.price).ToList();
           
            if (!products.Any())
            {
                Console.WriteLine("no products found in this filter");
                return;
            }

            foreach (Product product in products)
            {
                Console.WriteLine("ID: " + product.productId +
                                  " | Name: " + product.productName +
                                  " | Price: " + product.price +
                                  " | Stock: " + product.stockQuantity);
            }
        }
        // Case 11 Get Category with all Its Products
        // --------------------------------------------------
        static void GetCategoryWithProducts()
        {
            Console.Clear();
            Console.WriteLine("----- Category With Products -----");

            Console.Write("Enter category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            // i used include to load the related Products with the selected Category 
            Category category = context.Categories.Include(c => c.Products).FirstOrDefault(c => c.categoryId == categoryId);
                
                

            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            Console.WriteLine("Category ID: " + category.categoryId);
            Console.WriteLine("Category Name: " + category.categoryName);
            Console.WriteLine("Description: " + category.description);

            if (!category.Products.Any())
            {
                Console.WriteLine("No products in this category.");
                return;
            }

            Console.WriteLine("Products:");
            foreach (Product product in category.Products)
            {
                Console.WriteLine("ID: " + product.productId +" | Name: " + product.productName +
                                  " | Price: " + product.price +" | Stock: " + product.stockQuantity);
                                  
            }
        }
        // Case 12 View Order History with Full Details
        // --------------------------------------------------
        static void ViewOrderHistory()
        {
            Console.Clear();
            Console.WriteLine("----- User Order History -----");

            Console.Write("Enter user ID: ");
            int userId = int.Parse(Console.ReadLine());

            // load  user (include) their orders ,(then include) each order's OrderProducts,

            // and (then include)  each OrderProduct's related Product details

            User user = context.Users.Include(u => u.Orders).ThenInclude(o => o.OrderProducts).ThenInclude(op => op.Product).FirstOrDefault(u => u.userId == userId);
                
                    
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine("User: " + user.username);

            if (!user.Orders.Any())
            {
                Console.WriteLine("This user has no orders.");
                return;
            }

            foreach (Order order in user.Orders)
            {
                Console.WriteLine("\nOrder ID: " + order.orderId);
                Console.WriteLine("Order Date: " + order.orderDate);
                Console.WriteLine("Status: " + order.status);
                Console.WriteLine("Total Amount: " + order.totalAmount);

                Console.WriteLine("Order Products:");

                foreach (OrderProduct item in order.OrderProducts)
                {
                    Console.WriteLine("Product: " + item.Product.productName +
                                      " | Quantity: " + item.quantity +
                                      " | Unit Price: " + item.unitPrice);
                }
            }
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
                        PlaceOrder();
                        break;

                    case 5:
                        WriteReview();
                        break;

                    case 6:
                        UpdateProduct();
                        break;

                    case 7:
                        CancelOrder();
                        break;

                    case 8:
                        DeleteReview();
                        break;

                    case 9:
                        ViewAllProducts();
                        break;

                    case 10:
                        FilterProducts();
                        break;

                    case 11:
                        GetCategoryWithProducts();
                        break;

                    case 12:
                        ViewOrderHistory();
                        break;
                    case 13:
                        
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