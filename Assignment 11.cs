using System;
using System.Collections.Generic;

namespace Assignment_08
{
    #region Section 01 - Books, Delegates, Anonymous Methods, and Lambdas

    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }

        public Book(
            string _ISBN,
            string _Title,
            string[] _Authors,
            DateTime _PublicationDate,
            decimal _Price)
        {
            ISBN = _ISBN;
            Title = _Title;
            Authors = _Authors;
            PublicationDate = _PublicationDate;
            Price = _Price;
        }

        public override string ToString()
        {
            string authors = Authors == null ? string.Empty : string.Join(", ", Authors);

            return $"ISBN: {ISBN}, Title: {Title}, Authors: {authors}, " +
                   $"Publication Date: {PublicationDate:yyyy-MM-dd}, Price: {Price:0.00}";
        }
    }

    public static class BookFunctions
    {
        public static string GetTitle(Book B)
        {
            return B.Title;
        }

        public static string GetAuthors(Book B)
        {
            return B.Authors == null
                ? string.Empty
                : string.Join(", ", B.Authors);
        }

        public static string GetPrice(Book B)
        {
            return B.Price.ToString("0.00");
        }
    }

    // Section 01 - (a)
    // User-defined delegate with the same signature as BookFunctions methods:
    // It receives a Book and returns a string.
    public delegate string BookFunctionPointer(Book book);

    public static class LibraryEngine
    {
        // Used with the user-defined delegate.
        public static void ProcessBooks(
            List<Book> bList,
            BookFunctionPointer fPtr)
        {
            foreach (Book B in bList)
            {
                Console.WriteLine(fPtr(B));
            }
        }

        // Section 01 - (b)
        // Same idea, but using the built-in Func<Book, string> delegate.
        public static void ProcessBooks(
            List<Book> bList,
            Func<Book, string> fPtr)
        {
            foreach (Book B in bList)
            {
                Console.WriteLine(fPtr(B));
            }
        }
    }

    #endregion


    #region Section 02 - Order Processing System

    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"Order Id: {Id}, Customer: {CustomerName}, " +
                   $"Price: {Price:0.00}, Quantity: {Quantity}";
        }
    }

    // Part 1 - User-Defined Delegate
    public delegate decimal PriceCalculator(Order order);

    public static class OrderPricing
    {
        // Price x Quantity
        public static decimal CalculateTotal(Order order)
        {
            return order.Price * order.Quantity;
        }

        // Example discount: 10%
        public static decimal CalculateTotalWithDiscount(Order order)
        {
            decimal total = order.Price * order.Quantity;
            decimal discount = total * 0.10m;

            return total - discount;
        }

        // Part 1 - Uses the custom delegate.
        public static decimal CalculateOrderPrice(
            Order order,
            PriceCalculator calculator)
        {
            return calculator(order);
        }

        // Part 2 - Uses the built-in Func<Order, decimal>.
        public static decimal CalculateOrderPrice(
            Order order,
            Func<Order, decimal> calculator)
        {
            return calculator(order);
        }
    }

    public static class OrderValidation
    {
        // Part 3 - Predicate<Order>
        public static bool ValidateOrder(
            Order order,
            Predicate<Order> validationRule)
        {
            return validationRule(order);
        }
    }

    public static class OrderActionRunner
    {
        // Part 4 - Action<Order>
        // The behavior is supplied from outside instead of hard-coding it here.
        public static void ProcessOrder(
            Order order,
            Action<Order> action)
        {
            action(order);
        }
    }

    public class OrderService
    {
        private readonly Func<Order, decimal> _pricingStrategy;
        private readonly Predicate<Order> _validationRule;

        /*
         * Bonus Challenge:
         * OrderService does NOT know how pricing is calculated.
         * It only receives a pricing behavior through Func<Order, decimal>.
         *
         * I also inject the validation rule for the same reason:
         * the service does not need to know every validation rule.
         */
        public OrderService(
            Func<Order, decimal> pricingStrategy,
            Predicate<Order> validationRule)
        {
            _pricingStrategy = pricingStrategy;
            _validationRule = validationRule;
        }

        // Part 5 - Event
        public event Action<Order> OrderProcessed;

        public decimal ProcessOrder(Order order)
        {
            // Validation is part of the final requested flow.
            if (!_validationRule(order))
            {
                throw new InvalidOperationException(
                    $"Order {order.Id} is not valid and cannot be processed.");
            }

            // Pricing behavior is supplied at runtime.
            decimal finalPrice = _pricingStrategy(order);

            Console.WriteLine(
                $"Processing Order {order.Id}. Final Price = {finalPrice:0.00}");

            // Only OrderService can raise the event.
            OrderProcessed?.Invoke(order);

            return finalPrice;
        }
    }

    #endregion


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==============================");
            Console.WriteLine("       ASSIGNMENT 08");
            Console.WriteLine("==============================");

            RunSection01();

            Console.WriteLine();
            Console.WriteLine("======================================");
            Console.WriteLine();

            RunSection02();
        }


        #region Section 01 Demo

        private static void RunSection01()
        {
            Console.WriteLine("SECTION 01 - BOOKS");
            Console.WriteLine("------------------");

            List<Book> books = new List<Book>
            {
                new Book(
                    "978-001",
                    "C# Fundamentals",
                    new[] { "Ahmed Ali", "Sara Hassan" },
                    new DateTime(2024, 1, 10),
                    250.00m),

                new Book(
                    "978-002",
                    "Advanced C#",
                    new[] { "Mona Adel" },
                    new DateTime(2025, 5, 20),
                    320.00m),

                new Book(
                    "978-003",
                    "Delegates and Events",
                    new[] { "Omar Samir", "Nour Khaled" },
                    new DateTime(2026, 2, 15),
                    400.00m)
            };

            Console.WriteLine("\n1) Book.ToString():");
            foreach (Book book in books)
            {
                Console.WriteLine(book);
            }

            // ---------------------------------------------------------
            // (a) User-defined delegate
            // ---------------------------------------------------------
            Console.WriteLine("\n2) User-Defined Delegate - GetTitle:");

            BookFunctionPointer titlePointer = BookFunctions.GetTitle;
            LibraryEngine.ProcessBooks(books, titlePointer);

            // ---------------------------------------------------------
            // (b) Built-in delegate
            // Func<Book, string>
            // ---------------------------------------------------------
            Console.WriteLine("\n3) Built-In Delegate Func<Book, string> - GetAuthors:");

            Func<Book, string> authorsFunction = BookFunctions.GetAuthors;
            LibraryEngine.ProcessBooks(books, authorsFunction);

            Console.WriteLine("\n4) Built-In Delegate Func<Book, string> - GetPrice:");

            Func<Book, string> priceFunction = BookFunctions.GetPrice;
            LibraryEngine.ProcessBooks(books, priceFunction);

            // ---------------------------------------------------------
            // (c) Anonymous Method - GetISBN
            // ---------------------------------------------------------
            Console.WriteLine("\n5) Anonymous Method - GetISBN:");

            BookFunctionPointer getISBN = delegate (Book book)
            {
                return book.ISBN;
            };

            LibraryEngine.ProcessBooks(books, getISBN);

            // ---------------------------------------------------------
            // (d) Lambda Expression - GetPublicationDate
            // ---------------------------------------------------------
            Console.WriteLine("\n6) Lambda Expression - GetPublicationDate:");

            BookFunctionPointer getPublicationDate =
                book => book.PublicationDate.ToString("yyyy-MM-dd");

            LibraryEngine.ProcessBooks(books, getPublicationDate);
        }

        #endregion


        #region Section 02 Demo

        private static void RunSection02()
        {
            Console.WriteLine("SECTION 02 - ORDER PROCESSING SYSTEM");
            Console.WriteLine("------------------------------------");

            Order order = new Order
            {
                Id = 1001,
                CustomerName = "Ahmed",
                Price = 500m,
                Quantity = 2
            };

            Console.WriteLine($"\nOrder: {order}");

            // =========================================================
            // Part 1 - User-Defined Delegate
            // =========================================================
            Console.WriteLine("\nPART 1 - USER-DEFINED DELEGATE");

            // Explicitly store the methods in PriceCalculator variables.
            // This also makes it clear that Part 1 is using the
            // USER-DEFINED delegate rather than the Func<> overload.
            PriceCalculator normalCalculator =
                OrderPricing.CalculateTotal;

            PriceCalculator discountCalculator =
                OrderPricing.CalculateTotalWithDiscount;

            decimal normalTotal =
                OrderPricing.CalculateOrderPrice(
                    order,
                    normalCalculator);

            decimal discountedTotal =
                OrderPricing.CalculateOrderPrice(
                    order,
                    discountCalculator);

            Console.WriteLine($"Normal Total: {normalTotal:0.00}");
            Console.WriteLine($"Total With 10% Discount: {discountedTotal:0.00}");


            // =========================================================
            // Part 2 - Func<Order, decimal>
            // =========================================================
            Console.WriteLine("\nPART 2 - FUNC<ORDER, DECIMAL>");

            Func<Order, decimal> normalPrice =
                x => x.Price * x.Quantity;

            Func<Order, decimal> tenPercentDiscount =
                x => (x.Price * x.Quantity) * 0.90m;

            decimal funcNormal =
                OrderPricing.CalculateOrderPrice(order, normalPrice);

            decimal funcDiscount =
                OrderPricing.CalculateOrderPrice(order, tenPercentDiscount);

            Console.WriteLine($"Func Normal Price: {funcNormal:0.00}");
            Console.WriteLine($"Func 10% Discount: {funcDiscount:0.00}");

            /*
             * Part 2 Question:
             * What problem does Func<> solve compared with creating a custom delegate?
             *
             * Answer:
             * Func<> gives us a ready-made generic delegate type for methods that
             * return a value. Instead of creating a new delegate type every time
             * we need a method with a certain input/output shape, we can use Func<>.
             *
             * Example:
             * Func<Order, decimal>
             *
             * means:
             * - Input  : Order
             * - Output : decimal
             *
             * A custom delegate is still useful when we want a meaningful
             * domain-specific name such as PriceCalculator.
             */


            // =========================================================
            // Part 3 - Predicate<Order>
            // =========================================================
            Console.WriteLine("\nPART 3 - PREDICATE<ORDER>");

            Predicate<Order> quantityIsValid =
                x => x.Quantity > 0;

            Predicate<Order> priceIsValid =
                x => x.Price > 0;

            Predicate<Order> customerNameIsValid =
                x => !string.IsNullOrWhiteSpace(x.CustomerName);

            bool validQuantity =
                OrderValidation.ValidateOrder(order, quantityIsValid);

            bool validPrice =
                OrderValidation.ValidateOrder(order, priceIsValid);

            bool validCustomer =
                OrderValidation.ValidateOrder(order, customerNameIsValid);

            Console.WriteLine($"Quantity > 0: {validQuantity}");
            Console.WriteLine($"Price > 0: {validPrice}");
            Console.WriteLine($"Customer Name Exists: {validCustomer}");

            /*
             * Part 3 Question:
             * Why is Predicate<Order> more expressive here than Func<Order, bool>
             * even though both can represent a function returning bool?
             *
             * Answer:
             * They can represent the same technical shape, but Predicate<Order>
             * communicates the INTENTION more clearly.
             *
             * Predicate<Order> means:
             * "I am testing an Order against a condition."
             *
             * Func<Order, bool> only tells us:
             * "I receive an Order and return a bool."
             *
             * Therefore Predicate<Order> is more readable for validation,
             * filtering, matching, and condition-checking code.
             */


            // =========================================================
            // Part 4 - Action<Order>
            // =========================================================
            Console.WriteLine("\nPART 4 - ACTION<ORDER>");

            Action<Order> printOrder = x =>
            {
                Console.WriteLine($"[PRINT] Order {x.Id} processed.");
            };

            Action<Order> sendConfirmation = x =>
            {
                Console.WriteLine(
                    $"[CONFIRMATION] Confirmation sent to {x.CustomerName}.");
            };

            Action<Order> writeAudit = x =>
            {
                Console.WriteLine(
                    $"[AUDIT] Order {x.Id} was processed at {DateTime.Now}.");
            };

            // Same ProcessOrder method, different behavior.
            OrderActionRunner.ProcessOrder(order, printOrder);
            OrderActionRunner.ProcessOrder(order, sendConfirmation);
            OrderActionRunner.ProcessOrder(order, writeAudit);

            // Multicast delegate:
            // All three actions are stored in one invocation list.
            Console.WriteLine("\nMulticast Action:");

            Action<Order> allActions = printOrder;
            allActions += sendConfirmation;
            allActions += writeAudit;

            OrderActionRunner.ProcessOrder(order, allActions);


            // =========================================================
            // Part 5 + Part 6 + Part 7 + Bonus
            // Events, subscribe/unsubscribe, final flow, runtime pricing
            // =========================================================
            Console.WriteLine("\nPART 5/6/7 + BONUS - EVENTS AND FINAL FLOW");

            // Three separate validation rules are combined into one Predicate.
            Predicate<Order> completeValidation =
                x =>
                    x.Quantity > 0 &&
                    x.Price > 0 &&
                    !string.IsNullOrWhiteSpace(x.CustomerName);

            // Bonus Challenge:
            // Different pricing strategies can be selected at runtime.
            // OrderService does not change when a new strategy is selected.
            Dictionary<string, Func<Order, decimal>> pricingStrategies =
                new Dictionary<string, Func<Order, decimal>>
                {
                    {
                        "Normal Price",
                        x => x.Price * x.Quantity
                    },
                    {
                        "10% Discount",
                        x => (x.Price * x.Quantity) * 0.90m
                    },
                    {
                        "20% Discount",
                        x => (x.Price * x.Quantity) * 0.80m
                    },
                    {
                        "VIP Discount",
                        x => (x.Price * x.Quantity) * 0.70m
                    }
                };

            // This value could come from user input, configuration,
            // database data, etc. Changing this value does not require
            // modifying OrderService.
            string selectedStrategyName = "VIP Discount";

            Func<Order, decimal> selectedPricingStrategy =
                pricingStrategies[selectedStrategyName];

            Console.WriteLine(
                $"Selected Pricing Strategy: {selectedStrategyName}");

            OrderService orderService =
                new OrderService(
                    selectedPricingStrategy,
                    completeValidation);

            // Event handlers are stored in variables so that we can
            // subscribe and later unsubscribe the exact same delegate.
            Action<Order> handler1 = x =>
            {
                Console.WriteLine(
                    $"[HANDLER 1] Order {x.Id} completed.");
            };

            Action<Order> handler2 = x =>
            {
                Console.WriteLine(
                    $"[HANDLER 2] Notification sent for Order {x.Id}.");
            };

            Action<Order> handler3 = x =>
            {
                Console.WriteLine(
                    $"[HANDLER 3] Audit recorded for Order {x.Id}.");
            };

            // Subscribe multiple handlers.
            orderService.OrderProcessed += handler1;
            orderService.OrderProcessed += handler2;
            orderService.OrderProcessed += handler3;

            Console.WriteLine("\nFirst processing: all 3 handlers should run.");

            decimal finalPrice =
                orderService.ProcessOrder(order);

            Console.WriteLine(
                $"Returned Final Price: {finalPrice:0.00}");

            // Unsubscribe Handler1.
            orderService.OrderProcessed -= handler1;

            Console.WriteLine(
                "\nSecond processing after removing Handler1:");

            // Now only Handler2 and Handler3 should execute.
            orderService.ProcessOrder(order);

            Console.WriteLine(
                "\nNotice: Handler1 did not run the second time.");
        }

        #endregion


        /*
         * =============================================================
         * ASSIGNMENT QUESTIONS - ANSWERS
         * =============================================================
         *
         * Q1:
         * What is the difference between:
         * PriceCalculator
         * and
         * Func<Order, decimal>?
         *
         * Answer:
         * PriceCalculator is a USER-DEFINED delegate:
         *
         *     public delegate decimal PriceCalculator(Order order);
         *
         * Func<Order, decimal> is a BUILT-IN generic delegate.
         *
         * Both can point to a method/lambda that receives an Order and
         * returns decimal.
         *
         * The main difference is naming and intent:
         * - PriceCalculator gives the delegate a meaningful domain name.
         * - Func<Order, decimal> avoids creating a new delegate type when
         *   a built-in delegate already matches the needed signature.
         *
         *
         * -------------------------------------------------------------
         *
         * Q2:
         * What is the difference between:
         * Action<Order>
         * and
         * Func<Order, decimal>?
         *
         * Answer:
         * Action<Order>:
         * - Receives an Order.
         * - Returns NOTHING (void).
         * - Good for actions/side effects such as printing, logging,
         *   sending notifications, etc.
         *
         * Func<Order, decimal>:
         * - Receives an Order.
         * - MUST return a decimal.
         * - Good for calculations or transformations.
         *
         *
         * -------------------------------------------------------------
         *
         * Q3:
         * Why does Predicate<T> return bool?
         * What kind of problem is it designed to represent?
         *
         * Answer:
         * Predicate<T> represents a TEST or CONDITION.
         * A condition has two possible results:
         * - true  -> the object satisfies the condition.
         * - false -> the object does not satisfy the condition.
         *
         * It is useful for:
         * - validation
         * - filtering
         * - searching
         * - checking whether an item matches a rule
         *
         * Example:
         *
         *     Predicate<Order> valid =
         *         order => order.Quantity > 0;
         *
         *
         * -------------------------------------------------------------
         *
         * Q4:
         * What is the difference between a delegate and an event?
         *
         * Answer:
         * A delegate is a type-safe reference to one or more methods.
         * It can be assigned, passed as a parameter, and invoked.
         *
         * An event uses a delegate internally but adds controlled
         * publish/subscribe behavior.
         *
         * Outside code can normally subscribe/unsubscribe using += and -=,
         * but only the class that declares the event can raise/invoke it.
         *
         *
         * -------------------------------------------------------------
         *
         * Q5:
         * Why can't external code normally invoke an event declared
         * in another class?
         *
         * Answer:
         * Because an event represents something that belongs to the
         * publisher class.
         *
         * For example, only OrderService should be allowed to say:
         * "The order has been processed."
         *
         * If external classes could raise OrderProcessed themselves,
         * they could announce an event that never actually happened.
         *
         * The event keyword protects this encapsulation.
         *
         *
         * -------------------------------------------------------------
         *
         * Q6:
         * What happens when multiple handlers subscribe to the same event?
         *
         * Answer:
         * The event becomes multicast.
         *
         * When the event is raised, every subscribed handler in its
         * invocation list is called, normally in subscription order.
         *
         * Example:
         * Handler1 -> Handler2 -> Handler3
         *
         * Note:
         * If one handler throws an unhandled exception, normal delegate
         * invocation may stop before later handlers execute.
         *
         *
         * -------------------------------------------------------------
         *
         * Q7:
         * Explain:
         *
         *     orderService.OrderProcessed += HandleOrderProcessed;
         *
         * Breakdown:
         *
         * orderService
         *     The OrderService object/publisher.
         *
         * OrderProcessed
         *     The event we want to listen to.
         *
         * +=
         *     The subscription operator.
         *     It ADDS the handler to the event's invocation list.
         *
         * HandleOrderProcessed
         *     The method/delegate that should run when the event is raised.
         *
         * In simple words:
         * "When orderService raises OrderProcessed, also call
         * HandleOrderProcessed."
         *
         *
         * -------------------------------------------------------------
         *
         * Q8 - Challenge:
         * What is the difference between:
         *
         *     Action<Order>
         *
         * and:
         *
         *     event Action<Order>
         *
         * They both involve Action<Order>, so why use an event?
         *
         * Answer:
         * Action<Order> by itself is simply a delegate.
         *
         * If we publicly expose a delegate field, outside code could
         * potentially:
         * - invoke it directly,
         * - replace its handlers,
         * - assign null,
         * - overwrite the invocation list.
         *
         * event Action<Order> restricts what outside code can do.
         * External code can normally only:
         *
         *     += subscribe
         *     -= unsubscribe
         *
         * It cannot directly raise the event.
         *
         * This keeps the publisher in control and is the correct model
         * for notifications such as OrderProcessed.
         *
         *
         * =============================================================
         * BONUS CHALLENGE EXPLANATION
         * =============================================================
         *
         * The program stores multiple pricing strategies as:
         *
         *     Func<Order, decimal>
         *
         * including:
         * - Normal Price
         * - 10% Discount
         * - 20% Discount
         * - VIP Discount
         *
         * OrderService receives the selected strategy through its
         * constructor:
         *
         *     new OrderService(selectedPricingStrategy, validationRule);
         *
         * Therefore OrderService does NOT need if/else statements to
         * decide which pricing algorithm should run.
         *
         * The caller decides the behavior and supplies it as a delegate.
         * This follows the assignment requirement and makes the design
         * easier to extend without changing OrderService.
         * =============================================================
         */
    }
}
