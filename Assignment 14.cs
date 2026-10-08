// Assignment 14 - Entity Framework Core (Code First)
// Based on the Session 14 demo and the EFCoreAssignment instructions.
// Projects: (1) E-Commerce, (2) Library, (3) Health Care.
// Required NuGet packages: Microsoft.EntityFrameworkCore and Microsoft.EntityFrameworkCore.SqlServer
// SQL Server: change "Server=." in the three DbContext classes if your SQL instance is different.
// This is ONE C# file; add it to a .NET Console project and remove the default Program.cs.
// The demo uses EnsureCreated() for simplicity. For a migrations-based project,
// do NOT use EnsureCreated(); create and apply migrations instead.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop = Assignment_14.ECommerce;
using Books = Assignment_14.LibrarySystem;
using Clinic = Assignment_14.HealthCare;

// ============================================================
// PROJECT 1: E-COMMERCE SYSTEM
// Category (1) ---> (Many) Product
// Customer (1) ---> (Many) Order
// Order (Many) <---> (Many) Product via OrderDetail
// ============================================================
namespace Assignment_14.ECommerce
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation property: one Category contains many Products.
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // Foreign key + reference navigation property.
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        // Link to Orders through the join entity.
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }

    public class OrderDetail
    {
        // Composite primary key: (OrderId, ProductId).
        public int OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;

        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;

        public int Quantity { get; set; }
    }

    public class ECommerceDbContext : DbContext
    {
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=Assignment14_ECommerce;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.ProductId });

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);
        }
    }
}

// ============================================================
// PROJECT 2: LIBRARY SYSTEM
// Author (1) ---> (Many) Book
// Book (Many) <---> (Many) Borrower via Loan
// ============================================================
namespace Assignment_14.LibrarySystem
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;

        public int AuthorId { get; set; }
        public virtual Author Author { get; set; } = null!;

        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }

    public class Borrower
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime MembershipDate { get; set; }

        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }

    public class Loan
    {
        // Composite primary key: (BookId, BorrowerId).
        // The assignment models one Loan row per Book/Borrower pair.
        public int BookId { get; set; }
        public virtual Book Book { get; set; } = null!;

        public int BorrowerId { get; set; }
        public virtual Borrower Borrower { get; set; } = null!;

        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }

    public class LibraryDbContext : DbContext
    {
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Borrower> Borrowers => Set<Borrower>();
        public DbSet<Loan> Loans => Set<Loan>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=Assignment14_Library;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Loan>()
                .HasKey(l => new { l.BookId, l.BorrowerId });

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId);

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Borrower)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BorrowerId);
        }
    }
}

// ============================================================
// PROJECT 3: HEALTH CARE SYSTEM
// Patient (Many) <---> (Many) Doctor via Appointment
// ============================================================
namespace Assignment_14.HealthCare
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }

    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }

    public class Appointment
    {
        // Composite primary key includes the date so the same patient
        // can have multiple appointments with the same doctor.
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; } = null!;

        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; } = null!;

        public DateTime AppointmentDate { get; set; }
    }

    public class HealthCareDbContext : DbContext
    {
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Appointment> Appointments => Set<Appointment>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=Assignment14_HealthCare;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasKey(a => new { a.PatientId, a.DoctorId, a.AppointmentDate });

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId);
        }
    }
}

// ============================================================
// PROGRAM: Small CRUD / navigation-property demo, as in Session 14
// ============================================================
namespace Assignment_14
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                RunECommerceDemo();
                RunLibraryDemo();
                RunHealthCareDemo();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database operation failed. Check SQL Server and the connection strings.");
                Console.WriteLine(ex.Message);
            }
        }

        private static void RunECommerceDemo()
        {
            using var dbContext = new Shop.ECommerceDbContext();
            dbContext.Database.EnsureCreated(); // For this standalone demo, not EF Migrations.

            // CREATE: insert sample related entities once.
            if (!dbContext.Categories.Any())
            {
                var category = new Shop.Category { Name = "Electronics" };
                var product = new Shop.Product
                {
                    Name = "Keyboard",
                    Price = 650m,
                    Category = category
                };
                var customer = new Shop.Customer
                {
                    Name = "Ahmed",
                    Email = "ahmed@example.com"
                };
                var order = new Shop.Order
                {
                    OrderDate = DateTime.Today,
                    Customer = customer
                };
                order.OrderDetails.Add(new Shop.OrderDetail
                {
                    Product = product,
                    Quantity = 2
                });

                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
            }

            // UPDATE: tracked objects are saved automatically by SaveChanges().
            var existingProduct = dbContext.Products.FirstOrDefault(p => p.Name == "Keyboard");
            if (existingProduct != null)
            {
                existingProduct.Price = 700m;
                dbContext.SaveChanges();
            }

            // READ: Include and ThenInclude load navigation properties.
            var orders = dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                        .ThenInclude(p => p.Category)
                .AsNoTracking()
                .ToList();

            Console.WriteLine("=== PROJECT 1: E-COMMERCE ===");
            foreach (var order in orders)
            {
                Console.WriteLine($"Order #{order.Id} | Customer: {order.Customer.Name} | Date: {order.OrderDate:d}");
                foreach (var detail in order.OrderDetails)
                {
                    Console.WriteLine($"  Product: {detail.Product.Name} | Category: {detail.Product.Category.Name} | Qty: {detail.Quantity} | Price: {detail.Product.Price:C}");
                }
            }

            // DELETE example: remove only a temporary product, not real order data.
            var temporary = new Shop.Product
            {
                Name = "Temporary Product",
                Price = 1m,
                CategoryId = dbContext.Categories.Select(c => c.Id).First()
            };
            dbContext.Products.Add(temporary);
            dbContext.SaveChanges();
            dbContext.Products.Remove(temporary);
            dbContext.SaveChanges();
            Console.WriteLine();
        }

        private static void RunLibraryDemo()
        {
            using var dbContext = new Books.LibraryDbContext();
            dbContext.Database.EnsureCreated();

            if (!dbContext.Authors.Any())
            {
                var author = new Books.Author
                {
                    Name = "Naguib Mahfouz",
                    BirthDate = new DateTime(1911, 12, 11)
                };
                var book = new Books.Book
                {
                    Title = "Cairo Trilogy",
                    ISBN = "9780000000002",
                    Author = author
                };
                var borrower = new Books.Borrower
                {
                    Name = "Mai",
                    MembershipDate = DateTime.Today
                };
                var loan = new Books.Loan
                {
                    Book = book,
                    Borrower = borrower,
                    LoanDate = DateTime.Today,
                    ReturnDate = null
                };

                dbContext.Loans.Add(loan);
                dbContext.SaveChanges();
            }

            var loans = dbContext.Loans
                .Include(l => l.Book)
                    .ThenInclude(b => b.Author)
                .Include(l => l.Borrower)
                .AsNoTracking()
                .ToList();

            Console.WriteLine("=== PROJECT 2: LIBRARY ===");
            foreach (var loan in loans)
            {
                Console.WriteLine($"Book: {loan.Book.Title} | Author: {loan.Book.Author.Name} | Borrower: {loan.Borrower.Name} | Loan Date: {loan.LoanDate:d}");
            }
            Console.WriteLine();
        }

        private static void RunHealthCareDemo()
        {
            using var dbContext = new Clinic.HealthCareDbContext();
            dbContext.Database.EnsureCreated();

            if (!dbContext.Patients.Any())
            {
                var patient = new Clinic.Patient
                {
                    Name = "Sara",
                    DateOfBirth = new DateTime(2001, 5, 10)
                };
                var doctor = new Clinic.Doctor
                {
                    Name = "Dr. Ali",
                    Specialization = "Cardiology"
                };
                var appointment = new Clinic.Appointment
                {
                    Patient = patient,
                    Doctor = doctor,
                    AppointmentDate = DateTime.Today.AddHours(10)
                };

                dbContext.Appointments.Add(appointment);
                dbContext.SaveChanges();
            }

            var appointments = dbContext.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .AsNoTracking()
                .ToList();

            Console.WriteLine("=== PROJECT 3: HEALTH CARE ===");
            foreach (var appointment in appointments)
            {
                Console.WriteLine($"Patient: {appointment.Patient.Name} | Doctor: {appointment.Doctor.Name} | Specialty: {appointment.Doctor.Specialization} | Appointment: {appointment.AppointmentDate:g}");
            }
        }
    }
}
